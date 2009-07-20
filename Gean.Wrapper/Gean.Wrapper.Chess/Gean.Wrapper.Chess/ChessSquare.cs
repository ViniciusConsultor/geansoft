using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一种国际象棋棋格的坐标表示方法。类似Point的一个类，比Point多一些与返回字母的方法。
    /// </summary>
    public class ChessSquare
    {
        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="x">棋格的横坐标</param>
        /// <param name="y">棋格的纵坐标</param>
        public ChessSquare(int x, int y, Enums.ChessSquareSide side)
        {
            #region Exception
            if (!ChessSquare.Check(x, y))
            {
                throw new ArgumentOutOfRangeException("坐标值超限！");
            }
            #endregion

            this.X = x;
            this.Y = y;
            this.ChessSquareSide = side;
        }
        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="c">棋格的横坐标的字符</param>
        /// <param name="y">棋格的纵坐标</param>
        public ChessSquare(char c, int y, Enums.ChessSquareSide side)
        {
            int i = Utility.CharToInt(c);

            #region Exception
            if (!ChessSquare.Check(i, y))
            {
                throw new ArgumentOutOfRangeException("坐标值超限！");
            }
            #endregion

            this.X = i;
            this.Y = y;
            this.ChessSquareSide = side;
        }

        /// <summary>
        /// 棋格的横坐标
        /// </summary>
        public int X { get; internal set; }
        /// <summary>
        /// 棋格的纵坐标
        /// </summary>
        public int Y { get; internal set; }
        /// <summary>
        /// 黑格,白格
        /// </summary>
        public Enums.ChessSquareSide ChessSquareSide { get; internal set; }

        /// <summary>
        /// 棋格的横坐标的字母表示法。
        /// </summary>
        public char CharX
        {
            get { return Utility.IntToChar(this.X); }
        }

        /// <summary>
        /// 获取该棋格坐标点的有效性
        /// </summary>
        public bool IsAvailability
        {
            get
            {
                return ChessSquare.Check(this.X, this.Y);
            }
        }

        /// <summary>
        /// 按指定的宽度返回该Grid描述的棋格的矩形。
        /// </summary>
        /// <param name="width">指定的宽度</param>
        /// <returns></returns>
        public Rectangle GetRectangle(int width)
        {
            Point point = new Point((X - 1) * width, (8 - Y) * width);
            Size size = new Size(width, width);
            return new Rectangle(point, size);
        }

        /// <summary>
        /// 获取或设置当前格子中拥有的棋子
        /// </summary>
        public Chessman OwnedChessman
        {
            get { return this._ownedChessman; }
            set
            {
                OnPlayBefore(new PlayEventArgs(value));//注册落子前事件
                this._ownedChessman = value;
                OnPlayAfter(new PlayEventArgs(value));//注册落子后事件
            }
        }
        private Chessman _ownedChessman = null;

        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetX(int x)
        {
            this.X = x;
        }
        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="c">一个字母，是一个不能小于a,大于h的字母</param>
        public void SetX(char c)
        {
            this.X = Utility.CharToInt(c);
        }
        /// <summary>
        /// 用指定的值设置棋格的纵坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetY(int i)
        {
            this.Y = i;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.CharX).Append(this.Y);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            ChessSquare grid = (ChessSquare)obj;
            if (!grid.X.Equals(this.X))
                return false;
            if (!grid.Y.Equals(this.Y))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked((this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.ChessSquareSide.GetHashCode()) * 17);
        }

        public static bool operator !=(ChessSquare a, ChessSquare b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(ChessSquare a, ChessSquare b)
        {
            return a.Equals(b);
        }

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

        public static ChessSquare Parse(string square, Enums.ChessSquareSide squareSide)
        {
            if (string.IsNullOrEmpty(square))
                throw new ArgumentOutOfRangeException("Square IsNullOrEmpty!");
            if (square.Length != 2)
                throw new ArgumentOutOfRangeException("\"" + square + "\" is OutOfRange!");
            int x; int y;
            x = Utility.CharToInt(square[0]);
            y = Convert.ToInt32(square.Substring(1));
            return new ChessSquare(x, y, squareSide);
        }

        #region custom event

        /// <summary>
        /// 在该棋格中拥有的棋子发生变化的时候(变化前、即将发生变化)发生。
        /// (包括该在棋格落子和移走该棋格拥有的棋子)
        /// </summary>
        public event PlayBeforeEventHandler PlayBeforeEvent;
        protected virtual void OnPlayBefore(PlayEventArgs e)
        {
            if (PlayBeforeEvent != null)
                PlayBeforeEvent(this, e);
        }
        public delegate void PlayBeforeEventHandler(object sender, PlayEventArgs e);

        /// <summary>
        /// 在该棋格中拥有的棋子发生变化后发生(包括该在棋格落子和移走该棋格拥有的棋子)
        /// </summary>
        public event PlayAfterEventHandler PlayAfterEvent;
        protected virtual void OnPlayAfter(PlayEventArgs e)
        {
            if (PlayAfterEvent != null)
                PlayAfterEvent(this, e);
        }
        public delegate void PlayAfterEventHandler(object sender, PlayEventArgs e);

        public class PlayEventArgs : ChessmanEventArgs
        {
            public PlayEventArgs(Chessman man)
                : base(man)
            {

            }
        }

        #endregion
    }
}
