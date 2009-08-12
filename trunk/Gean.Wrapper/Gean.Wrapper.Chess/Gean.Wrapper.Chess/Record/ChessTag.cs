﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessTag : Definer
    {
        public override string ToString()
        {
            //[Date "2008.03.26"]
            //[Round "1"]
            //[White "Lou Yiping"]
            //[Black "Li Shilong"]
            //[Result "1-0"]
            StringBuilder sb = new StringBuilder();
            foreach (var item in this._definer)
            {
                sb.Append('[');
                sb.Append(item.Key).Append(' ');
                sb.Append('"');
                sb.Append((string)item.Value);
                sb.Append('"');
                sb.AppendLine("]");
            }
            return sb.ToString();
        }
    }
}