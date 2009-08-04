using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    static class Utility
    {
        public const int TOP = 8;
        public const int FOOTER = 1;
        public const int LEFT = 1;
        public const int RIGHT = 8;

        /// <summary>
        /// 将指定的棋盘横坐标字符转换成整型数字
        /// </summary>
        /// <param name="c">指定的棋盘横坐标字符</param>
        /// <returns>
        /// 如果返回值为-1，则输入值是非棋盘横坐标字符。
        /// 坐标值遵循象棋规则将从1开始。
        /// </returns>
        static public int CharToInt(char c)
        {
            if (char.IsUpper(c))
            {
                string str = Convert.ToString(c);
                str = str.ToLowerInvariant();
                c = Convert.ToChar(str);
            }
            switch (c)
            {
                #region case
                case 'a':
                    return 1;
                case 'b':
                    return 2;
                case 'c':
                    return 3;
                case 'd':
                    return 4;
                case 'e':
                    return 5;
                case 'f':
                    return 6;
                case 'g':
                    return 7;
                case 'h':
                    return 8;
                default:
                    return -1;
                #endregion
            }
        }

        /// <summary>
        /// 将指定的棋盘横坐标字符转换成整型数字
        /// </summary>
        /// <param name="str">指定的棋盘横坐标字符</param>
        /// <returns>
        /// 如果返回值为-1，则输入值是非棋盘横坐标字符。
        /// 坐标值遵循象棋规则将从1开始。
        /// </returns>
        static public int StringToInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            if (str.Length > 1)
            {
                return -1;
            }
            return Utility.CharToInt(Convert.ToChar(str.ToLowerInvariant()));
        }

        /// <summary>
        /// 将指定的坐标值转换成字符
        /// </summary>
        static public char IntToChar(int i)
        {
            if (i >= 1 && i <= 8)
            {
                #region switch
                switch (i)
                {
                    case 1:
                        return 'a';
                    case 2:
                        return 'b';
                    case 3:
                        return 'c';
                    case 4:
                        return 'd';
                    case 5:
                        return 'e';
                    case 6:
                        return 'f';
                    case 7:
                        return 'g';
                    case 8:
                        return 'h';
                    default:
                        return '*';
                }
                #endregion
            }
            return '*';
        }

        /// <summary>
        /// 将指定的坐标值转换成字符
        /// </summary>
        static public string IntToString(int i)
        {
            return Utility.IntToChar(i).ToString();
        }

        /// <summary>
        /// 解析棋局记录中的附属字符串：评论，变招等
        /// </summary>
        /// <param name="value">附属字符串</param>
        /// <param name="flag">用于表明字符串类型的标记符，评论是#，变招是%</param>
        /// <param name="number">编号</param>
        /// <param name="username">用户名，应是一个有效的邮件地址</param>
        /// <param name="record">实体字符串</param>
        static public void ParseAppendantString(string value, char flag, out int number, out string username, out string record)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Comment cannot is NullOrEmpty!");
            if (!value.StartsWith(flag.ToString()))
                throw new FormatException(value + " -> " + flag + " ???");

            string[] commentArray = value.Split(new char[] { flag }, 3, StringSplitOptions.RemoveEmptyEntries);

            number = 0;
            record = "";
            username = "";

            if (commentArray.Length > 1)
            {
                try
                {
                    number = Convert.ToInt32(commentArray[0].Trim());
                }
                catch//如果解析失败，给出一个int值以便正确运行
                {
                    number = Math.Abs(value.GetHashCode() ^ commentArray.GetHashCode());
                }
            }
            switch (commentArray.Length)
            {
                case 1:
                    {
                        number = Math.Abs(value.GetHashCode() + commentArray.GetHashCode());
                        record = commentArray[0];
                        break;
                    }
                case 2:
                    {
                        record = commentArray[1];
                        break;
                    }
                case 3:
                    {
                        string strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";//一般规则，非严谨的邮址验证规则
                        Regex r = new Regex(strExp);
                        Match m = r.Match(commentArray[1]);
                        if (m.Success)
                        {
                            username = commentArray[1];
                        }
                        record = commentArray[2];
                        break;
                    }
                default:
                    throw new FormatException(value);
            }
        }

        /// <summary>
        /// 棋局记录中的一些辅助项的生成
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="number"></param>
        /// <param name="userID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string BylawItemToString(string userID, string value)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(userID))
            {
                sb.Append('<');
                sb.Append(userID);
                sb.Append('>');
            }
            sb.AppendLine(value);
            return sb.ToString();
        }

        static public ChessStep.IndexList IndexParse(string value, char flagA, char flagB)
        {
            ChessStep.IndexList indexs = new ChessStep.IndexList();
            int a = value.IndexOf(flagA);
            int b = value.IndexOf(flagB);
            value = value.Substring(a + 1, b - (a + 1)).Trim();
            string[] arr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in arr)
            {
                indexs.Add(int.Parse(str.Trim()));
            }
            return indexs;
        }

    }
}
