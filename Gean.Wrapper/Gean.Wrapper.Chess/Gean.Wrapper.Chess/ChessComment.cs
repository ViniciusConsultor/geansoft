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
            return unchecked(17 * Number.GetHashCode() ^ UserID.GetHashCode() ^ Comment.GetHashCode());
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
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Comment cannot is NullOrEmpty!");
            if (!value.StartsWith("#"))
                throw new FormatException(value + " -> # ???");

            string[] commentArray = value.Split(new char[] { '#' }, 3, StringSplitOptions.RemoveEmptyEntries);

            if (commentArray.Length == 1)
            {
                return new ChessComment(commentArray[0], Math.Abs(value.GetHashCode() + commentArray.GetHashCode()));
            }
            int n = 0;//递增数组元素的个数
            int number = 0;
            try
            { 
                number = Convert.ToInt32(commentArray[n].Trim());
                n++;
            }
            catch//如果解析失败，给出一个int值以便正确运行
            { number = Math.Abs(value.GetHashCode() ^ commentArray.GetHashCode()); }

            string comment = "";
            if (commentArray.Length == 2)
            {
                return new ChessComment(commentArray[n], number);
            }

            string email = "";
            if (commentArray.Length == 3)
            {
                string strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";//一般规则，非严谨的邮址验证规则
                Regex r = new Regex(strExp);
                Match m = r.Match(commentArray[n]);
                if (m.Success)
                {
                    email = commentArray[n];
                }
                comment = commentArray[++n];
            }
            return new ChessComment(email, comment, number);
        }
    }
}
