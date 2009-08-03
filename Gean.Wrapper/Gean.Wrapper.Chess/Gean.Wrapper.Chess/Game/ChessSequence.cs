using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    
    /// <summary>
    /// 一个棋局记录中核心棋招序列
    /// </summary>
    public sealed class ChessSequence : ChessStepPairSequence
    {
        public ChessSequence()
            : base("", "", 0)
        {
        }

        public override string ToString()
        {
            return this.SequenceToString();
        }

        private int _number = 1;
        private ChessStep _tmpWhiteChessStep = null;
        private ChessStep _tmpBlackChessStep = null;

        public void Add(Enums.Action action, Enums.ChessmanSide chessmanSide, ChessStep chessStep)
        {
            if (action == Enums.Action.Opennings)
            {
                return;
            }
            if (chessmanSide == Enums.ChessmanSide.White)
            {
                this._tmpWhiteChessStep = chessStep;
            }
            else
            {
                this._tmpBlackChessStep = chessStep;
            }
            if ((_tmpBlackChessStep != null) && (_tmpWhiteChessStep != null))
            {
                this.Add(new ChessStepPair(_number, _tmpWhiteChessStep, _tmpBlackChessStep));
                _number++;
                _tmpBlackChessStep = null;
                _tmpWhiteChessStep = null;
            }

        }

        public ChessStepPair Peek()
        {
            return this.ChessStepPairs[this.ChessStepPairs.Count - 1];
        }

        public static ChessSequence Parse(string stringvalue)
        {
            ChessSequence cs = new ChessSequence();
            string regexstring = @"\b\d+.";
            Regex regex = new Regex(regexstring);
            string[] arr = regex.Split(stringvalue);
            int j = 1;
            foreach (var item in arr)
            {
                if (string.IsNullOrEmpty(item)) continue;
                cs.Add(ChessStepPair.Parse(j, item));
                j++;
            }
            return cs;
        }
    }
}
