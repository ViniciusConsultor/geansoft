﻿using System;
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
    public class ChessmanRook : ChessPiece
    {
        public ChessmanRook(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Rook, side)
        {
            ChessPosition point = ChessPiece.GetOpenningsPoint(side, gridSide, 1, 8);
            this.ChessPositions.Push(point);
        }

        public ChessmanRook(Enums.ChessmanSide side, ChessPosition point)
            : base(Enums.ChessmanType.Rook, side)
        {
            this.ChessPositions.Push(point);
        }

        public override ChessPosition[] GetEnablePositions()
        {
            throw new NotImplementedException();
        }
    }
}