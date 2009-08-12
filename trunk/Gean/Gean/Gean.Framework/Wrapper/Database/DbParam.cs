#region 命名空间引用
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
#endregion

namespace Gean.Data
{
    /// <summary>
    /// 数据库参数
    /// </summary>
    [Serializable]
    public class DbParam
    {
        #region 字段定义
        /// <summary>
        /// 参数名
        /// </summary>
        private string _paramName;

        /// <summary>
        /// 参数的类型
        /// </summary>
        private DbType _type;

        /// <summary>
        /// 参数的值
        /// </summary>
        private object _value;

        #endregion

        #region 属性

        #region 参数名
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParamName
        {
            get
            {
                return _paramName;
            }
            set
            {
                _paramName = value;
            }
        }
        #endregion

        #region 参数的类型
        /// <summary>
        /// 参数的类型
        /// </summary>
        public DbType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        #endregion

        #region 参数的值
        /// <summary>
        /// 参数的值
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DbParam()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="type">参数的类型</param>
        /// <param name="value">参数的值</param>
        public DbParam( string paramName, DbType type, object value )
        {
            _paramName = paramName;
            _type = type;
            _value = value;
        }
        #endregion
    }
}
