using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gean.Wrapper
{
    /// <summary>
    /// 系统时间的修改。
    /// </summary>
    public class SystemDateTime
    {
        [DllImport("Kernel32.dll")]
        private static extern Boolean SetSystemTime([In, Out] ApiDateTime st);

        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="newDateTime">新时间</param>
        /// <returns></returns>
        public static bool SetDateTime(DateTime newDateTime)
        {
            int UtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime(2001, 09, 01)).Hours;
            newDateTime = newDateTime.AddHours(-UtcOffset); 

            ApiDateTime st = new ApiDateTime();
            st.year = Convert.ToUInt16(newDateTime.Year);
            st.month = Convert.ToUInt16(newDateTime.Month);
            st.day = Convert.ToUInt16(newDateTime.Day);
            st.dayofweek = Convert.ToUInt16(newDateTime.DayOfWeek);
            st.hour = Convert.ToUInt16(newDateTime.Hour);
            st.minute = Convert.ToUInt16(newDateTime.Minute);
            st.second = Convert.ToUInt16(newDateTime.Second);
            st.milliseconds = Convert.ToUInt16(newDateTime.Millisecond);
            return SetSystemTime(st);
        }

        /// <summary>
        /// 系统时间的结构封装
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct ApiDateTime
        {
            public ushort year;
            public ushort month;
            public ushort dayofweek;
            public ushort day;
            public ushort hour;
            public ushort minute;
            public ushort second;
            public ushort milliseconds;
        }
    }
}
