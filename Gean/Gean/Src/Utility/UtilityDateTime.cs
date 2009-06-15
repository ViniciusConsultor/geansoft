using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean
{
    /// <summary>
    /// 有关DateTime的扩展方法
    /// </summary>
    public class UtilityDateTime
    {
        /// <summary>
        /// 月份名称的字符串数组(英语)
        /// </summary>	
        public static string[] Monthes
        {
            get { return new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; }
        }

        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (string.IsNullOrEmpty(datetimestr))
            {
                Debug.Fail("DateTime String IsNullOrEmpty!");
                return replacestr;
            }
            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }
            DateTime s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd
        /// </sumary>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="dateValue">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string dateValue)
        {
            return UtilityRegex.Date.IsMatch(dateValue);
        }

        /// <summary>
        /// 判断字符串是否是00:00:00字符串
        /// </summary>
        /// <param name="timeValue">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsTimeString(string timeValue)
        {
            return UtilityRegex.Time.IsMatch(timeValue);
        }

        /// <summary>
        /// 返回与当前时间相差的秒数
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(Time).AddSeconds(Sec);
            if (ts.TotalSeconds > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalSeconds < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// 返回与当前时间相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalMinutes < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalMinutes;
        }

        /// <summary>
        /// 返回与当前时间相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalHours < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalHours;
        }

        /// <summary>
        /// 计算本周的起止日期
        /// </summary>
        public void ThisWeekRange(ref DateTime start, ref DateTime end)
        {
            DateTime dt = DateTime.Today;
            int weekNow = Convert.ToInt32(dt.DayOfWeek);
            int dayDiff = (-1) * weekNow;
            int dayAdd = 6 - weekNow;
            start = System.DateTime.Now.AddDays(dayDiff).Date;
            end = System.DateTime.Now.AddDays(dayAdd).Date;
        }

        /// <summary>
        /// 根据某年的第几周获取这周的起止日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOrder"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void WeekRange(int year, int weekOrder, ref DateTime start, ref DateTime end)
        {
            //非法参数
            if (year <= 0 || weekOrder <= 0 || weekOrder > 53)
            {
                return;
            }

            //当年的第一天
            DateTime firstDay = new DateTime(year, 1, 1);

            //当年的第一天是星期几
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);

            //计算当年第一周的起止日期，可能跨年
            int dayDiff = (-1) * firstOfWeek;
            int dayAdd = 6 - firstOfWeek;
            start = firstDay.AddDays(dayDiff).Date;
            end = firstDay.AddDays(dayAdd).Date;

            //如果不是要求计算第一周
            if (weekOrder != 1)
            {
                int addDays = (weekOrder - 1) * 7;
                start = start.AddDays(addDays);
                end = end.AddDays(addDays);
            }
        }
        
        /// <summary>
        /// 计算某天属于当年的第几周
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public int GetWeekOrderOfDate(DateTime Date)
        {
            //当天所在的年份
            int year = Date.Year;
            //当年的第一天
            DateTime firstDay = new DateTime(year, 1, 1);
            //当年的第一天是星期几
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);
            //当年第一周的天数
            int firstWeekDayNum = 7 - firstOfWeek;

            //传入日期在当年的天数与第一周天数的差
            int otherDays = Date.DayOfYear - firstWeekDayNum;
            //传入日期不在第一周内
            if (otherDays > 0)
            {
                int weekNumOfOtherDays;
                if (otherDays % 7 == 0)
                {
                    weekNumOfOtherDays = otherDays / 7;
                }
                else
                {
                    weekNumOfOtherDays = otherDays / 7 + 1;
                }

                return weekNumOfOtherDays + 1;
            }
            //传入日期在第一周内
            else
            {
                return 1;
            }
        }
    }
}
