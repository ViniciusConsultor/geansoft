using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Gean.Data
{
    /// <summary> 
    /// 分页类PagerHelper 的摘要说明。 
    /// </summary> 
    public class PagerHelper
    {

        #region 属性

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 需要返回的列
        /// </summary>
        public string FieldsToReturn { get; set; }

        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string FieldNameToSort { get; set; }

        /// <summary>
        /// 页尺寸,就是一页显示多少条记录
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前的页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 是否只返回总记录数
        /// </summary>
        public bool IsDoCount { get; set; }

        /// <summary>
        /// 是否以降序排列结果
        /// </summary>
        public bool IsDescending { get; set; }

        /// <summary>
        /// 检索条件(注意: 不要加 where)
        /// </summary>
        public string StrWhere { get; set; }

        #endregion

        #region 构造函数

        /// <summary> 
        /// 得到记录总数的构造函数 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="strwhere">检索条件</param> 
        /// <param name="connectionString">连接字符串</param> 
        public PagerHelper(string tableName, string strwhere, string connectionString)
        {
            this.TableName = tableName;
            this.IsDoCount = true;

            this.StrWhere = strwhere;
            _connectionString = connectionString;
        }

        private string _connectionString;

        /// <summary>
        /// 用于返回所有记录数据或者所有记录总数的构造函数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isDoCount">是否只返回总记录数</param>
        /// <param name="fieldNameToSort">排序字段名称</param>
        /// <param name="connectionString">连接字符串</param>
        public PagerHelper(string tableName, bool isDoCount, string fieldNameToSort, string connectionString)
        {
            this.TableName = tableName;
            this.IsDoCount = isDoCount;

            this.FieldNameToSort = fieldNameToSort;
            _connectionString = connectionString;
        }

        /// <summary>
        /// 完整的构造函数,可以包含条件,返回记录字段等条件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isDoCount">是否只返回总记录数</param>
        /// <param name="fieldsToReturn">需要返回的列</param>
        /// <param name="fieldNameToSort">排序字段名称</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="isDescending">是否以降序排列</param>
        /// <param name="strwhere">检索条件</param>
        /// <param name="connectionString">连接字符串</param>
        public PagerHelper(string tableName, bool isDoCount, string fieldsToReturn, string fieldNameToSort,
            int pageSize, int pageIndex, bool isDescending, string strwhere, string connectionString)
        {
            this.TableName = tableName;
            this.IsDoCount = isDoCount;

            this.FieldsToReturn = fieldsToReturn;
            this.FieldNameToSort = fieldNameToSort;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.IsDescending = isDescending;

            this.StrWhere = strwhere;
            _connectionString = connectionString;
        }

        #endregion

        #region 方法
        /*
        public IDataReader GetDataReader()
        {
            if (this.IsDoCount) { throw new ArgumentException("要返回记录集，DoCount属性一定为false"); }

            return SqlHelper.ExecuteReader(_connectionString, CommandType.StoredProcedure, "Pagination",
                                           new SqlParameter("@tblName", this._tableName),
                                           new SqlParameter("@strGetFields", this._fieldsToReturn),
                                           new SqlParameter("@fldName", this._fieldNameToSort),
                                           new SqlParameter("@PageSize", this._pageSize),
                                           new SqlParameter("@PageIndex", this._pageIndex),
                                           new SqlParameter("@doCount", this._isDoCount),
                                           new SqlParameter("@OrderType", this._isDescending),
                                           new SqlParameter("@strWhere", this._strWhere)
                );
        }

        public DataSet GetDataSet()
        {
            if (this._isDoCount) { throw new ArgumentException("要返回记录集，DoCount属性一定为false"); }

            return SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "Pagination",
                                            new SqlParameter("@tblName", this._tableName),
                                            new SqlParameter("@strGetFields", this._fieldsToReturn),
                                            new SqlParameter("@fldName", this._fieldNameToSort),
                                            new SqlParameter("@PageSize", this._pageSize),
                                            new SqlParameter("@PageIndex", this._pageIndex),
                                            new SqlParameter("@doCount", this._isDoCount),
                                            new SqlParameter("@OrderType", this._isDescending),
                                            new SqlParameter("@strWhere", this._strWhere)
                );
        }

        public int GetCount()
        {
            if (!this._isDoCount) { throw new ArgumentException("要返回总数统计，DoCount属性一定为true"); }

            return (int)SqlHelper.ExecuteScalar(_connectionString, CommandType.StoredProcedure, "Pagination",
                                                 new SqlParameter("@tblName", this._tableName),
                                                 new SqlParameter("@strGetFields", this._fieldsToReturn),
                                                 new SqlParameter("@fldName", this._fieldNameToSort),
                                                 new SqlParameter("@PageSize", this._pageSize),
                                                 new SqlParameter("@PageIndex", this._pageIndex),
                                                 new SqlParameter("@doCount", this._isDoCount),
                                                 new SqlParameter("@OrderType", this._isDescending),
                                                 new SqlParameter("@strWhere", this._strWhere)
                );
        }
        */
        #endregion

    }
}
