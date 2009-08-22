
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
            string side = string.Empty;
            switch (this.ChessmanSide)
            {
                case Enums.ChessmanSide.White:
                    side = "W";
                    break;
                case Enums.ChessmanSide.Black:
                    side = "B";
                    break;
                default:
                    break;
            }
            string type = Enums.ChessmanTypeToString(this.ChessmanType);
            return string.Format("{0}{1}", side, type);
        }
    }
}
