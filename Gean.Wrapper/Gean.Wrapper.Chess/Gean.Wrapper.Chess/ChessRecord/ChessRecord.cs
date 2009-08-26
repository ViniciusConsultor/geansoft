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
        public ChessRecord()
        {
            this.Definer = new ChessDefiner();
            this.Items = new ChessSequence();
        }

        public ChessDefiner Definer { get; set; }

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

        #region override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;
            
            ChessRecord record = obj as ChessRecord;
            if (!UtilityEquals.EnumerableEquals(this.Definer, record.Definer))
                return false;
            if (!UtilityEquals.CollectionsEquals<ISequenceItem>(this.Items, record.Items))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (
                3 * (Definer.GetHashCode() ^ Items.GetHashCode())
                );
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Definer.ToString());
            sb.AppendLine(this.Items.ToString());
            sb.AppendLine();
            return sb.ToString();
        }

        #endregion

        #region IEnumerable

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
            private int _position = -1;

            public ChessStepEnumerator(IList<ISequenceItem> list)
            {
                _chessSteps = new List<ChessStep>();
                foreach (ISequenceItem item in list)
                {
                    if (!(item is ChessStep))
                        continue;
                    _chessSteps.Add(item as ChessStep);
                }
            }

            public bool MoveNext()
            {
                _position++;
                return (_position < _chessSteps.Count);
            }

            public void Reset()
            {
                _position = -1;
            }

            public object Current
            {
                get { return _chessSteps[_position]; }
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

        #endregion
    }
}