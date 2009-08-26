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
            this.Record = new ChessRecord();

            this.FenNotation = fenBuilder;
            this.LoadPositions();
            if (fenBuilder.Equals(FENBuilder.NewGame))
            {
                this.InitializeChessmans();
            }
            else
            {
                this.InitializeChessmans(fenBuilder.ToChessmans());
            }
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

        #region property

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
        public virtual FENBuilder FenNotation { get; private set; }
        /// <summary>
        /// 棋局当前回合数
        /// </summary>
        public virtual int Number
        {
            get { return FenNotation.FullMove; }
            set { FenNotation.FullMove = value; }
        }
        public virtual Enums.GameSide GameSide
        {
            get { return Enums.ToGameSide(FenNotation.Color); }
            set { FenNotation.Color = Enums.FormGameSide(value); }
        }
        public virtual bool BlackCastleKing
        {
            get { return FenNotation.BlackCastleKing; }
            set { FenNotation.BlackCastleKing = value; }
        }
        public virtual bool BlackCastleQueen
        {
            get { return FenNotation.BlackCastleQueen; }
            set { FenNotation.BlackCastleQueen = value; }
        }
        public virtual bool WhiteCastleKing
        {
            get { return FenNotation.WhiteCastleKing; }
            set { FenNotation.WhiteCastleKing = value; }
        }
        public virtual bool WhiteCastleQueen
        {
            get { return FenNotation.WhiteCastleQueen; }
            set { FenNotation.WhiteCastleQueen = value; }
        }
        public virtual string Enpassant
        {
            get { return FenNotation.Enpassant; }
            set { FenNotation.Enpassant = value; }
        }
        public virtual int HalfMove
        {
            get { return FenNotation.HalfMove; }
            set { FenNotation.HalfMove = value; }
        }

        #endregion

        /// <summary>
        /// 当前棋盘所拥有的棋子集合
        /// </summary>
        public virtual ChessmanCollection Chessmans { get; private set; }

        /// <summary>
        /// 获取本局棋的记录
        /// </summary>
        public virtual ChessRecord Record { get; private set; }

        /// <summary>
        /// 返回指定的位置是否有棋子
        /// </summary>
        /// <param name="position">指定的位置</param>
        /// <returns></returns>
        public virtual bool HasChessman(ChessPosition position)
        {
            char c = this.FenNotation[position.Dot];
            if (c == '1')
                return false;
            else
                return true;
        }

        #endregion

        #region Initialize Chessmans

        /// <summary>
        /// 初始化默认棋子集合(32个棋子)
        /// </summary>
        protected virtual void InitializeChessmans()
        {
            this.Chessmans = ChessmanCollection.OpeningChessmansCreator();
            foreach (Chessman man in this.Chessmans)
            {
                man.PositionChangedEvent += new Chessman.PositionChangedEventHandler(ChessmanPositionChangedEvent);
            }
        }

        /// <summary>
        /// 初始化棋子集合
        /// </summary>
        /// <param name="images">一个指定棋子集合</param>
        protected virtual void InitializeChessmans(IEnumerable<Chessman> chessmans)
        {
            this.Chessmans = new ChessmanCollection();
            this.Chessmans.AddRange(chessmans);
            foreach (Chessman man in this.Chessmans)
            {
                man.PositionChangedEvent += new Chessman.PositionChangedEventHandler(ChessmanPositionChangedEvent);
            }
        }

        #endregion

        #region === Move ===

        protected virtual void ChessmanPositionChangedEvent(object sender, Chessman.PositionChangedEventArgs e)
        {
            this.MoveIn(e.Action, e.MovedChessman, e.CaptruedChessman, e.OldItem, e.NewItem);
        }

        /// <summary>
        /// 将指定的棋子从指定源位置移到目标位置。
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        /// <param name="srcPos">指定源位置</param>
        /// <param name="tgtPos">指定目标位置</param>
        protected virtual void MoveIn(Enums.Action action, 
                                      Chessman movedChessman, 
                                      Chessman captruedChessman, 
                                      ChessPosition srcPos, 
                                      ChessPosition tgtPos)
        {
            ChessStep chessStep = new ChessStep(Number++, movedChessman.ChessmanType, srcPos, tgtPos, action);
            this.Record.Items.Add(chessStep);
            //this.FenNotation = this.FenNotation.Move(movedChessman, action, srcPos, tgtPos);
            //注册行棋事件
            OnMoveIn(new MoveInEventArgs(action, movedChessman.GameSide, chessStep));
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
            public Enums.GameSide GameSide { get; private set; }
            public ChessStep ChessStep { get; private set; }
            public MoveInEventArgs(Enums.Action action, Enums.GameSide chessmanSide, ChessStep chessStep)
            {
                this.Action = action;
                this.GameSide = chessmanSide;
                this.ChessStep = chessStep;
            }
        }

        #endregion


    }
}