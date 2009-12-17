using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Gean.Math
{
    public class UtilityMath
    {
        /// <summary>
        /// 将一个字符类型的数字加1，如字符串"20060308"，加1后变成"20060309"。
        /// 如果字符串是"0002006"，"JH200603"，甚至是"JH20060A01"，这个函数是
        /// 将字符串的ASCII码+1，所以基本不需考虑是否包含字母或包含0前缀。 
        /// </summary>
        /// <param name="numberStr">The number STR.</param>
        /// <returns></returns>
        public static string IncString(string numberStr)
        {
            if (numberStr == "") return "";

            char[] numberChar = numberStr.ToCharArray();
            bool isJw = true; //进位
            int charIndex = numberStr.Length - 1;
            string resultStr = "";

            while (charIndex >= 0)
            {
                if (isJw == true)
                {
                    isJw = false;
                    if (numberStr[charIndex] == '9' || numberStr[charIndex] == 'Z' || numberStr[charIndex] == 'z')
                    {
                        resultStr = "0" + resultStr;
                        isJw = true;
                    }
                    else
                    {
                        int ASCIIValue = (int)numberStr[charIndex] + 1;
                        char ASCII = (char)ASCIIValue;
                        resultStr = new string(ASCII, 1) + resultStr;
                    }
                }
                else
                {
                    resultStr = new string(numberStr[charIndex], 1) + resultStr;
                }
                charIndex--;
            }
            return resultStr;
        } 

        /// <summary>   
        /// 连乘积函数
        /// 类似：1 x 2 x 3 x 4 ……
        /// </summary>   
        /// <param name="start">起点数(较小的数字)</param>
        /// <param name="end">终点数(较大的数字)</param>
        public static BigInteger ContinuousMultiplication(int start, int end)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} can not be less than 0", start));
            if (end < 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} can not be less than 0", end));
            if (start > end)
                throw new ArgumentOutOfRangeException(string.Format("{0} compare {1} large", start, end));
            if (end == 0) 
                return 1;

            BigInteger tempResult = 1;
            for (int i = start; i <= end; i++)
            {
                tempResult *= i;
            }
            return tempResult;
        }
        
        /// <summary>
        /// 阶乘函数。
        /// 也就是求解将n个相异物排成一列的排列数。
        /// 阶乘(factorial)是基斯顿·卡曼(Christian Kramp, 1760 – 1826)于1808年发明的运算符号。
        /// 阶乘指从1乘以2乘以3乘以4一直乘到所要求的数。 
        /// 即为限定连乘积函数从“0”开始。
        /// </summary>
        /// <param name="n">要求的阶乘数</param>
        /// <returns></returns>
        public static BigInteger Factorial(int n)
        {
            return ContinuousMultiplication(1, n);
        }

        /// <summary>   
        /// 排列数函数
        /// 公式P是指排列，从N个元素取R个进行排列。
        /// </summary>
        public static BigInteger P(int n, int r)
        {
            if (1 > r || r > n)
            {
                throw new ArgumentOutOfRangeException("N >= R >= 1");
            }
            return ContinuousMultiplication(n - r + 1, n);
        }

        /// <summary>
        /// 组合数函数
        /// 公式C是指组合，从N个元素取R个进行组合。
        /// </summary>
        /// <param name="n"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static BigInteger C(int n, int r)
        {
            if (1 > r || r > n)
            {
                throw new ArgumentOutOfRangeException("N >= R >= 1");
            }
            BigInteger a = ContinuousMultiplication(n - r + 1, n);
            BigInteger b = ContinuousMultiplication(2, r);
            return a / b;
        }

        /// <summary>  
        /// 排列循环方法  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long _test_P1(int N, int R)
        {
            if (R > N || R <= 0 || N <= 0) throw new ArgumentException("params invalid!");
            long t = 1;
            int i = N;

            while (i != N - R)
            {
                try
                {
                    checked
                    {
                        t *= i;
                    }
                }
                catch
                {
                    throw new OverflowException("overflow happens!");
                }
                --i;
            }
            return t;
        }

        /// <summary>  
        /// 排列堆栈方法  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long _test_P2(int N, int R)
        {
            if (R > N || R <= 0 || N <= 0) throw new ArgumentException("arguments invalid!");
            Stack<int> s = new Stack<int>();
            long iRlt = 1;
            int t;
            s.Push(N);
            while ((t = s.Peek()) != N - R)
            {
                try
                {
                    checked
                    {
                        iRlt *= t;
                    }
                }
                catch
                {
                    throw new OverflowException("overflow happens!");
                }
                s.Pop();
                s.Push(t - 1);
            }
            return iRlt;
        }

        /// <summary>  
        /// 组合  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long _test_C(int N, int R)
        {
            return _test_P1(N, R) / _test_P1(R, R);
        }

        /// <summary>
        /// 10置换法
        /// 算法思想：
        /// (1)  初始化一个m个元素的数组（全部由0，1组成），将前n个初始化为1，后面的为0。这时候就可以输出第一个组合序列了。
        /// (2)  从前往后找，找到第一个10组合，将其反转成01，然后将这个10组合前面的所有1，全部往左边推 ，
        /// 即保证其前面的1都在最左边。这时又可以输出一组组合序列了。
        /// (3)  重复第(2)步，知道找不到10组合位置。这时已经输出了全部的可能序列了。 
        /// 为什么？你想，（以m=5,n=3为例）一开始是11100，最后就是00111，已经没有10组合了 。 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        public void _test_combination(int m, int n)
        {
            char[] totalArray = new char[m];
            //记录排序次数  
            int totalSortNum = 0;

            //建立 111...100...0  
            for (int i = 0; i < m; i++)
            {
                if (i < n)
                    totalArray[i] = '1';
                else
                    totalArray[i] = '0';
            }
            totalSortNum += 1;

            //"10"反转置换法  
            int index = -1;
            while ((index = ArrayToString(totalArray).IndexOf("10")) != -1)
            {
                //交换"10"为"01"  
                totalArray[index] = '0';
                totalArray[index + 1] = '1';
                //计算刚反转的"10"前面所有的'1'全部移动到最左边  
                int count = 0;
                for (int i = 0; i < index; i++)
                {
                    if (totalArray[i] == '1')
                        count++;
                }
                for (int j = 0; j < index; j++)
                {
                    if (j < count)
                        totalArray[j] = '1';
                    else totalArray[j] = '0';
                }
                //输出结果  
                totalSortNum++;
            }
        }
        private string ArrayToString(char[] cs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in cs)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
