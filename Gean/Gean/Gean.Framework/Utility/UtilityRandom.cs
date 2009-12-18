using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Gean
{
    /// <summary>
    /// 针对.net的Random随机数生成器的扩展。
    /// http://www.NSimple.cn/
    /// 2008年9月9日16时46分
    /// </summary>
    public class UtilityRandom
    {
        /// <summary>
        /// 大小写字母与数字(以英文逗号相隔)
        /// </summary>
        static string _charToSplit = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        /// <summary>
        /// 表示伪随机数生成器。静态属性。
        /// </summary>
        static Random Random { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UtilityRandom()
        {
            Random = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        /// <summary>
        /// 获取一个随机整数。
        /// </summary>
        /// <param name="minValue">随机整数的最小值</param>
        /// <param name="maxValue">随机整数的最大值</param>
        /// <returns></returns>
        public int GetInt(int minValue, int maxValue)
        {
            return this.GetInts(1, minValue, maxValue)[0];
        }

        /// <summary>
        /// 获取一定数量的随机整数，可能会有重复。
        /// </summary>
        /// <param name="num">需获得随机整数的数量</param>
        /// <param name="minValue">随机整数的最小值</param>
        /// <param name="maxValue">随机整数的最大值</param>
        /// <returns></returns>
        public int[] GetInts(int num, int minValue, int maxValue)
        {
            int[] ints = new int[num];
            for (int i = 0; i < num; i++)
            {
                ints[i] = Random.Next(minValue, maxValue);
            }
            return ints;
        }

        /// <summary>
        /// 获取一定数量不重复的随机整数。
        /// </summary>
        /// <param name="num">需获得随机整数的数量</param>
        /// <param name="minValue">随机整数的最小值</param>
        /// <param name="maxValue">随机整数的最大值</param>
        /// <returns></returns>
        public int[] GetUnrepeatInts(int num, int minValue, int maxValue)
        {
            if (num > maxValue - minValue)
            {
                Debug.Fail("num > maxValue - minValue");
                num = maxValue - minValue;
            }
            List<int> ints = new List<int>(num);
            for (int i = 0; i < num; i++)
            {
                bool hasValue = false;
                while (!hasValue)
                {
                    int m = Random.Next(minValue, maxValue);
                    if (!ints.Contains(m))
                    {
                        ints.Add(m);
                        hasValue = true;
                    }
                }//while
            }//for
            return ints.ToArray();
        }

        /// <summary>
        /// 获取指定长度的(单字节)字符串
        /// </summary>
        /// <param name="num">所需字符串的长度</param>
        /// <param name="type">字符串中的字符的类型</param>
        /// <returns></returns>
        public string GetString(int num, RandomCharType type)
        {
            string[] chars = _charToSplit.Split(',');
            int begin = 0;
            int end = chars.Length;
            switch (type)
            {
                #region case
                case RandomCharType.Number:
                    end = 11;
                    break;
                case RandomCharType.Uppercased:
                    begin = 10 + 26;
                    break;
                case RandomCharType.Lowercased:
                    begin = 10;
                    end = 10 + 26;
                    break;
                case RandomCharType.NumberAndLowercased:
                    end = 10 + 26;
                    break;
                case RandomCharType.UppercasedAndLowercased:
                    begin = 10;
                    break;
                case RandomCharType.All:
                case RandomCharType.NumberAndUppercased:
                    break;
                case RandomCharType.None:
                default:
                    Debug.Fail(type.ToString());
                    return "";
                #endregion
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                if (type == RandomCharType.NumberAndUppercased)
                {
                    bool isLow = true;
                    while (isLow)//如果生成的数是小写字母范围的，去除
                    {
                        int m = Random.Next(begin, end);
                        if (!(m >= 10 && m < 10 + 26))
                        {
                            sb.Append(chars[m]);
                            isLow = false;
                        }
                    }
                }
                else
                {
                    sb.Append(chars[Random.Next(begin, end)]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 枚举：生成的随机字符串（数字与大小写字母）的组合类型。
        /// </summary>
        public enum RandomCharType
        {
            /// <summary>
            /// 任意。数字与大小写字母。
            /// </summary>
            All,
            /// <summary>
            /// 数字。
            /// </summary>
            Number,
            /// <summary>
            /// 大写字母。
            /// </summary>
            Uppercased,
            /// <summary>
            /// 小写字母。
            /// </summary>
            Lowercased,
            /// <summary>
            /// 数字与大写字母。
            /// </summary>
            NumberAndUppercased,
            /// <summary>
            /// 数字与小写字母。
            /// </summary>
            NumberAndLowercased,
            /// <summary>
            /// 小写字母与大写字母。
            /// </summary>
            UppercasedAndLowercased,
            /// <summary>
            /// 嘛也不是
            /// </summary>
            None,
        }
    }
}