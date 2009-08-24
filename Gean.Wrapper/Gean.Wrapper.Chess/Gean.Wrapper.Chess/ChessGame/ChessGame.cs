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

        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame() : this(FENBuilder.NewGame) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame(FENBuilder fenBuilder)
        {
            this.LoadPositions();
            this.CurrFENBuilder = fenBuilder;
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        protected virtual void LoadPositions()
        {
            this.ChessPositions = new ChessPosition[64];
            for (int x = 1; x <= 64; x++)
            {
                this.ChessPositions[x - 1] = ChessPosition.GetPositionByDot(x);
            }
        }

        #endregion

        #region this

        /// <summary>
        /// 根据FEN的点(1-64)来获取位置
        /// </summary>
        public virtual ChessPosition this[int dot]
        {
            get { return this.ChessPositions[dot - 1]; }
        }
        /// <summary>
        /// 获取指定坐标值的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="x">棋格的x坐标(按象棋规则，不能为0)</param>
        /// <param name="y">棋格的y坐标(按象棋规则，不能为0)</param>
        public virtual ChessPosition this[int x, int y]
        {
            get { return this[ChessPosition.CalculateDot(x - 1, y - 1)]; }
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

        #endregion

        #region FENBuilder

        /// <summary>
        /// 棋局当前FEN记录
        /// </summary>
        public virtual FENBuilder CurrFENBuilder { get; private set; }
        /// <summary>
        /// 棋局当前回合数
        /// </summary>
        public virtual int Number
        {
            get { return CurrFENBuilder.FullMove; }
            set { CurrFENBuilder.FullMove = value; }
        }
        public virtual Enums.ChessmanSide ChessmanSide
        {
            get { return Enums.ToChessmanSide(CurrFENBuilder.Color); }
            set { CurrFENBuilder.Color = Enums.FormChessmanSide(value); }
        }
        public virtual bool BlackCastleKing
        {
            get { return CurrFENBuilder.BlackCastleKing; }
            set { CurrFENBuilder.BlackCastleKing = value; }
        }
        public virtual bool BlackCastleQueen
        {
            get { return CurrFENBuilder.BlackCastleQueen; }
            set { CurrFENBuilder.BlackCastleQueen = value; }
        }
        public virtual bool WhiteCastleKing
        {
            get { return CurrFENBuilder.WhiteCastleKing; }
            set { CurrFENBuilder.WhiteCastleKing = value; }
        }
        public virtual bool WhiteCastleQueen
        {
            get { return CurrFENBuilder.WhiteCastleQueen; }
            set { CurrFENBuilder.WhiteCastleQueen = value; }
        }
        public virtual string Enpassant
        {
            get { return CurrFENBuilder.Enpassant; }
            set { CurrFENBuilder.Enpassant = value; }
        }
        public virtual int HalfMove
        {
            get { return CurrFENBuilder.HalfMove; }
            set { CurrFENBuilder.HalfMove = value; }
        }

        #endregion

        /// <summary>
        /// 获取本局棋的记录
        /// </summary>
        public virtual ChessRecord Record { get; private set; }

        public bool HasChessman(ChessPosition position, out Enums.ChessmanType type)
        {
            type = Enums.ChessmanType.None;
            char c = this.CurrFENBuilder[position.Dot];
            if (c == '1')
                return false;
            else
                type = Enums.ToChessmanType(c);
            return true;
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
        public ChessStep MoveIn(FENBuilder fen, Chessman chessman, ChessPosition srcPos, ChessPosition tgtPos)
        {
            //指定的棋子为空或动作为空
            if (Chessman.IsNullOrEmpty(chessman) || tgtPos == ChessPosition.Empty)
                throw new ArgumentNullException();
            Enums.Action action = Enums.Action.General;


            ChessStep chessStep = new ChessStep(Number, chessman.ChessmanSide, chessman.ChessmanType, srcPos, tgtPos, action);
            this.CurrFENBuilder = this.CurrFENBuilder.Move(chessman, action, srcPos, tgtPos);
            //注册行棋事件
            OnMoveIn(new MoveInEventArgs(action, chessman.ChessmanSide, chessStep));
            return chessStep;
        }

        #endregion

        #region IEnumerable

        #region IEnumerable<ChessPosition> 成员

        public IEnumerator<ChessPosition> GetEnumerator()
        {
            return (IEnumerator<ChessPosition>)this.ChessPositions.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.ChessPositions.GetEnumerator();
        }

        #endregion

        /*
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
        */

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