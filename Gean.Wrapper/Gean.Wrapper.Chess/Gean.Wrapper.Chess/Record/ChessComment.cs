using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋评的类
    /// </summary>
    public class ChessComment : BylawItem
    {
        public ChessComment(string userId, string comment, int number)
            : base(number, userId, comment, '#')
        {
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
            return new ChessComment(userId, comment, number);
        }
    }
}
