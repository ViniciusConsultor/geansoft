using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessStep
    {
        public int SerialNumber { get; set; }
        public Chessman WhiteChessman { get; set; }
        public Chessman BlackChessman { get; set; }
        public ChessStepAction WhiteStepAction { get; set; }
        public ChessStepAction BlackStepAction { get; set; }

        public ChessStep(int number,
            Chessman white_chessman, Chessman black_chessman,
            ChessStepAction white_action, ChessStepAction black_action)
        {
            this.SerialNumber = number;
            this.WhiteChessman = white_chessman;
            this.BlackChessman = black_chessman;
            this.WhiteStepAction = white_action;
            this.BlackStepAction = black_action;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.SerialNumber.ToString()).Append('.');
            sb.Append(' ');
            if (this.WhiteChessman.Man != ChessmanWord.Pawn)
            {
                sb.Append(this.WhiteChessman.ToSimpleString());
            }
            sb.Append(this.WhiteStepAction.ToString());
            sb.Append(' ');
            if (this.BlackChessman.Man != ChessmanWord.Pawn)
            {
                sb.Append(this.BlackChessman.ToSimpleString());
            }
            sb.Append(this.BlackStepAction.ToString());
            sb.Append(' ');
            return sb.ToString();
        }
    }
}
