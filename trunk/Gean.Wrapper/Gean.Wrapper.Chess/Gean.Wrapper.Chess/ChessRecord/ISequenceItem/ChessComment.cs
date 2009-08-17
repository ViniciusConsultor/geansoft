using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋评的类
    /// </summary>
    public class ChessComment : ISequenceItem
    {
        public ChessComment()
        {
        }
        public ChessComment(string comment)
            : this("", comment)
        { }
        public ChessComment(string userId, string comment)
        {
            this.UserID = userId;
            this.Comment = comment;
        }

        public string UserID { get; set; }
        public string Comment { get; set; }

        #region ISequenceItem 成员

        public string Value
        {
            get { return this.ToString(); }
            set
            {
                ChessComment c = Parse(value);
                this.UserID = c.UserID;
                this.Comment = c.Comment;
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(' ').Append('{').Append(' ');
            if (!string.IsNullOrEmpty(this.UserID))
            {
                sb.Append('<').Append(this.UserID).Append('>');
            }
            sb.Append(this.Comment);
            sb.Append(' ').Append('}').Append(' ');
            return sb.ToString();
        }

        /// <summary>
        /// 对给定的定符串进行解析，返回一个棋局评论类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ChessComment Parse(string value)
        {
            char flag = '#';
            int number;
            string comment;
            string userId;
            Utility.ParseAppendantString(value, flag, out number, out userId, out comment);
            return new ChessComment(userId, comment);
        }

    }
}
