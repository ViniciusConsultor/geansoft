using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一局棋的记录，该记录可能与更多的棋局记录保存在一个PGN文件中
    /// </summary>
    public class ChessRecord : IGameReaderEvents
    {
        public Definer Definer { get; internal set; }
        public ChessMainSequence Sequence { get; internal set; }
        public ChessCommentCollection Comments { get; internal set; }
        public ChessChoicesCollection Choices { get; internal set; }

        public ChessRecord()
        {
            this.Definer = new Definer();
            this.Sequence = new ChessMainSequence();
            this.Comments = new ChessCommentCollection();
            this.Choices = new ChessChoicesCollection();
        }

        #region override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;
            
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

        #endregion

        #region IGameReaderEvents 成员

        public void NewGame(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void ExitHeader(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void EnterVariation(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void ExitVariation(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void Starting(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void Finished(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void TagParsed(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void NagParsed(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void MoveParsed(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void CommentParsed(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        public void EndMarker(IGameReader iParser)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}