using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class Game : Situation, IGame
    {
        #region IGame 成员

        public Pieces ActivedPieces
        {
            get { throw new NotImplementedException(); }
        }

        public Pieces MovedPieces
        {
            get { throw new NotImplementedException(); }
        }

        public Piece this[int dot]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IPieceMove 成员

        public Enums.Action PieceMoveIn(Pair<Position, Position> step)
        {
            throw new NotImplementedException();
        }

        public event PieceMoveIn PieceMoveInEvent;

        public Enums.Action PieceMoveOut(Piece piece, Position pos)
        {
            throw new NotImplementedException();
        }

        public event PieceMoveOut PieceMoveOutEvent;

        #endregion

        #region IEnumerable<Piece> 成员

        public IEnumerator<Piece> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
