using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class Checker
    {

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null) return true;
                
            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                    return true;
                else
                    return false;
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
                return true;

            //不为空
            return false;
        }

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            return IsNullOrEmpty<object>(data);
        }

        ///// <summary>
        ///// 判断某值是否在枚举内（位枚举）
        ///// </summary>
        ///// <param name="checkingValue">被检测的枚举值</param>
        ///// <param name="expectedValue">期望的枚举值</param>
        ///// <returns></returns>
        //public static bool Exists<E>(E checkingValue, E expectedValue) where E : Enum
        //{
        //    int intCheckingValue = Convert.ToInt32(checkingValue);
        //    int intExpectedValue = Convert.ToInt32(expectedValue);
        //    return (intCheckingValue & intExpectedValue) == intExpectedValue;
        //}
    }
}
