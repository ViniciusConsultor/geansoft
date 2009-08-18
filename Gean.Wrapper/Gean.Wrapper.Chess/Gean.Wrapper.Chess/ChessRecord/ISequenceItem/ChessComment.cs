using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Gean.Resources;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋评的类
    /// </summary>
    public class ChessComment : ISequenceItem
    {
        public string UserID { get; set; }
        public string Comment { get; set; }

        public ChessComment()
        {
            this.UserID = "";
            this.Comment = "";
        }

        #region ISequenceItem 成员

        public string Value
        {
            get { return this.ToString(); }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" { ");
            if (!string.IsNullOrEmpty(this.UserID))
            {
                sb.AppendFormat("<{0}> ", this.UserID);
            }
            sb.Append(this.Comment).Append(" } ");
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessComment comment = (ChessComment)obj;
            if (!(this.UserID.Equals(comment.UserID))) return false;
            if (!(this.Comment.Equals(comment.Comment))) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (this.Comment.GetHashCode() + this.UserID.GetHashCode()));
        }

        /// <summary>
        /// 对给定的定符串进行解析，返回一个棋局评论类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ChessComment Parse(string value)
        {
            ChessComment comment = new ChessComment();
            value = value.Trim().Trim(new char[] { '{', '}' }).Trim();
            if (value[0] == '<')
            {
                if (value.IndexOf('>') > 1)
                {
                    int index = value.IndexOf('>') - 1;
                    string user = value.Substring(1, index).Trim();
                    Regex r = new Regex(RegexString.RegexStr_SimpleEmail);
                    Match m = r.Match(user);
                    if (m.Success)
                    {
                        comment.UserID = user;
                        value = value.Substring(index+2).Trim();
                    }
                }
            }
            comment.Comment = value;
            return comment;
        }

    }
}
