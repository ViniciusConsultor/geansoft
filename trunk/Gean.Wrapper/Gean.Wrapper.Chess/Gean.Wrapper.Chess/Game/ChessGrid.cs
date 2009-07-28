﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一种针对国际象棋棋格的坐标表示方法的具体描述类型。
    /// 该类型组合了Rectangle类型和一个似Point的类型。
    /// Gean: 2009-07-28 11:55:01
    /// </summary>
    public class ChessGrid
    {

        #region ctor

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="pointX">棋格的横坐标</param>
        /// <param name="pointY">棋格的纵坐标</param>
        /// <param name="Rectangle">棋格的实际计算机矩形</param>
        public ChessGrid(int pointX, int pointY, Rectangle rect)
        {
            #region Exception
            if (!ChessGrid.Check(pointX, pointY))
            {
                throw new ArgumentOutOfRangeException("坐标值超限！");
            }
            #endregion

            this.PointX = pointX;
            this.PointY = pointY;
            this.PointCharX = Utility.IntToChar(pointY);
            this.GridSide = ChessGrid.GetGridSide(pointY, pointY);

            this.OwnedRectangle = rect;
        }

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="pointX">棋格的横坐标</param>
        /// <param name="pointY">棋格的纵坐标</param>
        /// <param name="location">棋格的实际计算机坐标对</param>
        /// <param name="size">棋格的实际计算机长宽值</param>
        public ChessGrid(int pointX, int pointY, Point location, Size size)
            : this(pointX, pointY, new Rectangle(location, size)) { }

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="x">棋格的横坐标</param>
        /// <param name="y">棋格的纵坐标</param>
        internal ChessGrid(int pointX, int pointY)
            : this(pointX, pointY, Rectangle.Empty) { }

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="c">棋格的横坐标的字符</param>
        /// <param name="y">棋格的纵坐标</param>
        internal ChessGrid(char c, int pointY)
            : this(Utility.CharToInt(c), pointY, Rectangle.Empty) { }

        #endregion

        #region chessman

        /// <summary>
        /// 获取或设置当前格子中拥有的棋子
        /// </summary>
        public Chessman OwnedChessman { get; private set; }

        /// <summary>
        /// 将指定的棋子移到本棋格，实际都是调用this.SetOwnedChessman方法(已注册落子事件)。
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        public bool MoveIn(Chessman chessman)
        {
            if (chessman == null)
                throw new ArgumentNullException();
            if (this.OwnedChessman == null)//本棋格中无棋子
            {
                OnMoveInBefore(new MoveEventArgs(chessman));//注册落子前事件
                this.OwnedChessman = chessman;
                OnMoveInAfter(new MoveEventArgs(chessman));//注册落子后事件
            }
            else//本棋格中有棋子
            {
                if (this.OwnedChessman.ChessmanSide == chessman.ChessmanSide)
                {
                    Debug.Fail("ChessmanSide isn't same!");
                    return false;
                }
                OnKilling(new ChessmanKillEventArgs(this, this.OwnedChessman, chessman.ChessGrids.Peek(), chessman));
                OnMoveInBefore(new MoveEventArgs(chessman));//注册落子前事件
                this.OwnedChessman = chessman;
                OnMoveInAfter(new MoveEventArgs(chessman));//注册落子后事件
                OnKilled(new ChessmanKillEventArgs(this, this.OwnedChessman, chessman.ChessGrids.Peek(), chessman));
            }
            return true;
        }
        /// <summary>
        /// 将本棋格的棋子移除，实际都是调用this.SetOwnedChessman方法(已注册移除事件)。
        /// </summary>
        public void MoveOut()
        {
            if (this.OwnedChessman != null)//本棋格中无棋子
            {
                OnMoveOutBefore(new MoveEventArgs(Chessman.Empty));//注册动子前事件
                this.OwnedChessman = Chessman.Empty;
                OnMoveOutBefore(new MoveEventArgs(Chessman.Empty));//注册动子后事件
            }
        }

        #endregion

        #region Inner Rectangle

        /// <summary>
        /// 该实体棋格的实际矩形
        /// </summary>
        public Rectangle OwnedRectangle { get; private set; }
        public Point RectangleLocation
        {
            get { return this.OwnedRectangle.Location; }
        }
        public Size RectangleSize
        {
            get { return this.OwnedRectangle.Size; }
        }
        public int RectangleX
        {
            get { return this.OwnedRectangle.X; }
        }
        public int RectangleY
        {
            get { return this.OwnedRectangle.Y; }
        }
        public int RectangleHeight
        {
            get { return this.OwnedRectangle.Height; }
        }
        public int RectangleWidth
        {
            get { return this.OwnedRectangle.Width; }
        }
        /// <summary>
        /// 获取 x 坐标，该坐标是此 Rectangle 结构的 Rectangle.X 与 Rectangle.Width 属性值之和。
        /// </summary>
        public int RectangleRight
        {
            get { return this.OwnedRectangle.Right; }
        }
        /// <summary>
        /// 获取 y 坐标，该坐标是此 Rectangle 结构的 Rectangle.Y 与 Rectangle.Height 属性值之和。
        /// </summary>
        public int RectangleBottom
        {
            get { return this.OwnedRectangle.Bottom; }
        }

        public bool RectangleContains(Point pt)
        {
            return this.OwnedRectangle.Contains(pt);
        }
        public bool RectangleContains(int x, int y)
        {
            return this.OwnedRectangle.Contains(x, y);
        }

        #endregion

        #region Grid Point

        /// <summary>
        /// 棋格在棋盘上的横坐标
        /// </summary>
        public int PointX { get; internal set; }
        /// <summary>
        /// 棋格在棋盘上的横坐标的字母表示法。
        /// </summary>
        public char PointCharX { get; private set; }
        /// <summary>
        /// 棋格在棋盘上的纵坐标
        /// </summary>
        public int PointY { get; internal set; }
        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetPointX(int pointX)
        {
            this.PointX = pointX;
        }
        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="c">一个字母，是一个不能小于a,大于h的字母</param>
        public void SetPointX(char pointCharX)
        {
            this.PointX = Utility.CharToInt(pointCharX);
        }
        /// <summary>
        /// 用指定的值设置棋格的纵坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetPointY(int pointY)
        {
            this.PointY = pointY;
        }

        #endregion

        #region Grid Side

        /// <summary>
        /// 黑格,白格
        /// </summary>
        public Enums.ChessGridSide GridSide { get; internal set; }
        /// <summary>
        /// 根据坐标值判断该格是黑格还是白格
        /// </summary>
        private static Enums.ChessGridSide GetGridSide(int pointX, int pointY)
        {
            if ((pointY % 2) == 0)
            {
                if ((pointX % 2) == 0)
                    return Enums.ChessGridSide.White;
                else
                    return Enums.ChessGridSide.Black;
            }
            else
            {
                if ((pointX % 2) == 0)
                    return Enums.ChessGridSide.Black;
                else
                    return Enums.ChessGridSide.White;
            }
        }

        #endregion

        #region override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.PointCharX).Append(this.PointY);
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ChessGrid point = (ChessGrid)obj;
            if (!point.GridSide.Equals(this.GridSide))
                return false;
            if (!point.PointX.Equals(this.PointX))
                return false;
            if (!point.PointY.Equals(this.PointY))
                return false;
            if (!UtilityEquals.PairEquals(this.OwnedChessman, point.OwnedChessman))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked((PointX.GetHashCode() + PointY.GetHashCode() + GridSide.GetHashCode()) * 3);
        }

        #endregion

        #region static

        /// <summary>
        /// 返回一个为空的值。该变量为只读。
        /// </summary>
        public static readonly ChessGrid Empty = null;

        /// <summary>
        /// 检查指定的一双整数值是否符合棋盘坐标值的限制
        /// </summary>
        public static bool Check(int x, int y)
        {
            if (((x >= 1 && x <= 8)) && ((y >= 1 && y <= 8)))
                return true;
            else
                return false;
        }

        public static ChessGrid Parse(string point)
        {
            if (string.IsNullOrEmpty(point))
                throw new ArgumentOutOfRangeException("point IsNullOrEmpty!");
            if (point.Length != 2)
                throw new ArgumentOutOfRangeException("\"" + point + "\" is OutOfRange!");

            int x = Utility.CharToInt(point[0]);
            int y = Convert.ToInt32(point.Substring(1));
            return new ChessGrid(x, y);
        }

        #endregion

        #region custom event

        /* 动子与落子
         * 动子（MoveOut）
         * 落子（MoveIn）
         */

        /// <summary>
        /// 在该棋格即将动子前发生
        /// </summary>
        public event MoveOutBeforeEventHandler MoveOutBeforeEvent;
        protected virtual void OnMoveOutBefore(MoveEventArgs e)
        {
            if (MoveOutBeforeEvent != null)
                MoveOutBeforeEvent(this, e);
        }
        public delegate void MoveOutBeforeEventHandler(object sender, MoveEventArgs e);

        /// <summary>
        /// 在该棋格动子后发生
        /// </summary>
        public event MoveOutAfterEventHandler MoveOutAfterEvent;
        protected virtual void OnMoveOutAfter(MoveEventArgs e)
        {
            if (MoveOutAfterEvent != null)
                MoveOutAfterEvent(this, e);
        }
        public delegate void MoveOutAfterEventHandler(object sender, MoveEventArgs e);

        /// <summary>
        /// 在该棋格中即将落子的时候发生。
        /// </summary>
        public event MoveInBeforeEventHandler MoveInBeforeEvent;
        protected virtual void OnMoveInBefore(MoveEventArgs e)
        {
            if (MoveInBeforeEvent != null)
                MoveInBeforeEvent(this, e);
        }
        public delegate void MoveInBeforeEventHandler(object sender, MoveEventArgs e);

        /// <summary>
        /// 在该棋格中落子后发生
        /// </summary>
        public event MoveInAfterEventHandler MoveInAfterEvent;
        protected virtual void OnMoveInAfter(MoveEventArgs e)
        {
            if (MoveInAfterEvent != null)
                MoveInAfterEvent(this, e);
        }
        public delegate void MoveInAfterEventHandler(object sender, MoveEventArgs e);

        public class MoveEventArgs : ChessmanEventArgs
        {
            public MoveEventArgs(Chessman man)
                : base(man) { }
        }

        /*棋子被杀
         */

        /// <summary>
        /// 在该棋子被杀死后发生
        /// </summary>
        public event KilledEventHandler KilledEvent;
        protected virtual void OnKilled(ChessmanKillEventArgs e)
        {
            if (KilledEvent != null)
                KilledEvent(this, e);
        }
        public delegate void KilledEventHandler(object sender, ChessmanKillEventArgs e);

        /// <summary>
        /// 在该棋子正在被杀死（也可理解为，即将被杀死时）发生
        /// </summary>
        public event KillingEventHandler KillingEvent;
        protected virtual void OnKilling(ChessmanKillEventArgs e)
        {
            if (KillingEvent != null)
                KillingEvent(this, e);
        }
        public delegate void KillingEventHandler(object sender, ChessmanKillEventArgs e);

        /// <summary>
        /// 包含棋子杀死事件的数据
        /// </summary>
        public class ChessmanKillEventArgs : EventArgs
        {
            /// <summary>
            /// 被杀的棋所在的棋格
            /// </summary>
            public ChessGrid CurrGrid { get; private set; }
            /// <summary>
            /// 被杀的棋
            /// </summary>
            public Chessman CurrChessman { get; private set; }

            /// <summary>
            /// 杀棋的棋来路所在的棋格（杀棋的棋的源棋格）
            /// </summary>
            public ChessGrid SourceGrid { get; private set; }
            /// <summary>
            /// 杀棋的棋
            /// </summary>
            public Chessman SourceChessman { get; private set; }

            /// <summary>
            /// 包含棋子杀死事件的数据
            /// </summary>
            /// <param name="currGrid">被杀的棋所在的棋格</param>
            /// <param name="currChessman">被杀的棋</param>
            /// <param name="sourceGrid">杀棋的棋来路所在的棋格（杀棋的棋的源棋格）</param>
            /// <param name="sourceChessman">杀棋的棋</param>
            public ChessmanKillEventArgs(ChessGrid currGrid, Chessman currChessman, ChessGrid sourceGrid, Chessman sourceChessman)
            {
                this.CurrGrid = currGrid;
                this.CurrChessman = currChessman;
                this.SourceGrid = sourceGrid;
                this.SourceChessman = sourceChessman;
            }
        }


        #endregion

    }
}
