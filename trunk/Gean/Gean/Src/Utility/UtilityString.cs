using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Text;

namespace Gean
{
    /// <summary>
    /// 有关字符串String的扩展方法
    /// </summary>
    public static class UtilityString
    {
        /// <summary>
        /// 用一个字符串将一个原始字符串分割成字符串数组，而.net中只能使用char进行分割。
        /// </summary>
        /// <param name="sourceString">输入的字符串</param>
        /// <param name="p">字符串型的分割符</param>
        /// <param name="splitoption">指定包含还是省略返回值中的空子字符串。</param>
        /// <returns>希望的数组</returns>
        public static string[] Split(string sourceString, string p, StringSplitOptions splitoption)
        {
            string substr;
            int found = 0;
            List<string> rtnList = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(sourceString) || string.IsNullOrEmpty(p))
                {
                    return null;
                }
                for (int i = 0; i < sourceString.Length; i++)
                {
                    //查找分隔符
                    found = sourceString.IndexOf(p, i);
                    if (found >= 0)
                    {
                        //取分隔符前的字符串
                        substr = sourceString.Substring(i, found - i);
                        switch (splitoption)
                        {
                            case StringSplitOptions.None:
                                rtnList.Add(substr);
                                break;
                            case StringSplitOptions.RemoveEmptyEntries:
                                if (!string.IsNullOrEmpty(substr))
                                {
                                    rtnList.Add(substr);
                                }
                                break;
                            default:
                                Debug.Fail(splitoption.ToString());
                                break;
                        }
                        i = found + p.Length - 1;
                    }
                    else
                    {
                        rtnList.Add(sourceString.Substring(i));
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
            return rtnList.ToArray();
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            Hashtable h = new Hashtable();

            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }

            string[] result = new string[h.Count];

            h.Keys.CopyTo(result, 0);

            return result;
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, Split(stringarray, ",", StringSplitOptions.None), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, Split(stringarray, strsplit, StringSplitOptions.None), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, Split(stringarray, strsplit, StringSplitOptions.None), caseInsensetive);
        }

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }

        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Match m = null;
            for (m = UtilityRegex.Br.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase">全角数字</param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string ToSimplifiedChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string ToTraditionalChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 取指定长度的字符串，字符串如果操过指定长度则将超出的部分用指定字符串代替。
        /// </summary>
        /// <param name="srcString">要检查的字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="length">指定长度</param>
        /// <param name="tailString">用于替换的字符串,可为空</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string srcString, int startIndex, int length, string tailString)
        {
            string myResult = srcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(srcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    
                //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (startIndex >= srcString.Length)
                    {
                        return "";
                    }
                    else
                    {
                        return srcString.Substring(startIndex,
                           ((length + startIndex) > srcString.Length) ? (srcString.Length - startIndex) : length);
                    }
                }//if
            }//foreach

            if (length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(srcString);
                //当字符串长度大于起始位置
                if (bsSrcString.Length > startIndex)
                {
                    int endIndex = bsSrcString.Length;
                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (startIndex + length))
                    {
                        endIndex = length + startIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾
                        length = bsSrcString.Length - startIndex;
                        tailString = "";
                    }
                    int nRealLength = length;
                    int[] anResultFlag = new int[length];
                    byte[] bsResult = null;
                    int nFlag = 0;
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }
                        anResultFlag[i] = nFlag;
                    }
                    if ((bsSrcString[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                    {
                        nRealLength = length + 1;
                    }
                    bsResult = new byte[nRealLength];
                    Array.Copy(bsSrcString, startIndex, bsResult, 0, nRealLength);
                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + tailString;
                }//if
            }//if (length >= 0)
            return myResult;
        }
    }
}