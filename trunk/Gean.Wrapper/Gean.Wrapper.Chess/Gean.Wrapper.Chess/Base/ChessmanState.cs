
namespace Gean.Wrapper.Chess
{
    public class ChessmanState
    {
        public Enums.ChessmanSide ChessmanSide { get; private set; }
        public Enums.ChessmanType ChessmanType { get; private set; }

        public ChessmanState(Enums.ChessmanSide chessmanSide, Enums.ChessmanType chessmanType)
        {
            this.ChessmanSide = chessmanSide;
            this.ChessmanType = chessmanType;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            ChessmanState state = (ChessmanState)obj;
            if (!this.ChessmanType.Equals(state.ChessmanType))
                return false;
            if (!this.ChessmanSide.Equals(state.ChessmanSide))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (this.ChessmanSide.GetHashCode() + this.ChessmanType.GetHashCode()));
        }
        public override string ToString()
        {
            return string.Format("Side: {0} , Type: {1}", this.ChessmanSide.ToString(), this.ChessmanType.ToString());
        }

    }
}
