using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋局中的单方的一步棋。
    /// 对于这步棋，绑定了一个注释的集合，一个变招的集合（变招也是每一步棋的集合）。
    /// </summary>
    public class ChessStep
    {
        public Chessman Chessman { get; set; }
        public ChessStepAction StepAction { get; set; }
        public ChessCommentCollection Comments { get; set; }
        public ChessStepSequenceCollection ChoiceSteps { get; set; }

        public ChessStep(Chessman chessman, ChessStepAction action)
        {
            this.Comments = new ChessCommentCollection();
            this.ChoiceSteps = new ChessStepSequenceCollection();
            this.Chessman = chessman;
            this.StepAction = action;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Chessman.Man != ChessmanWord.Pawn)
            {
                sb.Append(this.Chessman.ToSimpleString());
            }
            sb.Append(this.StepAction.ToString());
            sb.Append(' ');
            if (this.Comments.Count > 0)//如果有注释，打印注释
            {
                sb.Append("(");
                foreach (ChessComment comment in this.Comments)
                {
                    sb.Append(comment.ToString()).Append(' ');
                }
                sb.Remove(sb.Length - 1, 1).Append(")");
            }
            sb.Append(' ');
            if (this.ChoiceSteps.Count > 0)//如果有变招，打印变招字符串
            {
                sb.Append("{");
                foreach (ChessStepSequence step in this.ChoiceSteps)
                {
                    sb.Append(step.ToString()).Append(' ');
                }
                sb.Remove(sb.Length - 1, 1).Append("}");
            }
            sb.Append(' ');
            return sb.ToString();
        }
    }
}
