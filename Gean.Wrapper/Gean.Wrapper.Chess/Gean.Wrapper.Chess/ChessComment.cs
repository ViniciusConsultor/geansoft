using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessComment
    {
        public string UserID { get; set; }
        public string Comment { get; set; }
        public ChessComment(string userId,string comment)
        {
            this.UserID = userId;
            this.Comment = comment;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("//");
            sb.Append(this.UserID);
            sb.Append(" : ");
            sb.AppendLine(this.Comment);
            return sb.ToString();
        }
    }
}
