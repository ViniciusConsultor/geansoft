﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋局结果
    /// </summary>
    public class ChessResult : ISequenceItem
    {
        public Enums.Result Result 
        {
            get { return this._result; }
            internal set
            {
                _result = value;
                this.Value = Enums.ResultToString(value);
            }
        }
        private Enums.Result _result;

        /// <summary>
        /// 1-0
        /// </summary>
        public readonly static string ResultWhiteWin = "1-0";
        /// <summary>
        /// 0-1
        /// </summary>
        public readonly static string ResultBlackWin = "0-1";
        /// <summary>
        /// 1/2-1/2
        /// </summary>
        public readonly static string ResultDraw1 = "1/2-1/2";
        /// <summary>
        /// 1-1
        /// </summary>
        public readonly static string ResultDraw2 = "1-1";
        /// <summary>
        /// ½-½
        /// </summary>
        public readonly static string ResultDraw3 = "½-½";
        /// <summary>
        /// UnKnown,?,
        /// </summary>
        public readonly static string ResultUnKnown = "?";

        public string Value { get; private set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Value))
                return "";
            return this.Value;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessResult r = (ChessResult)obj;
            return Result.Equals(r.Result);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static ChessResult Parse(string value)
        {
            ChessResult result = new ChessResult();
            if (string.IsNullOrEmpty(value))
            {
                result.Result = Enums.Result.UnKnown;
            }
            else
            {
                result.Result = Enums.ToResult(value);
            }
            return result;
        }
    }
}