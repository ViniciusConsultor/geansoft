using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子：车。
    /// In chess, a rook is one of the chess pieces which stand in the corners
    /// of the board at the beginning of a game. 
    /// Rooks can move forwards, backwards, or sideways, but not diagonally. 
    /// </summary>
    public class ChessmanRook : Chessman
    {
        internal ChessmanRook(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Rook, side)
        {
            this.ChessGrids.Add(Chessman.GetOpenningspoint(side, gridSide, 1, 8));
        }

        internal ChessmanRook(Enums.ChessmanSide side, ChessGrid point)
            : base(Enums.ChessmanType.Rook, side)
        {
            this.ChessGrids.Add(point);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "R";
        }
    }
}
