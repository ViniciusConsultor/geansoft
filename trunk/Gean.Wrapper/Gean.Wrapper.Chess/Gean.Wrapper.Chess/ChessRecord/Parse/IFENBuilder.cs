
namespace Gean.Wrapper.Chess
{
    interface IFENBuilder
    {
        string PiecePlacementData { get; }
        char ActiveColor { get; set; }
        bool BlackKingCastlingAvailability { get; }
        bool BlackQueenCastlingAvailability { get; }
        bool WhiteKingCastlingAvailability { get; }
        bool WhiteQueenCastlingAvailability { get; }
        ChessPosition EnPassantTargetPosition { get; }
        int HalfMoveClock { get; }
        int FullMoveNumber { get; }
        FENBuilder MoveIn(Chessman chessman, Enums.Action action, ChessPosition srcPos, ChessPosition tgtPos);
        char this[int dot] { get; set; }
        void Clear();
        void Parse(string str);
        string ToFENString();
    }
}
