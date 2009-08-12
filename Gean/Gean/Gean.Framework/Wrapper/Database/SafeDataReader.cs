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
    /// 数据读取器的增强版本
    /// </summary>
    public class SafeDataReader : IDisposable
    {
        #region 字段
        /// <summary>
        /// 数据读取器
        /// </summary>
        private IDataReader _reader;
        #endregion

        #region 构造函数
        /// <summary>
        /// 加载数据读取器
        /// </summary>
        /// <param name="reader">数据读取器</param>
        public SafeDataReader( IDataReader reader )
        {
            this._reader = reader;
        }
        #endregion

        #region 获取字符串值
        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public string GetString( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return string.Empty;
            }

            try
            {
                return _reader[fieldName.Trim()].ToString();
            }
            catch ( Exception ex )
            {
                
                return string.Empty;
            }
        }
        #endregion

        #region 获取布尔值
        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public bool GetBoolean( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return false;
            }

            return UtilityConvert.ToBoolean(_reader[fieldName.Trim()]);
        }
        #endregion

        #region 获取整数值
        /// <summary>
        /// 获取整数值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public int GetInt32( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return 0;
            }

            return UtilityConvert.ToInt32( _reader[fieldName.Trim()] );
        }
        #endregion

        #region 获取浮点值
        /// <summary>
        /// 获取浮点值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public double GetDouble( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return 0;
            }

            return UtilityConvert.ToDouble( _reader[fieldName.Trim()] );
        }
        #endregion

        #region 获取Decimal
        /// <summary>
        /// 获取浮点值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public decimal GetDecimal( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return 0;
            }

            return UtilityConvert.ToDecimal( _reader[fieldName.Trim()] );
        }
        #endregion

        #region 获取日期值
        /// <summary>
        /// 获取日期值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public DateTime GetDateTime( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return Convert.ToDateTime( "1900-1-1" );
            }

            return UtilityConvert.ToDateTime( _reader[fieldName.Trim()] );
        }
        #endregion

        #region 获取SmartDate
        /// <summary>
        /// 获取SmartDate日期值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public SmartDate GetSmartDate( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return new SmartDate( true );
            }

            return UtilityConvert.ToSmartDate( _reader[fieldName.Trim()] );
        }
        #endregion

        #region 获取GUID
        /// <summary>
        /// 获取GUID
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public Guid GetGUID( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return Guid.Empty;
            }

            try
            {
                return (Guid)( _reader[fieldName.Trim()] );
            }
            catch
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region 获取枚举
        /// <summary>
        /// 获取枚举
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <param name="fieldName">字段名</param>
        public T GetEnum<T>( string fieldName )
        {
            //有效性检测
            if ( Checking.IsNullOrEmpty( fieldName ) || _reader[fieldName] == null )
            {
                return default(T);
            }

            return UtilityEnums.GetInstance<T>( _reader[fieldName.Trim()] );
        }
        #endregion

        #region 读取数据
        /// <summary>
        /// 读取数据
        /// </summary>
        public bool Read()
        {
            return _reader.Read();
        }
        #endregion

        #region 释放资源
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Close()
        {
            _reader.Close();
        }
        #endregion

        #region 重写Dispose
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }
        #endregion        
    }
}
