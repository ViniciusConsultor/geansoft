﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个记载有多个棋局记录的文件，被解析成一个ChessRecord的IList集合。即本类型。
    /// </summary>
    public class ChessRecordFile : IList<ChessRecord>, IPGNReaderEvents
    {

        private List<ChessRecord> _chessRecords = new List<ChessRecord>();

        #region IGameReaderEvents 成员

        private Stack _states;
        private string _lastNumber;
        private ChessStepPair _tmpPair;
        private ChessComment _tmpComment;
        private IStepTree _tmpTree = null;

        public void NewGame(IPGNReader iParser)
        {
            _states = new Stack();
            _tmpTree = new ChessRecord();
            _chessRecords.Add((ChessRecord)_tmpTree);
        }

        public void ExitHeader(IPGNReader iParser)
        {
        }

        public void EnterVariation(IPGNReader iParser)
        {
            _states.Push(_lastNumber);
            if (_tmpPair != null)
            {
                _tmpPair.Parent = _tmpTree;
                _tmpTree = _tmpPair;
                _tmpTree.Items.Add(_tmpPair);
                _tmpPair = null;
            }
            else
            {
                IStepTree tmpTree = null;
                int i = 1;
                while (!(_tmpTree.Items[_tmpTree.Items.Count - i] is IStepTree))
                {
                    i++;
                }
                tmpTree = (IStepTree)_tmpTree.Items[_tmpTree.Items.Count - i];
                tmpTree.Parent = _tmpTree;
                _tmpTree = tmpTree;
            }
            if (_tmpTree.Items == null)
            {
                _tmpTree.Items = new ChessSequence();
            }
        }

        public void ExitVariation(IPGNReader iParser)
        {
            if (iParser.State != Enums.PGNReaderState.Number)
                MoveParsed(iParser);
            if (_tmpPair != null)
            {
                _tmpTree.Items.Add(_tmpPair);
                _tmpPair = null;
            }
            _tmpTree = (IStepTree)_tmpTree.Parent;
            _lastNumber = (string)_states.Pop();
        }

        public void Starting(IPGNReader iParser)
        {
        }

        public void Finished(IPGNReader iParser)
        {
            if (_tmpPair != null)
            {
                _tmpTree.Items.Add(_tmpPair);
                _tmpPair = null;
            }
            _states = new Stack();
        }

        public void TagParsed(IPGNReader iParser)
        {
            try
            {
                ((ChessRecord)_tmpTree).Definer.Set<string>(iParser.Tag, iParser.Value);
            }
            catch (ChessRecordException e)
            {
                throw e;
            }
        }

        public void NagParsed(IPGNReader iParser)
        {
            if (_tmpPair != null)
            {
                _tmpTree.Items.Add(_tmpPair);
                _tmpPair = null;
            }
            ChessNag nag = new ChessNag(iParser.Value);
            _tmpTree.Items.Add(nag);
        }

        public void MoveParsed(IPGNReader iParser)
        {
            if (iParser.State == Enums.PGNReaderState.Number)
            {
                if (_tmpPair == null)
                {
                    _lastNumber = iParser.Value;
                    _tmpPair = new ChessStepPair();
                    _tmpPair.Number = int.Parse(_lastNumber);
                }
            }
            else if (iParser.State == Enums.PGNReaderState.White)
            {
                if (_tmpPair != null)
                    _tmpPair.White = ChessStep.Parse(iParser.Value, Enums.ChessmanSide.White);
            }
            else if (iParser.State == Enums.PGNReaderState.Black)
            {
                if (_tmpPair == null)
                {
                    _tmpPair = new ChessStepPair();
                    _tmpPair.Number = int.Parse(_lastNumber);
                }
                _tmpPair.Black = ChessStep.Parse(iParser.Value, Enums.ChessmanSide.Black);
                _tmpTree.Items.Add(_tmpPair);
                _tmpPair = null;
            }
        }

        public void CommentParsed(IPGNReader iParser)
        {
            _tmpComment = new ChessComment(iParser.Value);
            _tmpTree.Items.Add(_tmpComment);
        }

        public void EndMarker(IPGNReader iParser)
        {
            if (_tmpPair != null)
            {
                _tmpTree.Items.Add(_tmpPair);
                _tmpPair = null;
            }
            ChessEnd end = new ChessEnd(iParser.Value);
            _tmpTree.Items.Add(end);
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