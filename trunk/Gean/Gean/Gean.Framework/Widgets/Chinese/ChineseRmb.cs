using System;
using System.Collections.Generic;
using System.Text;
using Gean.Math;

namespace Gean.Chinese
{
    /// <summary>
    /// 一个描述整数人民币的结构
    /// </summary>
    public struct ChineseIntegerRmb
    {
        public ChineseIntegerRmb(int digit, char number)
            : this(digit, int.Parse(number.ToString()))
        { }
        public ChineseIntegerRmb(int digit, string number)
            : this(digit, int.Parse(number))
        { }
        public ChineseIntegerRmb(int digit, int number)
        {
            _number = number;
            _digit = digit;
            _numberChar = GetNumberChar(number);
            _unitChar = GetUnitChar(digit);
        }

        public static bool operator ==(ChineseIntegerRmb a, ChineseIntegerRmb b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ChineseIntegerRmb a, ChineseIntegerRmb b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            ChineseIntegerRmb r = (ChineseIntegerRmb)(obj);
            if (!_number.Equals(r._number)) return false;
            if (!_digit.Equals(r._digit)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 27 ^ _digit ^ _number;
        }

        /// <summary>
        /// 返回字符串,即使用“F”作为格式符输出。
        /// </summary>
        public override string ToString()
        {
            return this.ToString("F");
        }

        /// <summary>
        /// 根据格式符要求返回字符串。
        /// 格式符：
        /// D:输出完整意义的人民币大写字符串；
        /// F:输出仅当前位人民币大写字符串，即两位。
        /// </summary>
        /// <param name="format">
        /// 格式符：
        /// D:输出完整意义的人民币大写字符串；
        /// F:输出仅当前位人民币大写字符串，即两位。
        ///</param>
        public string ToString(string format)
        {
            switch (format)
            {
                case "D":
                    {
                        if (_number == 0)
                            return "零元整";
                        StringBuilder sb = new StringBuilder();
                        sb.Append(_numberChar).Append(_unitChar); 
                        switch (_digit)
                        {
                            case 1://分整
                            case 2://角整
                            case 3://元整
                                sb.Append("整");
                                break;
                            case 4://拾元整
                            case 5://百元整
                            case 6://千元整
                            case 7://万元整
                                sb.Append("元整");
                                break;
                            case 8://拾万元整
                            case 9://百万元整
                            case 10://千万元整
                                sb.Append("万元整");
                                break;
                            case 11://亿元整
                                sb.Append("元整");
                                break;
                            case 12://拾亿元整
                            case 13://百亿元整
                            case 14://千亿元整
                            case 15://万亿元整
                                sb.Append("亿元整");
                                break;
                        }
                        return sb.ToString();
                    }
                case "F":
                default:
                    {
                        if (_number == 0)
                            return "零";
                        StringBuilder sb = new StringBuilder(2);
                        sb.Append(_numberChar).Append(_unitChar);
                        return sb.ToString();
                    }
            }
        }

        /// <summary>
        /// Gets 位的数字.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return this._number; }
        }
        private int _number;
        /// <summary>
        /// Gets 第几位.
        /// </summary>
        /// <value>The digit.</value>
        public int Digit
        {
            get { return this._digit; }
        }
        private int _digit;
        /// <summary>
        /// Gets 转换成的大写字符.
        /// </summary>
        /// <value>The number char.</value>
        public char NumberChar
        {
            get { return this._numberChar; }
        }
        private char _numberChar;
        /// <summary>
        /// Gets 转换成的单位字符.
        /// </summary>
        /// <value>The unit char.</value>
        public char UnitChar
        {
            get { return this._unitChar; }
        }
        private char _unitChar;

        public static char GetNumberChar(int number)
        {
            return ChineseNumber[number];
        }

        public static char GetUnitChar(int digit)
        {
            return ChineseUnit[digit - 1];
        }

        /// <summary>
        /// Gets 0-9所对应的汉字
        /// </summary>
        public static string ChineseNumber
        {
            get { return "零壹贰叁肆伍陆柒捌玖"; }
        }
        /// <summary>
        /// Gets 数字位所对应的汉字 
        /// </summary>
        public static string ChineseUnit
        {
            get { return "分角元拾佰仟万拾佰仟亿拾佰仟万"; }
        }

    }

    /// <summary>
    /// 一个描述人民币的类型
    /// </summary>
    public class ChineseRmb
    {
        public static string ToUpperChineseRmb(string numString)
        {
            return ToUpperChineseRmb(numString, UtilityMath.RoundingMode.Rounding_4she5ru);
        }

        public static string ToUpperChineseRmb(string numString, UtilityMath.RoundingMode roundMode)
        {
            decimal num;
            if (decimal.TryParse(numString, out num))
            {
                return ToUpperChineseRmb(num, roundMode);
            }
            else
            {
                throw new ArgumentException("非数字的字符串。");
            }
        }

        public static string ToUpperChineseRmb(decimal num)
        {
            return ToUpperChineseRmb(num, UtilityMath.RoundingMode.Rounding_4she5ru);
        }

        public static string ToUpperChineseRmb(decimal num, UtilityMath.RoundingMode roundMode)
        {
            //if (num < 0 || num > 9999999999999.99M)
            //{
            //    throw new ArgumentOutOfRangeException("金额应为正数，小于(且不等于)10万亿");
            //}

            //string zheng = "整";
            //string shuzi = "零壹贰叁肆伍陆柒捌玖";             
            //string danwei = "万仟佰拾亿仟佰拾万仟佰拾元角分";

            //string sourceNum = string.Empty;

            //#region Round -> ToString
            //switch (roundMode)//将num取绝对值并四舍五入取2位小数,然后乘100并转换成字符串形式
            //{
            //    case UtilityMath.RoundingMode.Rounding_4she5ru:
            //        sourceNum = ((long)(UtilityMath.Round(num, 2) * 100)).ToString();
            //        break;
            //    case UtilityMath.RoundingMode.Rounding_4she6ru5chengshuang:
            //        sourceNum = ((long)(System.Math.Round(num, 2) * 100)).ToString();
            //        break;
            //}
            //#endregion

            //char[] source = sourceNum.ToCharArray();

            //StringBuilder sb = new StringBuilder(sourceNum);
            //for (int i = sourceNum.Length - 1; i >= 0; i--)
            //{
            //    sb.Replace(sb[i], shuzi[Convert.ToInt16(sb[i].ToString())], i, 1);
            //}
            //int z = 0;
            //while (!(sb.Length - z * 2 <= 0))
            //{
            //    sb.Insert(sb.Length - z * 2, danwei[danwei.Length - 1 - z]);
            //    z++;
            //}

            //return sb.ToString();
            return ToUpperChineseRmb(num, 0);
        }

        /// <summary> 
        /// 将指定的数字转换成人民币的大写形式 
        /// </summary> 
        /// <param name="num">金额.数字型,小于9万亿,大于-9万亿</param> 
        /// <returns>返回大写形式</returns> 
        private static string ToUpperChineseRmb(decimal num, int x)
        {
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException("应设置金额为正数");
            }
            string sourceNum = "";   //数字的字符串形式 
            num = System.Math.Round(num, 2);        //将num取绝对值并四舍五入取2位小数 
            sourceNum = ((long)(num * 100)).ToString();  //将num乘100并转换成字符串形式 
            if (sourceNum.Length > 15)
            {
                throw new ArgumentOutOfRangeException("金额.数字型,小于9万亿");
            }

            string shuzi = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string danwei = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string tmpStringNum = "";   //从原num值中取出的值 
            string result = "";   //人民币大写金额形式 
            string numCn = "";    //数字的汉语读法 
            string weiCn = "";    //数字位的汉字读法 
            int index;              //num的值乘以100的字符串长度 
            int nZero = 0;      //用来计算连续的零值是几个 
            int tmpIntNum;           //从原num值中取出的值 

            index = sourceNum.Length;                        //找出最高位 
            danwei = danwei.Substring(15 - index);          //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (int i = 0; i < index; i++)
            {
                tmpStringNum = sourceNum.Substring(i, 1);          //取出需转换的某一位的值 
                tmpIntNum = Convert.ToInt32(tmpStringNum);      //转换为数字 
                if (i != (index - 3) && i != (index - 7) && i != (index - 11) && i != (index - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (tmpStringNum == "0")
                    {
                        numCn = "";
                        weiCn = "";
                        nZero = nZero + 1;
                    }
                    else
                    {
                        if (tmpStringNum != "0" && nZero != 0)
                        {
                            numCn = "零" + shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                        else
                        {
                            numCn = shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (tmpStringNum != "0" && nZero != 0)
                    {
                        numCn = "零" + shuzi.Substring(tmpIntNum * 1, 1);
                        weiCn = danwei.Substring(i, 1);
                        nZero = 0;
                    }
                    else
                    {
                        if (tmpStringNum != "0" && nZero == 0)
                        {
                            numCn = shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                        else
                        {
                            if (tmpStringNum == "0" && nZero >= 3)
                            {
                                numCn = "";
                                weiCn = "";
                                nZero = nZero + 1;
                            }
                            else
                            {
                                if (index >= 11)
                                {
                                    numCn = "";
                                    nZero = nZero + 1;
                                }
                                else
                                {
                                    numCn = "";
                                    weiCn = danwei.Substring(i, 1);
                                    nZero = nZero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (index - 11) || i == (index - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    weiCn = danwei.Substring(i, 1);
                }
                result = result + numCn + weiCn;

                if (i == index - 1 && tmpStringNum == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    result = result + '整';
                }
            }
            if (num == 0)
            {
                result = "零元整";
            }
            return result;
        }
    }

 /* 正确填写票据和结算凭证的基本规定
 * 
 * 
 * 银行、单位和个人填写的各种票据和结算凭证是办理支付结算和现金收付的
 * 重要依据，直接关系到支付结算的准确、及时和安全。票据和结算凭证是银
 * 行、单位和个人凭以记载帐务的会计凭证，是记载经济业务和明确经济责任
 * 的一种书面证明。因此，填写票据和结算凭证，必须做到标准化、规范化，
 * 要要素齐全、数字正确、字迹清晰、不错漏、不潦草，防止涂改。
 * 
 * 一、中文大写金额数字应用正楷或行书填写，如壹(壹)、贰(贰)、叁、肆(肆)、
 * 伍(伍)、陆(陆)、柒、捌、玖、拾、伯、仟、万(万)、亿、元、角、分、零、
 * 整(正)等字样。不得用一、二(两)、三、四、五、六、七、八、九、十、念、
 * 毛、另(或 0)填写，不得自造简化字。如果金额数字书写中使用繁体字，也
 * 应受理。
 * 
 * 二、 中文大写金额数字到”元”为止的，在”元”之后，应写”整”(或”正”)字，
 * 在”角”之后可以不写”整”(或”正”)字。数字有”分”的，”分”后面不写”整”(或
 * ”正”)字。
 * 
 * 三、 中文大写金额数字前应标明”人民币”字样，大写金额数字应紧接”人民
 * 币”字样填写，不得留有空白。大写金额数字前未印”人民币”字样的，应加
 * 填”人民币”三字。在票据和结算凭证大写金额栏内不得预印固定的”仟、佰、
 * 拾、万、仟、佰、拾、元、角、分”字样。
 * 
 * 四、阿拉伯小写金额数字中有”0″时， 中文大写应按照汉语语言规律、金额
 * 数字构成和防止涂改的要求进行书写。举例如下：
 * 
 * (一)阿拉伯数字中间有”0″时，中文大写金额要写”零”字。如￥1，409．50，
 * 应写成人民币壹仟肆佰零玖元伍角。
 * (二)阿拉伯数字中间连续有几个”0″时，中文大写金额中间可以只写一个”零”
 * 字。如￥6，007．14，应写成人民币陆仟零柒元壹角肆分。
 * (三)阿拉伯金额数字万位或元位是”0″，或者数字中间连续有几个”0″，万
 * 位、元位也是”0″，但千位、角位不是”0″时，中文大写金额中可以只写一
 * 个零字，也可以不写”零”字。如￥1，680．32，应写成人民币壹仟陆佰捌拾
 * 元零叁角贰分，或者写成人民币壹仟陆佰捌拾元叁角贰分；又如￥107，000．53，
 * 应写成人民币壹拾万柒仟元零伍角叁分，或者写成人民币壹拾万零柒仟元伍
 * 角叁分。
 * (四)阿拉伯金额数字角位是”0″，而分位不是”0″时，中文大写金额”元”后
 * 面应写”零”字。如￥16，409．02，应写成人民币壹万陆仟肆佰零玖元零贰
 * 分；又如￥325．04，应写成人民币叁佰贰拾伍元零肆分。
 * 
 * 五、阿拉伯小写金额数字前面，均应填写人民币符号”￥”(或草写)。阿拉伯
 * 小写金额数字要认真填写，不得连写分辨不清。
 * 
 * 六、票据的出票日期必须使用中文大写。为防止变造票据的出票日期，在填
 * 写月、日时，月为壹、贰和壹拾的，日为壹至玖和壹拾、贰拾和叁拾的，应
 * 在其前加”零”； 日为拾壹至拾玖的，应在其前加”壹”。如1月15日，应写成
 * 零壹月壹拾伍日。再如10月20日，应写成零壹拾月零贰拾日。
 * 
 * 七、票据出票日期使用小写填写的，银行不予受理。大写日期未按要求规范
 * 填写的，银行可予受理，但由此造成损失的，由出票人自行承担。
 * 
 * 该文引自中国人民银行会计司编写的最新《企业、银行正确办理支付结算指
 * 南》的第114页-第115页。
*/

}
