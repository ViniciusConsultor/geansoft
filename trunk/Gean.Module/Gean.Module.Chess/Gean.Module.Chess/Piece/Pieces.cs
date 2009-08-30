using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Module.Chess
{
    public class Pieces : IEnumerable<Piece>
    {
        List<Piece> _piecrs = new List<Piece>(32);

        /// <summary>
        /// 尝试从棋子集合中根据指定的棋盘位置获取棋子
        /// </summary>
        /// <param name="dot">指定的棋盘位置</param>
        /// <param name="chessman">棋子</param>
        /// <returns></returns>
        public bool TryGetChessman(int dot, out Piece piece)
        {
            piece = Piece.Empty;
            foreach (Piece man in _piecrs)
            {
                if ((man.Position.Dot == dot) && (!man.IsCaptured))
                {
                    piece = man;
                    return true;
                }
            }
            return false;
        }

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

        public void AddRange(IEnumerable<Piece> items)
        {
            this._piecrs.AddRange(items);
        }

        public int IndexOf(Piece value)
        {
            return this._piecrs.IndexOf(value);
        }

        public void Insert(int index, Piece value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            this._piecrs.RemoveAt(index);
        }

        public Piece this[int index]
        {
            get { return this._piecrs[index]; }
            set { this._piecrs[index] = value; }
        }

        #region ICollection<Piece> 成员

        public void Add(Piece item)
        {
            this._piecrs.Add(item);
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

        IEnumerator<Piece> IEnumerable<Piece>.GetEnumerator()
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
            pieces._piecrs.TrimExcess();
            return pieces;
        }

    }
}
