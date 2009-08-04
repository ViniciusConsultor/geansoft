using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    
    /// <summary>
    /// 一个棋局记录中核心棋招序列
    /// </summary>
    public sealed class ChessMainSequence : ChessChoicesSequence
    {
        public ChessMainSequence()
            : base("", "", 0)
        {
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

        public override string ToString()
        {
            return this.SequenceToString();
        }

        public static ChessMainSequence Parse(string value)
        {
            ChessMainSequence mainSequence = new ChessMainSequence();
            string regexstr = @"\b\d+.";
            Regex regex = new Regex(regexstr);
            string[] strArray = regex.Split(value);
            int j = 1;
            foreach (string item in strArray)
            {
                if (string.IsNullOrEmpty(item)) 
                    continue;
                mainSequence.Add(ChessStepPair.Parse(j, item));
                j++;
            }
            return mainSequence;
        }
    }
}
