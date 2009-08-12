#region 引用命名空间
using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Gean.Data
{
    /// <summary>
    /// 数据库查询接口
    /// </summary>
    public interface IQuery : IBaseQuery
    {
        #region 属性

        #region 表名
        /// <summary>
        /// 表名( 查询专用 )
        /// </summary>
        string TableName
        {
            get;
            set;
        }
        #endregion

        #region 主键字段
        /// <summary>
        /// 主键字段( 查询专用 )
        /// </summary>
        string PkColumnName
        {
            get;
            set;
        }
        #endregion

        #region Where子句
        /// <summary>
        /// Where子句( 查询专用 )
        /// </summary>
        string Where
        {
            get;
            set;
        }
        #endregion

        #region Group子句
        /// <summary>
        /// Group子句( 查询专用 )
        /// </summary>
        string Group
        {
            get;
            set;
        }
        #endregion

        #region 每页显示的记录数
        /// <summary>
        /// 每页显示的记录数( 查询专用 ),默认每页30行记录
        /// </summary>
        int PageSize
        {
            get;
            set;
        }
        #endregion

        #region 总记录数
        /// <summary>
        /// 总记录数( 查询专用 )
        /// </summary>
        int RowCount
        {
            get;
            set;
        }
        #endregion

        #region 总页数
        /// <summary>
        /// 总页数( 查询专用 )
        /// </summary>
        int PageCount
        {
            get;
            set;
        }
        #endregion

        #region Sql参数
        /// <summary>
        /// Sql参数( 查询专用,只读 )
        /// </summary>
        DbParamCollection SqlParams
        {
            get;
        }
        #endregion

        #region SQL参数集合字符串
        /// <summary>
        /// SQL参数字符串,仅用于日志记录( 只读 )
        /// </summary>
        string SqlParamsString
        {
            get;
        }
        #endregion

        #region 日志
        /// <summary>
        /// 日志,返回错误信息( 只读 )
        /// </summary>
        string Log
        {
            get;
        }
        #endregion

        #endregion

        #region 获取总记录数和总页数
        /// <summary>
        /// 获取总记录数和总页数( 查询专用 ),
        /// 注意：本方法必须在GetResult方法之前调用。
        /// </summary>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        void GetCount(out int rowCount, out int pageCount);
        #endregion

        #region 获取结果集
        /// <summary>
        /// 获取结果集( 查询专用 ),默认获取第1页的结果集
        /// </summary>
        DataTable GetResult();

        /// <summary>
        /// 获取结果集( 查询专用 )
        /// </summary>
        /// <param name="pageIndex">页索引,如果要获取第10页的数据，则传入10</param>
        DataTable GetResult(int pageIndex);

        /// <summary>
        /// 获取结果集( 查询专用 )
        /// </summary>
        /// <param name="pageIndex">页索引,如果要获取第10页的数据，则传入10</param>
        /// <param name="sql">获取创建的SQL查询语句</param>
        DataTable GetResult(int pageIndex, out string sql);
        #endregion

        #region 添加输入参数
        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="dbParams">SQL参数集合</param>
        void AddInParam(DbParamCollection dbParams);
        #endregion
    }
}
