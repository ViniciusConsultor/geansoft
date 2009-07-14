using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋局中的单方的一步棋。如："Nc6"代表马走到c6格。
    /// 两方各一步棋组成一个棋招（<see>ChessStepPair</see>）。
    /// 对于这步棋，绑定了一个注释的集合，一个变招的集合（变招也是每一步棋的集合）。
    /// </summary>
    public class ChessStep
    {
        /// <summary>
        /// 获取或设置该步棋的棋子
        /// </summary>
        public ChessmanBase Chessman { get; set; }
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public ChessboardGrid TargetGrid { get; set; }

        /// <summary>
        /// 获取或设置该步棋的注释集合
        /// </summary>
        public ChessCommentCollection Comments { get; set; }
        /// <summary>
        /// 获取或设置该步棋的变招集合
        /// </summary>
        public ChessStepPairSequenceCollection ChoiceSteps { get; set; }

        public ChessStep(ChessmanBase chessman, ChessboardGrid grid)
        {
            this.Comments = new ChessCommentCollection();
            this.ChoiceSteps = new ChessStepPairSequenceCollection();
            this.Chessman = chessman;
            this.TargetGrid = grid;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Chessman.ChessmanType != Enums.ChessmanType.Pawn)//如果是“兵”，不打印
            {
                sb.Append(this.Chessman.ToSimpleString());
            }
            sb.Append(this.TargetGrid.ToString());
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
                foreach (ChessStepPairSequence step in this.ChoiceSteps)
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
