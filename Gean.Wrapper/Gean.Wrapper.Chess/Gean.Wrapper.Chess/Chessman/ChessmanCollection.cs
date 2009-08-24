using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    public class ChessmanCollection : ICollection<Chessman>
    {
        List<Chessman> Chessmans = new List<Chessman>(32);

        public bool TryContains(Chessman item, out int dot)
        {
            if (Chessmans.Contains(item))
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
            this.Chessmans.AddRange(items);
        }

        public void Clear()
        {
            Chessmans.Clear();
        }

        public int IndexOf(Chessman value)
        {
            return this.Chessmans.IndexOf(value);
        }

        public void Insert(int index, Chessman value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            this.Chessmans.RemoveAt(index);
        }

        public Chessman this[int index]
        {
            get { return this.Chessmans[index]; }
            set { this.Chessmans[index] = value; }
        }

        #region ICollection<Chessman> 成员

        public void Add(Chessman item)
        {
            this.Chessmans.Add(item);
        }

        public bool Contains(Chessman item)
        {
            return this.Chessmans.Contains(item);
        }

        public void CopyTo(Chessman[] array, int arrayIndex)
        {
            this.Chessmans.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Chessmans.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Chessman item)
        {
            return Chessmans.Remove(item);
        }

        #endregion

        #region IEnumerable<Chessman> 成员

        public IEnumerator<Chessman> GetEnumerator()
        {
            return Chessmans.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Chessmans.GetEnumerator();
        }

        #endregion

        public static ChessmanCollection OpeningChessmans
        {
            get 
            {
                if (_openingChessmans == null)
                {
                    _openingChessmans = ChessmanCollection.ChessmansCreator(_openingChessmans);
                }
                return _openingChessmans;
            }
        }
        private static ChessmanCollection _openingChessmans = new ChessmanCollection();
        public static ChessmanCollection ChessmansCreator(ChessmanCollection chessmans)
        {
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
            chessmans.Add(ChessmanQueen.WhiteQueen);
            chessmans.Add(ChessmanQueen.BlackQueen);
            chessmans.Add(ChessmanKing.WhiteKing);
            chessmans.Add(ChessmanKing.BlackKing);

            return chessmans;
        }
    }
}
