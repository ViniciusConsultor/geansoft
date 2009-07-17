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
        public Chessman Chessman { get; set; }
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

        public ChessStep(Chessman chessman, ChessboardGrid grid)
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
            sb.AppendLine();
            return sb.ToString();
        }

        public static bool Parse(string str, out Square square, out Enums.ChessmanType manType)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                throw new ArgumentOutOfRangeException(str);

            square = new Square();
            manType = Enums.ChessmanType.Nothing;
            if (!char.IsUpper(str, 0))
            {
                manType = Enums.ParseChessmanType(str[0]);
            }
            else
            {

            }
            return true;
        }

    }
}

/*
正式的记谱法是先写棋子的字母.再写由哪格走到哪格.
例如Ng1-f3.表示马由g1格走到f3格.也有简略记谱法是只写目的地.没有来源地.
如Rd3.表示有车走到d3格.当有两个同样棋子可以到达同一个目的地时.则写出来源地的行或者列.如Rad3.
如果吃掉对方的棋子.则在两个位置之间加上x.如Bb5xc6或Bxc6. 
*/