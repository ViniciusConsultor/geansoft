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
        public ChessTag Tags { get; internal set; }
        public ChessSequence Sequence { get; internal set; }

        public ChessRecord()
        {
            this.Tags = new ChessTag();
            this.Sequence = new ChessSequence();
        }

        #region override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;
            
            ChessRecord pr = obj as ChessRecord;
            if (!UtilityEquals.EnumerableEquals(this.Tags, pr.Tags))
                return false;
            if (!UtilityEquals.CollectionsEquals<ChessStepPair>(this.Sequence, pr.Sequence))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (
                3 * (Tags.GetHashCode() + Sequence.GetHashCode())
                );
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Tags.ToString());
            sb.AppendLine();
            sb.AppendLine(this.Sequence.ToString());
            sb.AppendLine().AppendLine();
            return sb.ToString();
        }

        #endregion
    }
}