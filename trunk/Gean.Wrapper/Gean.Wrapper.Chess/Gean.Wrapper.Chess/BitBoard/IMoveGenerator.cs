using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{

    public interface IMoveGenerator
    {
        /// <summary>
        /// Generate the pseudo legal moves for a position
        /// </summary>
        /// <param name="board">The bitboard to query the possible pseudo legal moves</param>
        /// <param name="moves">An array to be filled with the resulting moves</param>
        /// <param name="startIndex">the index to start fill the moves array</param>
        /// <param name="mode">The generation mode: quiescent, captures, all</param>
        /// <returns></returns>
        int GetMoves(IBitBoard board, int[] moves, int startIndex, MoveGenerationMode mode);
        /// <summary>
        /// Check if the Enums.ChessmanSide "side" is in check with the given board
        /// </summary>
        /// <param name="board">the position</param>
        /// <param name="side">the side to check</param>
        /// <returns>returns true if the side is in check</returns>
        bool InCheck(IBitBoard board, Enums.ChessmanSide side);
        /// <summary>
        /// Check if a set of squares is in check from the point of view of Enums.ChessmanSide
        /// </summary>
        /// <param name="board">the position</param>
        /// <param name="cells">the cells to check</param>
        /// <param name="side">the side</param>
        /// <returns>return true if at least one of the cell is in check by the point of view of side</returns>
        bool InCheck(IBitBoard board, int[] cells, Enums.ChessmanSide side);
        /// <summary>
        /// Check move legality *WITHOUT CHECK CONTROL*
        /// Probe for castling validity and the move does not capture the king...
        /// This works only for move that are already prooved to be *pseudo-legal* !!!
        /// The implementation should not be keep in account moves from user interaction
        /// ( for speed reason )
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        bool IsMoveLegal(IBitBoard board, int move);
    }

    public enum MoveGenerationMode
    {
        All,
        OnlyCaptures,
        OnlyQuiescent
    }

    //public interface IMoveGeneratorOld
    /*{
        void Init();
        List<BitMoving> GetMoves(Enums.ChessmanSide c, BitBoardEngine board);
        List<BitMoving> GetMoves(Enums.ChessmanSide c, BitBoardEngine board, MoveGenerationMode mode);
        ulong GetPawnCaptures(int square, Enums.ChessmanSide color, BitBoardEngine board);
        ulong GetPawnMoves(Enums.ChessmanSide color, ulong mask, BitBoardEngine board);
        ulong GetKnightAttacks(int square, BitBoardEngine board);
        ulong GetBishopAttacks(int square, BitBoardEngine board);
        ulong GetRookAttacks(int square, BitBoardEngine board);
        ulong GetQueenAttacks(int square, BitBoardEngine board);
        ulong GetKingAttacks(int square, BitBoardEngine board);
    }*/

}
