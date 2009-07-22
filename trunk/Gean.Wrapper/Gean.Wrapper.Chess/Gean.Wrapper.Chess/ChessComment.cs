using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋评的类
    /// </summary>
    public class ChessComment
    {
        public ChessComment(string comment, int number) : this(string.Empty, comment, number) { }
        public ChessComment(string userId, string comment, int number)
        {
            this.UserID = userId;
            this.Comment = comment;
            this.Number = number;
        }

        /// <summary>
        /// 获取与设置该棋评在本棋局记录中的编号
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 获取与设置该棋评的作者的ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 获取与设置该棋评的正文
        /// </summary>
        public string Comment { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#");
            sb.Append(this.Number.ToString());
            sb.Append("#");
            if (!string.IsNullOrEmpty(this.UserID))
            {
                sb.Append(this.UserID);
                sb.Append("#");
            }
            sb.AppendLine(this.Comment);
            return sb.ToString();
        }
        public override int GetHashCode()
        {
            return unchecked(3 * Number.GetHashCode() + UserID.GetHashCode() + Comment.GetHashCode());
        }
        public override bool Equals(object obj)
        {
            ChessComment cc = (ChessComment)obj;
            if (!cc.Number.Equals(this.Number))
                return false;
            if (!cc.Comment.Equals(this.Comment))
                return false;
            if (!cc.UserID.Equals(this.UserID))
                return false;
            return true;
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
            string email;
            Utility.ParseAppendantString(value, flag, out number, out email, out comment);
            return new ChessComment(email, comment, number);
        }

    }
}
