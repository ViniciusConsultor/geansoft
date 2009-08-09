using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一局棋的记录，该记录可能与更多的棋局记录保存在一个PGN文件中
    /// </summary>
    public class ChessRecord : IStepTree, IEnumerable<ChessStep>
    {
        #region IStepTree 成员

        public object Parent { get; set; }

        public bool HasChildren
        {
            get
            {
                if (this.Items == null) return false;
                if (this.Items.Count <= 0) return false;
                return true;
            }
        }

        public ChessSequence Items { get; set; }

        #endregion

        public ChessRecord()
        {
            this.ChessTags = new ChessTag();
            this.Items = new ChessSequence();
        }

        public ChessTag ChessTags { get; set; }

        #region override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;
            
            ChessRecord pr = obj as ChessRecord;
            if (!UtilityEquals.EnumerableEquals(this.ChessTags, pr.ChessTags))
                return false;
            if (!UtilityEquals.CollectionsEquals<ISequenceItem>(this.Items, pr.Items))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (
                3 * (ChessTags.GetHashCode() + Items.GetHashCode())
                );
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.ChessTags.ToString());
            sb.AppendLine(this.Items.ToString());
            sb.AppendLine();
            return sb.ToString();
        }

        #endregion

        #region IEnumerable<ChessStep> 成员

        public IEnumerator<ChessStep> GetEnumerator()
        {
            return new ChessStepEnumerator(this.Items);
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ChessStepEnumerator(this.Items);
        }

        #endregion

        public class ChessStepEnumerator : IEnumerator<ChessStep>
        {
            private List<ChessStep> _chessSteps;
            private int position = -1;

            public ChessStepEnumerator(IList<ISequenceItem> list)
            {
                _chessSteps = new List<ChessStep>();
                foreach (ISequenceItem item in list)
                {
                    if (!(item is ChessStepPair))
                    {
                        continue;
                    }
                    ChessStepPair pair = item as ChessStepPair;
                    if (pair.White != null)
                        _chessSteps.Add(pair.White);
                    if (pair.Black != null)
                        _chessSteps.Add(pair.Black);
                }
            }

            public bool MoveNext()
            {
                position++;
                return (position < _chessSteps.Count);
            }

            public void Reset()
            {
                position = -1;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return _chessSteps[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            #region IEnumerator<ChessStep> 成员

            ChessStep IEnumerator<ChessStep>.Current
            {
                get { return (ChessStep)this.Current; }
            }

            #endregion

            #region IDisposable 成员

            public void Dispose()
            {
                _chessSteps = null;
            }

            #endregion
        }

    }
}