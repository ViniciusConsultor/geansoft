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
    public class ChessmanRook : ChessmanBase
    {
        public ChessmanRook(ChessboardGrid grid, Enums.ChessmanSide side)
            : base(grid, side, Enums.ChessmanType.Rook)
        {

        }

        public override void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public override string ToSimpleString()
        {
            return "R";
        }
    }
}
