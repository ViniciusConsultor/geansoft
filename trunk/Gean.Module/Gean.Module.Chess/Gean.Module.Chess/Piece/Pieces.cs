﻿using System.Collections;
using System.Collections.Generic;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 一个描述棋子集合的类型
    /// </summary>
    public class Pieces : ICollection<Piece>
    {
        List<Piece> _piecrs = new List<Piece>(32);

        /// <summary>
        /// 获取另一战方的“王”
        /// </summary>
        /// <param name="currSide">另一战方</param>
        /// <param name="pieces">棋子的集合</param>
        public PieceKing GetOtherGameSideKing(Enums.GameSide currSide)
        {
            if (currSide == Enums.GameSide.Black)
            {
                foreach (Piece piece in _piecrs)
                {
                    if (piece.PieceType == Enums.PieceType.WhiteKing)
                        return (PieceKing)piece;
                }
            }
            else
            {
                foreach (Piece piece in _piecrs)
                {
                    if (piece.PieceType == Enums.PieceType.BlackKing)
                        return (PieceKing)piece;
                }
            }
            throw new GameException(ExString.PieceKingIsNull);
        }

        /// <summary>
        /// 尝试从棋子集合中根据指定的棋盘位置获取棋子
        /// </summary>
        /// <param name="dot">指定的棋盘位置</param>
        /// <param name="Piece">棋子</param>
        public bool TryGetPiece(int dot, out Piece piece)
        {
            piece = Piece.Empty;
            foreach (Piece pe in _piecrs)
            {
                if (pe.Position.Dot == dot)
                {
                    piece = pe;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 尝试从棋子集合中判断是否包含指定的棋子，并获取棋盘位置
        /// </summary>
        /// <param name="Piece">指定的棋子</param>
        /// <param name="dot">棋盘位置</param>
        public bool TryContains(Piece item, out int dot)
        {
            if (_piecrs.Contains(item))
            {
                dot = item.Position.Dot;
                return true;
            }
            else
            {
                dot = 0;
                return false;
            }
        }

        /// <summary>
        /// 将指定棋子集合的元素添加到父棋子集合的末尾。
        /// </summary>
        /// <param name="items">指定棋子集合</param>
        public void AddRange(IEnumerable<Piece> items)
        {
            this._piecrs.AddRange(items);
        }
        
        #region ICollection<Piece>

        public void Add(Piece item)
        {
            this._piecrs.Add(item);
        }

        public void Clear()
        {
            _piecrs.Clear();
        }

        public bool Contains(Piece item)
        {
            return this._piecrs.Contains(item);
        }

        public void CopyTo(Piece[] array, int arrayIndex)
        {
            this._piecrs.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _piecrs.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Piece item)
        {
            return _piecrs.Remove(item);
        }

        #endregion

        #region IEnumerable<Piece> 成员

        public IEnumerator<Piece> GetEnumerator()
        {
            return _piecrs.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _piecrs.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 建立一个新棋局的默认集合
        /// </summary>
        public static Pieces OpeningPiecesCreator()
        {
            Pieces pieces = new Pieces();
            pieces.AddRange(PiecePawn.WhitePawns);
            pieces.AddRange(PiecePawn.BlackPawns);
            pieces.Add(PieceRook.Rook01);
            pieces.Add(PieceRook.Rook08);
            pieces.Add(PieceRook.Rook57);
            pieces.Add(PieceRook.Rook64);
            pieces.Add(PieceKnight.Knight02);
            pieces.Add(PieceKnight.Knight07);
            pieces.Add(PieceKnight.Knight58);
            pieces.Add(PieceKnight.Knight63);
            pieces.Add(PieceBishop.Bishop03);
            pieces.Add(PieceBishop.Bishop06);
            pieces.Add(PieceBishop.Bishop59);
            pieces.Add(PieceBishop.Bishop62);
            pieces.Add(PieceQueen.NewWhiteQueen);
            pieces.Add(PieceQueen.NewBlackQueen);
            pieces.Add(PieceKing.NewWhiteKing);
            pieces.Add(PieceKing.NewBlackKing);
            return pieces;
        }

    }
}