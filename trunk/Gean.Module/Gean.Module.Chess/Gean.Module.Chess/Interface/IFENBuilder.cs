
namespace Gean.Module.Chess
{
    /// <summary>
    /// 描述一局棋的当前局面
    /// </summary>
    public interface ISituation
    {
        string PiecePlacementData { get; }
        char ActiveColor { get; set; }
        bool BlackKingCastlingAvailability { get; }
        bool BlackQueenCastlingAvailability { get; }
        bool WhiteKingCastlingAvailability { get; }
        bool WhiteQueenCastlingAvailability { get; }
        Position EnPassantTargetPosition { get; }
        int HalfMoveClock { get; }
        int FullMoveNumber { get; }

        ISituation MoveIn(Piece piece, Enums.Action action, Position srcPos, Position tgtPos);

        char this[int dot] { get; set; }
        void Clear();
        void Parse(string str);
        string ToFENString();
    }
}
