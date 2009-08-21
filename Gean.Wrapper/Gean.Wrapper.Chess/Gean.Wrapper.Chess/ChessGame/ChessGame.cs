using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 这是描述一盘棋局的类。
    /// 它与ChessRecord的主要区别在于：
    /// 1.Game主要实现一系列棋子，棋子移动相关的方法，事件；
    /// 2.Record主要实现棋局记录相关的方法，事件；
    /// 已实现IEnumerable&lt;ChessPosition&gt;。
    /// </summary>
    public class ChessGame : IEnumerable<ChessPosition>
    {
        /// <summary>
        /// 获取与设置一盘棋局的所有棋格类
        /// </summary>
        protected virtual ChessPosition[] ChessPositions { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame() : this(new ChessRecord()) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame(ChessRecord record)
        {
            this.Record = record;
            this.LoadPositions();
        }

        /// <summary>
        /// 获取本局棋的记录
        /// </summary>
        public virtual ChessRecord Record { get; private set; }

        public virtual ChessPosition this[int dot]
        {
            get { return ChessPosition.Empty; }
        }
        /// <summary>
        /// 获取指定坐标值的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="x">棋格的x坐标(按象棋规则，不能为0)</param>
        /// <param name="y">棋格的y坐标(按象棋规则，不能为0)</param>
        public virtual ChessPosition this[int x, int y]
        {
            get { return this.ChessPositions[ChessPosition.CalculateDot(x - 1, y - 1)]; }
        }
        /// <summary>
        /// 获取指定坐标值的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="c">棋格的x坐标(按象棋规则，a-h)</param>
        /// <param name="y">棋格的y坐标(按象棋规则，不能为0)</param>
        public virtual ChessPosition this[char c, int y]
        {
            get { return this[Utility.CharToInt(c) - 1, y - 1]; }
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        protected virtual void LoadPositions()
        {
            this.ChessPositions = new ChessPosition[64];
            for (int x = 1; x <= 64; x++)
            {
                this.ChessPositions[x] = ChessPosition.GetPositionByDot(x);
            }
        }

        #region === Move ===

        /// <summary>
        /// 将指定的棋子移到本棋格(已注册杀棋事件与落子事件)。
        /// 1.棋子的战方应在调用该方法之前进行判断;
        /// 2.该棋子是否能够落入指定的棋格应在调用该方法之前进行判断;
        /// 重点方法。
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        /// <param name="action">该棋步的动作</param>
        public ChessStep MoveIn(int number, Chessman chessman, Enums.Action action, ChessPosition srcPos, ChessPosition tgtPos)
        {
            //指定的棋子为空或动作为空
            if (Chessman.IsNullOrEmpty(chessman) || action == Enums.Action.Invalid)
                throw new ArgumentNullException();

            switch (action)
            {
                #region case
                case Enums.Action.General:
                case Enums.Action.Check:
                    this.MoveInByGeneralAction(chessman);
                    break;
                case Enums.Action.Capture:
                    this.MoveInByCapture(chessman);
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
                //将棋步插入到该棋子的棋步集合中
                chessman.ChessPositions.Push(tgtPos);
            }
            ChessStep chessStep = new ChessStep(number, chessman.ChessmanSide, chessman.ChessmanType, srcPos, tgtPos, action);
            //注册行棋事件
            OnMoveIn(new MoveInEventArgs(action, chessman.ChessmanSide, chessStep));
            return chessStep;
        }

        /// <summary>
        /// 对指定的棋子执行的动子并落子的方法(含“杀棋”动作和“杀棋并将军”)
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        private void MoveInByCapture(Chessman chessman)
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
        private void MoveInByGeneralAction(Chessman chessman)
        {
            //1.动子（即从源棋格中移除该棋子）
            ChessPosition point = chessman.ChessPositions.Peek();
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
        private void MoveOut(bool isCapture)
        {
            Chessman man = this.Occupant;//棋格中的棋子
            man.IsCaptured = isCapture;//置该棋子的死活棋开关为“被杀死”状态
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

        ///// <summary>
        ///// 获取或设置当前格子是否被棋子占住
        ///// </summary>
        //public bool Occupied
        //{
        //    get { return _occupied; }
        //    set
        //    {
        //        if (value == false)
        //            this._occupant = null;
        //        _occupied = value;
        //    }
        //}
        //private bool _occupied = false;


        #endregion


        #region IEnumerable

        #region IEnumerable<ChessPosition> 成员

        public IEnumerator<ChessPosition> GetEnumerator()
        {
            return new ChessGridEnumerator(this.ChessPositions);
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ChessGridEnumerator(this.ChessPositions);
        }

        #endregion

        public class ChessGridEnumerator : IEnumerator<ChessPosition>
        {
            private List<ChessPosition> _chessPositions;
            private int _index = -1;

            public ChessGridEnumerator(ChessPosition[] positions)
            {
                _chessPositions = new List<ChessPosition>(64);
                for (int x = 1; x <= 64; x++)
                {
                    _chessPositions.Add(positions[x - 1]);
                }
            }

            #region IEnumerator<ChessPosition> 成员

            public ChessPosition Current
            {
                get
                {
                    try { return _chessPositions[_index]; }
                    catch
                    { throw new InvalidOperationException(); }
                }
            }

            #endregion

            #region IDisposable 成员

            public void Dispose()
            {
                _chessPositions = null;
            }

            #endregion

            #region IEnumerator 成员

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                _index++;
                return (_index < _chessPositions.Count);
            }

            public void Reset()
            {
                _index = -1;
            }

            #endregion
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