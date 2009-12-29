using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// 数据库参数
    /// </summary>
    [Serializable]
    public class DbParam
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 参数的类型
        /// </summary>
        public DbType Type { get; set; }

        /// <summary>
        /// 参数的值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DbParam()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="type">参数的类型</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">参数的值</param>
        public DbParam(DbType type, string paramName, object value)
        {
            this.ParamName = paramName;
            this.Type = type;
            this.Value = value;
        }
    }
}
