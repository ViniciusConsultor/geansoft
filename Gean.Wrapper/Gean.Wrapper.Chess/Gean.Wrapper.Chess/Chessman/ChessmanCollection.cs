using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    public class ChessmanCollection : FENBuilder, IEnumerable<Chessman>
    {
        List<Chessman> _chessmans = new List<Chessman>(32);

        /// <summary>
        /// 尝试从棋子集合中根据指定的棋盘位置获取棋子
        /// </summary>
        /// <param name="dot">指定的棋盘位置</param>
        /// <param name="chessman">棋子</param>
        /// <returns></returns>
        public bool TryGetChessman(int dot, out Chessman chessman)
        {
            chessman = Chessman.Empty;
            foreach (Chessman man in _chessmans)
            {
                if ((man.CurrPosition.Dot == dot) && (!man.IsCaptured))
                {
                    chessman = man;
                    return true;
                }
            }
            return false;
        }

        public bool TryContains(Chessman item, out int dot)
        {
            if (_chessmans.Contains(item))
            {
                dot = item.CurrPosition.Dot;
                return true;
            }
            else
            {
                dot = 0;
                return false;
            }
        }

        public void AddRange(IEnumerable<Chessman> items)
        {
            this._chessmans.AddRange(items);
        }

        public int IndexOf(Chessman value)
        {
            return this._chessmans.IndexOf(value);
        }

        public void Insert(int index, Chessman value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            this._chessmans.RemoveAt(index);
        }

        public Chessman this[int index]
        {
            get { return this._chessmans[index]; }
            set { this._chessmans[index] = value; }
        }

        #region ICollection<Chessman> 成员

        public void Add(Chessman item)
        {
            this._chessmans.Add(item);
        }

        public bool Contains(Chessman item)
        {
            return this._chessmans.Contains(item);
        }

        public void CopyTo(Chessman[] array, int arrayIndex)
        {
            this._chessmans.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _chessmans.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Chessman item)
        {
            return _chessmans.Remove(item);
        }

        #endregion

        #region IEnumerable<Chessman> 成员

        public IEnumerator<Chessman> GetEnumerator()
        {
            return _chessmans.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _chessmans.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 建立一个新棋局的默认集合
        /// </summary>
        public static ChessmanCollection OpeningChessmansCreator()
        {
            ChessmanCollection chessmans = new ChessmanCollection();
            chessmans.AddRange(ChessmanPawn.WhitePawns);
            chessmans.AddRange(ChessmanPawn.BlackPawns);
            chessmans.Add(ChessmanRook.Rook01);
            chessmans.Add(ChessmanRook.Rook08);
            chessmans.Add(ChessmanRook.Rook57);
            chessmans.Add(ChessmanRook.Rook64);
            chessmans.Add(ChessmanKnight.Knight02);
            chessmans.Add(ChessmanKnight.Knight07);
            chessmans.Add(ChessmanKnight.Knight58);
            chessmans.Add(ChessmanKnight.Knight63);
            chessmans.Add(ChessmanBishop.Bishop03);
            chessmans.Add(ChessmanBishop.Bishop06);
            chessmans.Add(ChessmanBishop.Bishop59);
            chessmans.Add(ChessmanBishop.Bishop62);
            chessmans.Add(ChessmanQueen.NewWhiteQueen);
            chessmans.Add(ChessmanQueen.NewBlackQueen);
            chessmans.Add(ChessmanKing.NewWhiteKing);
            chessmans.Add(ChessmanKing.NewBlackKing);
            chessmans._chessmans.TrimExcess();
            return chessmans;
        }
    }
}
