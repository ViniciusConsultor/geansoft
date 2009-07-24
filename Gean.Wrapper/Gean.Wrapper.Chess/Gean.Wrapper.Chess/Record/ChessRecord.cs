using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一局棋的记录，该记录可能与更多的棋局记录保存在一个PGN文件中
    /// </summary>
    public class ChessRecord
    {
        public Definer Definer { get; internal set; }
        public ChessStepPairSequence Sequence { get; internal set; }
        public ChessCommentCollection Comments { get; internal set; }
        public ChessChoicesCollection Choices { get; internal set; }

        public ChessRecord()
        {
            this.Definer = new Definer();
            this.Sequence = new ChessStepPairSequence();
            this.Comments = new ChessCommentCollection();
            this.Choices = new ChessChoicesCollection();
        }

        public override bool Equals(object obj)
        {
            ChessRecord pr = obj as ChessRecord;
            if (!UtilityEquals.EnumerableEquals(this.Definer, pr.Definer))
                return false;
            if (!UtilityEquals.CollectionsEquals<ChessStepPair>(this.Sequence, pr.Sequence))
                return false;
            if (!UtilityEquals.CollectionsEquals<BylawItem>(this.Comments, pr.Comments))
                return false;
            if (!UtilityEquals.CollectionsEquals<BylawItem>(this.Choices, pr.Choices))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (
                3 * (Definer.GetHashCode() + Sequence.GetHashCode() + Comments.GetHashCode() + Choices.GetHashCode())
                );
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Definer.ToString());
            sb.AppendLine();
            sb.AppendLine(this.Sequence.ToString());
            sb.AppendLine();
            sb.AppendLine(this.Comments.ToString());
            sb.AppendLine(this.Choices.ToString());
            sb.AppendLine().AppendLine();
            return sb.ToString();
        }

    }
}