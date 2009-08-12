using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Collections.Generic;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个针对国际象棋棋格的具体似Point描述类型。
    /// 1.声明了动子和落子方法;
    /// 2.注册了棋子的动子、落子、杀子事件。
    /// Gean: 2009-07-30 18:18:37
    /// </summary>
    public sealed class ChessGrid
    {

        #region ctor

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="pointX">棋格的横坐标</param>
        /// <param name="pointY">棋格的纵坐标</param>
        public ChessGrid(int pointX, int pointY)
        {
            #region Exception
            if (!ChessGrid.Verify(pointX, pointY))
            {
                throw new ArgumentOutOfRangeException("坐标值超限！");
            }
            #endregion

            this.X = pointX;
            this.Y = pointY;
            this.Horizontal = Utility.IntToChar(pointY);
            this.GridSide = ChessGrid.GetGridSide(pointY, pointY);
        }

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="c">棋格的横坐标的字符</param>
        /// <param name="y">棋格的纵坐标</param>
        internal ChessGrid(char c, int pointY)
            : this(Utility.CharToInt(c), pointY) { }

        #endregion

        #region === Move ===

        /// <summary>
        /// 将指定的棋子移到本棋格(已注册杀棋事件与落子事件)。
        /// 1.棋子的战方应在调用该方法之前进行判断;
        /// 2.该棋子是否能够落入指定的棋格应在调用该方法之前进行判断;
        /// 重点方法。
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        /// <param name="action">该棋步的动作</param>
        public ChessStep MoveIn(ChessGame game, Chessman chessman, Enums.Action action)
        {
            //指定的棋子为空或动作为空
            if (Chessman.IsNullOrEmpty(chessman) || action == Enums.Action.Invalid)
                throw new ArgumentNullException();
            ChessPosition sourcePoint = chessman.ChessPoints.Peek();
            ChessPosition targetPoint = ChessPosition.Empty;

            switch (action)
            {
                #region case
                case Enums.Action.General:
                case Enums.Action.Check:
                    this.MoveInByGeneralAction(game, chessman);
                    break;
                case Enums.Action.Kill:
                    this.MoveInByKillAction(game, chessman);
                    break;
                case Enums.Action.KingSideCastling:
                    this.MoveInByKingSideCastlingAction();
                    break;
                case Enums.Action.QueenSideCastling:
                    this.MoveInByQueenSideCastlingAction();
                    break;
                case Enums.Action.Opennings://仅开局摆棋，不激活任何相关事件
                    this.Occupant = chessman;
                    break;
                case Enums.Action.Invalid:
                default:
                    Debug.Fail("\" " + action.ToString() + " \" isn't FAIL action.");
                    break;
                #endregion
            }

            if (action != Enums.Action.Opennings)//在开局摆棋时，不需重复绑定Step(在棋子初始化时已绑定)
            {
                //将棋步注册到该棋子的棋步集合中
                targetPoint = new ChessPosition(this.X, this.Y);
                chessman.ChessPoints.Push(new ChessPosition(this.X, this.Y));
            }
            ChessStep chessStep = new ChessStep(chessman.ChessmanSide, chessman.ChessmanType, sourcePoint, targetPoint, action);
            //注册行棋事件
            OnMoveIn(new MoveInEventArgs(action, chessman.ChessmanSide, chessStep));
            return chessStep;
        }

        /// <summary>
        /// 对指定的棋子执行的动子并落子的方法(含“杀棋”动作和“杀棋并将军”)
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        private void MoveInByKillAction(ChessGame game, Chessman chessman)
        {
            //移除被杀死的棋子
            this.MoveOut(true);
            //调用落子方法
            this.MoveInByGeneralAction(game, chessman);
        }

        /// <summary>
        /// 对指定的棋子执行的动子并落子的一般性方法(含“将军”)
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        private void MoveInByGeneralAction(ChessGame chessGame, Chessman chessman)
        {
            //1.动子（即从源棋格中移除该棋子）
            ChessPosition point = chessman.ChessPoints.Peek();
            chessGame[point.X + 1, point.Y + 1].MoveOut(false);

            //2.落子
            this.Occupant = chessman;
        }

        /// <summary>
        /// 长易位(后侧)
        /// </summary>
        /// <param name="chessman"></param>
        private void MoveInByQueenSideCastlingAction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 短易位(王侧)
        /// </summary>
        /// <param name="chessman"></param>
        private void MoveInByKingSideCastlingAction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将本棋格的棋子移除(已注册移除事件)。
        /// </summary>
        /// <param name="isKill">
        /// 是否是杀招，true 时指被移除的棋是被杀死，false 时指被移除
        /// 的棋仅为“动子”，该棋子还将被移到其他的棋格。
        /// </param>
        private void MoveOut(bool isKill)
        {
            Chessman man = this.Occupant;//棋格中的棋子
            man.IsKilled = isKill;//置该棋子的死活棋开关为“被杀死”状态
            //移除棋子
            this.Occupied = false;
        }

        #endregion

        #region Occupant Chessman

        /// <summary>
        /// 获取或设置当前格子中拥有的棋子
        /// </summary>
        public Chessman Occupant
        {
            get { return _occupant; }
            set
            {
                if (value != null)
                    this._occupied = true;
                else
                    this._occupied = false;
                this._occupant = value;
            }
        }
        private Chessman _occupant;

        /// <summary>
        /// 获取或设置当前格子是否被棋子占住
        /// </summary>
        public bool Occupied
        {
            get { return _occupied; }
            set
            {
                if (value == false)
                    this._occupant = null;
                _occupied = value;
            }
        }
        private bool _occupied = false;


        #endregion

        #region Grid Point

        /// <summary>
        /// 棋格在棋盘上的横坐标
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// 棋格在棋盘上的纵坐标
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// 棋格在棋盘上的横坐标的字母表示法。
        /// </summary>
        public char Horizontal { get; private set; }
        /// <summary>
        /// 棋格在棋盘上的纵坐标的表示法。
        /// </summary>
        public int Vertical { get; private set; }

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
            sb.Append(this.Horizontal).Append(this.Y);
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessGrid rid = (ChessGrid)obj;
            if (!rid.GridSide.Equals(this.GridSide))
                return false;
            if (!rid.X.Equals(this.X))
                return false;
            if (!rid.Y.Equals(this.Y))
                return false;
            if (!UtilityEquals.PairEquals(this.Occupant, rid.Occupant))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked((X.GetHashCode() + Y.GetHashCode() + GridSide.GetHashCode()) * 3);
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
        public static bool Verify(int x, int y)
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

        #region custom MoveIn Event

        /// <summary>
        /// 在该棋格中落子后发生
        /// </summary>
        public event MoveInEventHandler MoveInEvent;
        private void OnMoveIn(MoveInEventArgs e)
        {
            if (MoveInEvent != null)
                MoveInEvent(this, e);
        }
        public delegate void MoveInEventHandler(object sender, MoveInEventArgs e);
        public class MoveInEventArgs : EventArgs
        {
            public Enums.Action Action { get; private set; }
            public Enums.ChessmanSide ChessmanSide { get; private set; }
            public ChessStep ChessStep { get; private set; }
            public MoveInEventArgs(Enums.Action action, Enums.ChessmanSide chessmanSide, ChessStep chessStep)
            {
                this.Action = action;
                this.ChessmanSide = chessmanSide;
                this.ChessStep = chessStep;
            }
        }

        #endregion

    }
}
