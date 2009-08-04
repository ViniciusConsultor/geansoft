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
        private ChessSequence _currentSequence;
        private ChessStepPair _moveStepPair;

        int num = 1;
        public void NewGame(IGameReader iParser)
        {
            Console.WriteLine("{0}.NewGame", num++);
            //_tmpRecord = new ChessRecord();
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
            Console.WriteLine("{0}.TagParsed", num++);
            //throw new NotImplementedException();
        }

        public void NagParsed(IGameReader iParser)
        {
            Console.WriteLine("{0}.NagParsed", num++);
            //throw new NotImplementedException();
        }

        public void MoveParsed(IGameReader iParser)
        {
            Console.WriteLine("{0}.MoveParsed", num++);
            //if (iParser.State == Enums.GameReaderState.NUMBER)
            //{
            //    if (_moveStepPair == null)
            //    {
            //        _moveStepPair = GameDOM.CreateElement("MOVE");
            //        coLastNumber = iParser.Value;
            //        _moveStepPair.SetAttribute("number", coLastNumber);
            //    }
            //}
            //else if (iParser.State == Enums.GameReaderState.WHITE)
            //{
            //    if (_moveStepPair != null)
            //        _moveStepPair.SetAttribute("white", iParser.Value);
            //}
            //else if (iParser.State == Enums.GameReaderState.BLACK)
            //{
            //    if (_moveStepPair == null)
            //    {
            //        _moveStepPair = GameDOM.CreateElement("MOVE");
            //        _moveStepPair.SetAttribute("number", coLastNumber);
            //    }
            //    _moveStepPair.SetAttribute("black", iParser.Value);
            //    _currentSequence.AppendChild(_moveStepPair);
            //    _moveStepPair = null;
            //}
        }

        public void CommentParsed(IGameReader iParser)
        {
            Console.WriteLine("{0}.CommentParsed", num++);
            //throw new NotImplementedException();
        }

        public void EndMarker(IGameReader iParser)
        {
            Console.WriteLine("{0}.EndMarker", num++);
            //throw new NotImplementedException();
        }

        #endregion

    }
}
