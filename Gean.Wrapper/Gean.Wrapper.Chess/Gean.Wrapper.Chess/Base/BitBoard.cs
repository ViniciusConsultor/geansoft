using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    using BitBoard = System.Int64;

    public class BitBoardWrapper
    {
        public readonly static BitBoard WP = 1;
        public readonly static BitBoard WN = 2;
        public readonly static BitBoard WB = 3;
        public readonly static BitBoard WR = 4;
        public readonly static BitBoard WQ = 5;
        public readonly static BitBoard WK = 6;
        public readonly static BitBoard BP = 7;
        public readonly static BitBoard BN = 8;
        public readonly static BitBoard BB = 9;
        public readonly static BitBoard BR = 10;
        public readonly static BitBoard BQ = 11;
        public readonly static BitBoard BK = 12;
        public readonly static BitBoard NOT_EMPTY = WP | WN | WB | WR | WQ | WK | BP | BN | BB | BR | BQ | BK;
        public readonly static BitBoard EMPTY = ~NOT_EMPTY;

        //public readonly static BitBoard PAWN_ATTACKS = ((WHITE_PAWN << 7) & ~RANK_A) & ((WHITE_PAWN << 9) & ~RANK_H);


    }
}
