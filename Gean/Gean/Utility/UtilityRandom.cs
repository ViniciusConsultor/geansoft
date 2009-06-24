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
        static string _CharToSplit = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        /// <summary>
        /// 包含汉字拼音的字符串数组。
        /// </summary>
        static string[] pinyins = new string[] { "a", "ai", "an", "ang", "ao", "ba", "bai", "ban", "bang", "bao", "bei", "ben", "beng", "bi", "bian", "biao", "bie", "bin", "bing", "bo", "bu", "ca", "cai", "can", "cang", "cao", "ce", "ceng", "cha", "chai", "chan", "chang", "chao", "che", "chen", "cheng", "chi", "chong", "chou", "chu", "chuai", "chuan", "chuang", "chui", "chun", "chuo", "ci", "cong", "cou", "cu", "cuan", "cui", "cun", "cuo", "da", "dai", "dan", "dang", "dao", "de", "deng", "di", "dian", "diao", "die", "ding", "diu", "dong", "dou", "du", "duan", "dui", "dun", "duo", "e", "en", "er", "fa", "fan", "fang", "fei", "fen", "feng", "fo", "fou", "fu", "ga", "gai", "gan", "gang", "gao", "ge", "gei", "gen", "geng", "gong", "gou", "gu", "gua", "guai", "guan", "guang", "gui", "gun", "guo", "ha", "hai", "han", "hang", "hao", "he", "hei", "hen", "heng", "hong", "hou", "hu", "hua", "huai", "huan", "huang", "hui", "hun", "huo", "ji", "jia", "jian", "jiang", "jiao", "jie", "jin", "jing", "jiong", "jiu", "ju", "juan", "jue", "jun", "ka", "kai", "kan", "kang", "kao", "ke", "ken", "keng", "kong", "kou", "ku", "kua", "kuai", "kuan", "kuang", "kui", "kun", "kuo", "la", "lai", "lan", "lang", "lao", "le", "lei", "leng", "li", "lia", "lian", "liang", "liao", "lie", "lin", "ling", "liu", "long", "lou", "lu", "lv", "luan", "lue", "lun", "luo", "ma", "mai", "man", "mang", "mao", "me", "mei", "men", "meng", "mi", "mian", "miao", "mie", "min", "ming", "miu", "mo", "mou", "mu", "na", "nai", "nan", "nang", "nao", "ne", "nei", "nen", "neng", "ni", "nian", "niang", "niao", "nie", "nin", "ning", "niu", "nong", "nu", "nv", "nuan", "nue", "nuo", "o", "ou", "pa", "pai", "pan", "pang", "pao", "pei", "pen", "peng", "pi", "pian", "piao", "pie", "pin", "ping", "po", "pu", "qi", "qia", "qian", "qiang", "qiao", "qie", "qin", "qing", "qiong", "qiu", "qu", "quan", "que", "qun", "ran", "rang", "rao", "re", "ren", "reng", "ri", "rong", "rou", "ru", "ruan", "rui", "run", "ruo", "sa", "sai", "san", "sang", "sao", "se", "sen", "seng", "sha", "shai", "shan", "shang", "shao", "she", "shen", "sheng", "shi", "shou", "shu", "shua", "shuai", "shuan", "shuang", "shui", "shun", "shuo", "si", "song", "sou", "su", "suan", "sui", "sun", "suo", "ta", "tai", "tan", "tang", "tao", "te", "teng", "ti", "tian", "tiao", "tie", "ting", "tong", "tou", "tu", "tuan", "tui", "tun", "tuo", "wa", "wai", "wan", "wang", "wei", "wen", "weng", "wo", "wu", "xi", "xia", "xian", "xiang", "xiao", "xie", "xin", "xing", "xiong", "xiu", "xu", "xuan", "xue", "xun", "ya", "yan", "yang", "yao", "ye", "yi", "yin", "ying", "yo", "yong", "you", "yu", "yuan", "yue", "yun", "za", "zai", "zan", "zang", "zao", "ze", "zei", "zen", "zeng", "zha", "zhai", "zhan", "zhang", "zhao", "zhe", "zhen", "zheng", "zhi", "zhong", "zhou", "zhu", "zhua", "zhuai", "zhuan", "zhuang", "zhui", "zhun", "zhuo", "zi", "zong", "zou", "zu", "zuan", "zui", "zun", "zuo" };

        /// <summary>
        /// 表示伪随机数生成器。静态属性。
        /// </summary>
        static Random Randow { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UtilityRandom()
        {
            Randow = new Random(unchecked((int)DateTime.Now.Ticks));
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
                ints[i] = Randow.Next(minValue, maxValue);
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
                    int m = Randow.Next(minValue, maxValue);
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
        /// 获取指定长度的字符串
        /// </summary>
        /// <param name="num">所需字符串的长度</param>
        /// <param name="type">字符串中的字符的类型</param>
        /// <returns></returns>
        public string GetString(int num, RandomCharType type)
        {
            string[] chars = _CharToSplit.Split(',');
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
                        int m = Randow.Next(begin, end);
                        if (!(m >= 10 && m < 10 + 26))
                        {
                            sb.Append(chars[m]);
                            isLow = false;
                        }
                    }
                }
                else
                {
                    sb.Append(chars[Randow.Next(begin, end)]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取几个拼音字符串数组
        /// </summary>
        /// <param name="num">获取的数量</param>
        /// <returns></returns>
        public string[] GetPinyin(int num)
        {
            string[] rtn = new string[num];
            for (int i = 0; i < num; i++)
            {
                rtn[i] = pinyins[Randow.Next(0, pinyins.Length)];
            }
            return rtn;
        }

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