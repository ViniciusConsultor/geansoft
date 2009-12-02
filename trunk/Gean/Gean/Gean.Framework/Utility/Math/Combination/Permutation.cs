using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Gean.MathHelper
{
    public class Permutation
    {

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

    }
}
