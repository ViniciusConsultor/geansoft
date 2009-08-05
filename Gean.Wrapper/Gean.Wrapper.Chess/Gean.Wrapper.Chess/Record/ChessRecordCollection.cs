using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    public class ChessRecordCollection : IList<ChessRecord>, IGameReaderEvents
    {
        List<ChessRecord> _chessRecords = new List<ChessRecord>();

        ChessRecord _tmpRecord = null;

        #region IList<ChessRecord> 成员

        public int IndexOf(ChessRecord item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ChessRecord item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ChessRecord this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<ChessRecord> 成员

        public void Add(ChessRecord item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ChessRecord item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ChessRecord[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ChessRecord item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<ChessRecord> 成员

        public IEnumerator<ChessRecord> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IGameReaderEvents 成员

        private Stack _states;
        private string _lastNumber;
        private ChessStepPair _tmpPair;
        private ChessComment _tmpComment;

        int num = 1;
        public void NewGame(IGameReader iParser)
        {
            _tmpRecord = new ChessRecord();
        }

        public void ExitHeader(IGameReader iParser)
        {
            Console.WriteLine("{0}.ExitHeader", num++);
            //throw new NotImplementedException();
        }

        public void EnterVariation(IGameReader iParser)
        {
            Console.WriteLine("{0}.EnterVariation", num++);
            //_states.Push(_lastNumber);
            //if (_moveStepPair != null)
            //{
            //    _currentSequence.Add(_moveStepPair);
            //    _moveStepPair = null;
            //}
            //_currentSequence = _currentSequence.AppendChild(newElement);
        }

        public void ExitVariation(IGameReader iParser)
        {
            Console.WriteLine("{0}.ExitVariation", num++);
            //if (iParser.State != Enums.GameReaderState.NUMBER)
            //    MoveParsed(iParser);
            //if (_moveStepPair != null)
            //{
            //    _currentSequence.AppendChild(_moveStepPair);
            //    _moveStepPair = null;
            //}
            //_currentSequence = _currentSequence.ParentNode;
            //_lastNumber = (string)_states.Pop();
        }

        public void Starting(IGameReader iParser)
        {
            Console.WriteLine("{0}.Starting", num++);
            //throw new NotImplementedException();
        }

        public void Finished(IGameReader iParser)
        {
            Console.WriteLine("{0}.Finished", num++);
            //throw new NotImplementedException();
        }

        public void TagParsed(IGameReader iParser)
        {
            try
            {
                _tmpRecord.Tags.Set<string>(iParser.Tag, iParser.Value);
            }
            catch (ChessRecordException e)
            {
                throw e;
            }
        }

        public void NagParsed(IGameReader iParser)
        {
            Console.WriteLine("{0}.NagParsed", num++);
            //throw new NotImplementedException();
        }

        public void MoveParsed(IGameReader iParser)
        {
            if (iParser.State == Enums.GameReaderState.Number)
            {
                if (_tmpPair == null)
                {
                    _lastNumber = iParser.Value;
                    _tmpPair = new ChessStepPair();
                    _tmpPair.Number = int.Parse(_lastNumber);
                }
            }
            else if (iParser.State == Enums.GameReaderState.White)
            {
                if (_tmpPair != null)
                    _tmpPair.White = ChessStep.Parse(iParser.Value, Enums.ChessmanSide.White);
            }
            else if (iParser.State == Enums.GameReaderState.Black)
            {
                if (_tmpPair == null)
                {
                    _tmpPair = new ChessStepPair();
                    _tmpPair.Number = int.Parse(_lastNumber);
                }
                _tmpPair.Black = ChessStep.Parse(iParser.Value, Enums.ChessmanSide.Black);
                _tmpRecord.Sequence.Add(_tmpPair);
                _tmpPair = null;
            }
        }

        public void CommentParsed(IGameReader iParser)
        {
            if (_tmpComment != null)
            {
                _tmpRecord.Sequence.Add(_tmpComment);
                _tmpComment = null;
            }
            _tmpComment = new ChessComment(iParser.Value);
            _tmpRecord.Sequence.Add(_tmpComment);
        }

        public void EndMarker(IGameReader iParser)
        {
            Console.WriteLine("{0}.EndMarker", num++);
            //throw new NotImplementedException();
        }

        #endregion

    }
}
