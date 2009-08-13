using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gean.Wrapper.Chess
{

    public interface IBitBoard
    {
        unsafe ulong* RawData { get; }
        int Flags { get; }
        void DoMove(int move);
        void UndoMove(int move);
        Enums.ChessmanSide ToMove { get; }
        ulong GetPawns(Enums.ChessmanSide s);
        ulong GetKnights(Enums.ChessmanSide s);
        ulong GetBishops(Enums.ChessmanSide s);
        ulong GetRooks(Enums.ChessmanSide s);
        ulong GetQueens(Enums.ChessmanSide s);
        ulong GetKing(Enums.ChessmanSide s);
        void SetBoard(string fen);
        string SavePos();
        void Dump(TextWriter tw);
    }

}
