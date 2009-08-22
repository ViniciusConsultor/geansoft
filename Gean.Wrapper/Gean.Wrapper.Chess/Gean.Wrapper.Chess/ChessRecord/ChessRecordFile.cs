using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一个记载有多个棋局记录文件中的数据封装类型。
    /// 该文件将被解析成一个IList&lt;ChessRecord&gt;集合,即本类型。
    /// 该类型实现了PGN解析接口。
    /// </summary>
    public class ChessRecordFile : IList<ChessRecord>, IPGNReaderEvents
    {

        private List<ChessRecord> _chessRecords = new List<ChessRecord>();

        #region IGameReaderEvents 成员

        private Stack _states;
        private string _lastNumber;
        private ChessStep _tmpStep;
        private ChessComment _tmpComment;
        private IStepTree _tmpStepTree = null;

        public void NewGame(IPGNReader iParser)
        {
            _states = new Stack();
            _tmpStepTree = new ChessRecord();
            _chessRecords.Add((ChessRecord)_tmpStepTree);
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
                    _tmpStepTree.Items = new ChessSequence();
                }
                //_tmpStepTree.Items.Add(_tmpStep);
                _tmpStep = null;
            }
            else
            {
                IStepTree tmpTree = null;
                int i = 1;
                while (!(_tmpStepTree.Items[_tmpStepTree.Items.Count - i] is IStepTree))
                {
                    i++;
                }
                tmpTree = (IStepTree)_tmpStepTree.Items[_tmpStepTree.Items.Count - i];
                tmpTree.Parent = _tmpStepTree;
                _tmpStepTree = tmpTree;
            }
            if (_tmpStepTree.Items == null)
            {
                _tmpStepTree.Items = new ChessSequence();
            }
        }

        public void ExitVariation(IPGNReader iParser)
        {
            if (iParser.State != Enums.PGNReaderState.Number)
                StepParsed(iParser);
            if (_tmpStep != null)
            {
                _tmpStepTree.Items.Add(_tmpStep);
                _tmpStep = null;
            }
            _tmpStepTree = (IStepTree)_tmpStepTree.Parent;
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
                ((ChessRecord)_tmpStepTree).Definer.Set<string>(iParser.Tag, iParser.Value);
            }
            catch (ChessRecordException e)
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
            ChessNag nag = new ChessNag(iParser.Value);
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
                _tmpStep = ChessStep.Parse(int.Parse(_lastNumber), iParser.Value, Enums.ChessmanSide.White);
                _tmpStepTree.Items.Add(_tmpStep);
            }
            else if (iParser.State == Enums.PGNReaderState.Black)
            {
                _tmpStep = ChessStep.Parse(int.Parse(_lastNumber), iParser.Value, Enums.ChessmanSide.Black);
                _tmpStepTree.Items.Add(_tmpStep);
            }
        }

        public void CommentParsed(IPGNReader iParser)
        {
            _tmpComment = ChessComment.Parse(iParser.Value);
            _tmpStepTree.Items.Add(_tmpComment);
        }

        public void EndMarker(IPGNReader iParser)
        {
            ChessResult end = ChessResult.Parse(iParser.Value);
            _tmpStepTree.Items.Add(end);
        }

        #endregion

        #region IList<ChessRecord> 成员

        public int IndexOf(ChessRecord item)
        {
            return _chessRecords.IndexOf(item);
        }

        public void Insert(int index, ChessRecord item)
        {
            _chessRecords.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _chessRecords.RemoveAt(index);
        }

        public ChessRecord this[int index]
        {
            get { return _chessRecords[index]; }
            set { _chessRecords[index] = value; }
        }

        #endregion

        #region ICollection<ChessRecord> 成员

        public void Add(ChessRecord item)
        {
            _chessRecords.Add(item);
        }

        public void Clear()
        {
            _chessRecords.Clear();
        }

        public bool Contains(ChessRecord item)
        {
            return _chessRecords.Contains(item);
        }

        public void CopyTo(ChessRecord[] array, int arrayIndex)
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

        public bool Remove(ChessRecord item)
        {
            return _chessRecords.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessRecord> 成员

        public IEnumerator<ChessRecord> GetEnumerator()
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
