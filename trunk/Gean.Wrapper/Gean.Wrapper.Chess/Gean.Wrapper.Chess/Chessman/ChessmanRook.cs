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
        public ChessmanRook(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Rook, side)
        {
            ChessGrid rid = Chessman.GetOpenningsGrid(side, gridSide, 1, 8);
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public ChessmanRook(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Rook, side)
        {
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
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
