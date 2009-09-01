using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 描述一个记载有多个棋局记录文件中的数据封装类型。
    /// 该文件将被解析成一个IList&lt;Record&gt;集合,即本类型。
    /// 该类型实现了PGN解析接口。
    /// </summary>
    public class RecordFile : IList<Record>, IPGNReaderEvents
    {

        private List<Record> _chessRecords = new List<Record>();

        #region IGameReaderEvents 成员

        private Stack _states;
        private string _lastNumber;
        private Step _tmpStep;
        private Annotation _tmpComment;
        private ITree _tmpStepTree = null;

        public void NewGame(IPGNReader iParser)
        {
            _states = new Stack();
            _tmpStepTree = new Record();
            _chessRecords.Add((Record)_tmpStepTree);
        }

        public void ExitHeader(IPGNReader iParser)
        {
        }

        public void EnterVariation(IPGNReader iParser)
        {
            _states.Push(_lastNumber);
            if (_tmpStep != null)
            {
                _tmpStep.Parent = _tmpStepTree;
                _tmpStepTree = _tmpStep;
                if (_tmpStepTree.Items == null)
                {
                    _tmpStepTree.Items = new Variation();
                }
                //_tmpStepTree.Items.Add(_tmpStep);
                _tmpStep = null;
            }
            else
            {
                ITree tmpTree = null;
                int i = 1;
                while (!(_tmpStepTree.Items[_tmpStepTree.Items.Count - i] is ITree))
                {
                    i++;
                }
                tmpTree = (ITree)_tmpStepTree.Items[_tmpStepTree.Items.Count - i];
                tmpTree.Parent = _tmpStepTree;
                _tmpStepTree = tmpTree;
            }
            if (_tmpStepTree.Items == null)
            {
                _tmpStepTree.Items = new Steps();
            }
        }

        public void ExitVariation(IPGNReader iParser)
        {
            if (iParser.State != Enums.PGNReaderState.Number)
                StepParsed(iParser);
            _tmpStepTree = (ITree)_tmpStepTree.Parent;
            _lastNumber = (string)_states.Pop();
        }

        public void Starting(IPGNReader iParser)
        {
        }

        public void Finished(IPGNReader iParser)
        {
            _states = new Stack();
        }

        public void TagParsed(IPGNReader iParser)
        {
            try
            {
                ((Record)_tmpStepTree).Tags.Set<string>(iParser.Tag, iParser.Value);
            }
            catch (RecordException e)
            {
                throw e;
            }
        }

        public void NagParsed(IPGNReader iParser)
        {
            if (_tmpStep != null)
            {
                _tmpStepTree.Items.Add(_tmpStep);
                _tmpStep = null;
            }
            Nag nag = new Nag(iParser.Value);
            _tmpStepTree.Items.Add(nag);
        }

        public void StepParsed(IPGNReader iParser)
        {
            if (iParser.State == Enums.PGNReaderState.Number)
            {
                _lastNumber = iParser.Value;
            }
            else if (iParser.State == Enums.PGNReaderState.White)
            {
                _tmpStep = Step.Parse(int.Parse(_lastNumber), iParser.Value, Enums.GameSide.White);
                _tmpStepTree.Items.Add(_tmpStep);
            }
            else if (iParser.State == Enums.PGNReaderState.Black)
            {
                _tmpStep = Step.Parse(int.Parse(_lastNumber), iParser.Value, Enums.GameSide.Black);
                _tmpStepTree.Items.Add(_tmpStep);
            }
        }

        public void CommentParsed(IPGNReader iParser)
        {
            _tmpComment = Annotation.Parse(iParser.Value);
            _tmpStepTree.Items.Add(_tmpComment);
        }

        public void EndMarker(IPGNReader iParser)
        {
            GameResult end = GameResult.Parse(iParser.Value);
            _tmpStepTree.Items.Add(end);
        }

        #endregion

        #region IList<Record> 成员

        public int IndexOf(Record item)
        {
            return _chessRecords.IndexOf(item);
        }

        public void Insert(int index, Record item)
        {
            _chessRecords.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _chessRecords.RemoveAt(index);
        }

        public Record this[int index]
        {
            get { return _chessRecords[index]; }
            set { _chessRecords[index] = value; }
        }

        #endregion

        #region ICollection<Record> 成员

        public void Add(Record item)
        {
            _chessRecords.Add(item);
        }

        public void Clear()
        {
            _chessRecords.Clear();
        }

        public bool Contains(Record item)
        {
            return _chessRecords.Contains(item);
        }

        public void CopyTo(Record[] array, int arrayIndex)
        {
            _chessRecords.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _chessRecords.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Record item)
        {
            return _chessRecords.Remove(item);
        }

        #endregion

        #region IEnumerable<Record> 成员

        public IEnumerator<Record> GetEnumerator()
        {
            return _chessRecords.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _chessRecords.GetEnumerator();
        }

        #endregion

    }
}
