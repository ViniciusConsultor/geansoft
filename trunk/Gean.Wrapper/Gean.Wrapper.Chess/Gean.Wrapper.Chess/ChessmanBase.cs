using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子抽象类(王,后,车,象,马,兵)
    /// </summary>
    public abstract class ChessmanBase : IChessman
    {
        /// <summary>
        /// 该棋子类型的构造函数
        /// </summary>
        /// <param name="item">棋子的具体表现</param>
        /// <param name="type">黑棋或是白棋</param>
        public ChessmanBase(Enums.ChessmanItem item, Enums.ChessmanType type)
        {
            this._chessmanItem = item;
            this._chessmanType = type;
        }

        public override string ToString()
        {
            return this._chessmanItem.ToString();
        }
        public string ToSimpleString()
        {
            switch (this._chessmanItem)
            {
                case Enums.ChessmanItem.Rook:
                    return "R";
                case Enums.ChessmanItem.Knight:
                    return "N";
                case Enums.ChessmanItem.Bishop:
                    return "B";
                case Enums.ChessmanItem.Queen:
                    return "Q";
                case Enums.ChessmanItem.King:
                    return "K";
                case Enums.ChessmanItem.Pawn:
                    return "P";
                default:
                    Debug.Fail(this._chessmanItem.ToString());
                    return string.Empty;
            }
        }

        public Enums.ChessmanType ChessmanType
        {
            get { return this._chessmanType; }
        }
        private Enums.ChessmanType _chessmanType;

        public Enums.ChessmanItem ChessmanItem
        {
            get { return this._chessmanItem; }
        }
        private Enums.ChessmanItem _chessmanItem;

        public virtual bool MoveToGrid(ChessboardGrid grid)
        {
            throw new NotImplementedException();
        }

        public void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public virtual void Remove()
        {
            throw new NotImplementedException();
        }
    }

}
