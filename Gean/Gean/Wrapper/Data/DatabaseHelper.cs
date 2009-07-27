/** 1. 功能：操作数据库公共类的抽象基类
 *  2. 作者：何平 
 *  3. 创建日期：2008-3-19
 *  4. 最后修改日期：2008-10-2
**/
#region 命名空间引用
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
#endregion

namespace Gean.DataBase
{
    /// <summary>
    /// 操作数据库公共类的抽象基类
    /// </summary>    
    public abstract class DatabaseHelper : IDataBase
    {
        #region 字段定义
        /// <summary>
        /// 默认的连接字符串
        /// </summary>
        protected static string _conStr;
        #endregion

        #region 静态构造函数,初始化连接字符串
        static DatabaseHelper()
        {
            //初始化连接字符串
            _conStr = BaseInfo.ConnectionString;
        }
        #endregion

        #region 执行SQL语句

        #region 执行增，删，改操作
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract int ExecSQL(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        public abstract int ExecSQL(string sql, string varName, byte[] buffer);

        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract int ExecSQL(StringBuilder sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        public abstract int ExecSQL(StringBuilder sql, string varName, byte[] buffer);
        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataSet GetDataSet(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataSet GetDataSet(StringBuilder sql, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个DataView集合
        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataView GetDataView(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataView GetDataView(StringBuilder sql, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataTable GetDataTable(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataTable GetDataTable(StringBuilder sql, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract IDataReader ExecReader(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个DataReader游标
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract IDataReader ExecReader(StringBuilder sql, params IDataParameter[] sqlParams);
        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataRow GetDataRow(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract DataRow GetDataRow(StringBuilder sql, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract string ExecScalar(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract T ExecScalar<T>(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract string ExecScalar(StringBuilder sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract T ExecScalar<T>(StringBuilder sql, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract string Lookup(string sql, string columnName, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract T Lookup<T>(string sql, string columnName, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract string Lookup(StringBuilder sql, string columnName, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract T Lookup<T>(StringBuilder sql, string columnName, params IDataParameter[] sqlParams);
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract bool HasData(string sql, params IDataParameter[] sqlParams);

        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public abstract bool HasData(StringBuilder sql, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个递增的数字ID
        /// <summary>
        /// 获取一个递增的数字ID
        /// </summary>
        /// <param name="ID_Field">表的ID字段名</param>
        /// <param name="tableName">表名</param>      
        public abstract int GetID(string ID_Field, string tableName);
        #endregion

        #region 判断指定数据库是否存在
        /// <summary>
        /// 判断指定数据库是否存在,存在返回true,不存在返回false
        /// </summary>
        /// <param name="databaseName">数据库名</param>        
        public abstract bool HasDataBase(string databaseName);
        #endregion

        #region 创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        public abstract void CreateDataBase(string databaseName);
        #endregion

        #endregion

        #region 执行事务
        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// 该方法用于执行只有一条SQL需要处理二进制数据的情况.
        /// </summary>        
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        /// <param name="errMsg">如果执行失败,返回的错误消息</param>
        /// <param name="sqls">多条SQL语句</param>
        public abstract bool ExecTransaction(string varName, byte[] buffer, out string errMsg, params string[] sqls);

        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// 该方法用于每条SQL都需要处理二进制数据的情况.
        /// </summary>   
        /// <param name="sqls">多条SQL语句</param>
        /// <param name="varNames">变量名的数组,数组元素的形式：@变量名.注意:数组中的变量名不能重名!</param>
        /// <param name="buffers">二进制数据的数组</param>        
        public abstract bool ExecTransaction(string[] sqls, string[] varNames, List<byte[]> buffers);

        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// </summary>
        /// <param name="sql">多条SQL语句</param>
        public abstract bool ExecTransaction(params string[] sql);

        /// <summary>
        /// 开始事务。注意：本方法必须配合ExecSQL和CommitTransaction方法使用.
        /// </summary>
        public abstract void BeginTransaction();

        /// <summary>
        /// 提交事务.注意：本方法必须配合BeginTransaction和ExecSQL方法使用.
        /// </summary>
        public abstract bool CommitTransaction();
        #endregion

        #region 执行存储过程

        #region 执行增，删，改操作
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract int ExecProc(string proc, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract DataSet GetDataSetByProc(string proc, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个DataView集合
        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract DataView GetDataViewByProc(string proc, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract DataTable GetDataTableByProc(string proc, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用DataReader的Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract IDataReader ExecReaderByProc(string proc, params IDataParameter[] sqlParams);
        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract DataRow GetDataRowByProc(string proc, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>        
        public abstract string ExecScalarByProc(string proc, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract T ExecScalarByProc<T>(string proc, params IDataParameter[] sqlParams);
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract string LookupByProc(string proc, string columnName, params IDataParameter[] sqlParams);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        /// <returns></returns>
        public abstract T LookupByProc<T>(string proc, string columnName, params IDataParameter[] sqlParams);
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public abstract bool HasDataByProc(string proc, params IDataParameter[] sqlParams);
        #endregion

        #endregion

        #region 创建传入存储过程的参数
        /// <summary>
        /// 创建传入存储过程的参数
        /// </summary>
        /// <param name="paramName">存储过程中的参数名</param>
        /// <param name="type">参数的类型</param>
        /// <param name="value">参数的值</param>        
        public abstract DbParameter CreateParam(string paramName, DbType type, object value);

        /// <summary>
        /// 创建传入存储过程的输出参数
        /// </summary>
        /// <param name="paramName">存储过程中的参数名</param>
        /// <param name="type">参数的类型</param>
        public abstract DbParameter CreateOutParam(string paramName, DbType type);
        #endregion

        #region 获取存储过程中返回的输出参数
        /// <summary>
        /// 获取存储过程中返回的输出参数
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="paramName">输出参数的名称</param>
        public abstract T GetOutParamValue<T>(string paramName);
        #endregion

        #region 更改数据库
        /// <summary>
        /// 更改数据库
        /// </summary>
        /// <param name="database">数据库名称</param>
        public abstract void ChangeDataBase(string database);
        #endregion

        #region 检测数据集中是否包含记录

        #region 重载方法1
        /// <summary>
        /// 检测DataTable中是否包含记录,存在返回true
        /// </summary>
        /// <param name="dt">数据集</param>
        public static bool Contains(DataTable dt)
        {
            //获取行数
            int count = dt.Rows.Count;

            //没有记录，返回false
            return count == 0 ? false : true;
        }
        #endregion

        #region 重载方法2
        /// <summary>
        /// 检测DataTable中是否包含指定记录,存在返回true
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="filter">查找的条件</param>
        public static bool Contains(DataTable dt, string filter)
        {
            //查找指定条件的记录
            DataRow[] rows = dt.Select(filter);

            //没有记录，返回false
            return rows.Length == 0 ? false : true;
        }
        #endregion

        #region 重载方法3
        /// <summary>
        /// 检测DataSet中是否包含记录,存在返回true
        /// </summary>
        /// <param name="ds">数据集</param>
        public static bool Contains(DataSet ds)
        {
            return Contains(ds.Tables[0]);
        }
        #endregion

        #region 重载方法4
        /// <summary>
        /// 检测DataSet中是否包含指定记录,存在返回true
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="filter">查找的条件</param>
        public static bool Contains(DataSet ds, string filter)
        {
            return Contains(ds.Tables[0], filter);
        }
        #endregion

        #region 重载方法5
        /// <summary>
        /// 检测DataView中是否包含记录,存在返回true
        /// </summary>
        /// <param name="dv">数据集</param>
        public static bool Contains(DataView dv)
        {
            return Contains(dv.Table);
        }
        #endregion

        #region 重载方法6
        /// <summary>
        /// 检测DataSet中是否包含指定记录,存在返回true
        /// </summary>
        /// <param name="dv">数据集</param>
        /// <param name="filter">查找的条件</param>
        public static bool Contains(DataView dv, string filter)
        {
            return Contains(dv.Table, filter);
        }
        #endregion

        #endregion

        #region 操作XML

        #region 将DataTable写入XML文件
        /// <summary>
        /// 将DataTable写入指定XML文件
        /// </summary>
        /// <param name="dt">要写入的DataTable</param>
        /// <param name="filePath">Xml文件的绝对路径</param>
        public static void WriteXml(DataTable dt, string filePath)
        {
            //创建一个DataSet容器
            DataSet ds;

            //如果DataTable还未装入DataSet容器，则装入
            if (ValidationHelper.IsNullOrEmpty<DataSet>(dt.DataSet))
            {
                //创建一个DataSet
                ds = new DataSet();

                //将DataTable加入到DataSet
                ds.Tables.Add(dt);
            }
            else
            {
                //获取该DataTable的容器
                ds = dt.DataSet;
            }

            //写入XML文件
            ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// 将DataTable写入指定XML文件
        /// </summary>
        /// <param name="dt">要写入的DataTable</param>
        /// <param name="filePath">Xml文件的绝对路径</param>
        /// <param name="dataSetName">写入Xml文件中的根标记名称，即DataSet的名称</param>
        public static void WriteXml(DataTable dt, string filePath, string dataSetName)
        {
            //创建一个DataSet容器
            DataSet ds;

            //如果DataTable还未装入DataSet容器，则装入
            if (ValidationHelper.IsNullOrEmpty<DataSet>(dt.DataSet))
            {
                //创建一个DataSet
                ds = new DataSet();

                //将DataTable加入到DataSet
                ds.Tables.Add(dt);
            }
            else
            {
                //获取该DataTable的容器
                ds = dt.DataSet;
            }

            //指定DataSet容器的名称
            ds.DataSetName = dataSetName;

            //写入XML文件
            ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }
        #endregion

        #region 将XML文件转换为DataTable
        /// <summary>
        /// 将XML文件读入DataTable
        /// </summary>
        /// <param name="filePath">xml文件的绝对路径</param>        
        public static DataTable ReadXml(string filePath)
        {
            //创建一个DataSet
            DataSet ds = new DataSet();

            //将Xml文件读入DataSet
            ds.ReadXml(filePath);

            //返回DataSet中的第一个表
            return ds.Tables[0];
        }
        #endregion

        #endregion

        #region 对SQL进行合法性验证，并修改为正确的SQL
        /// <summary>
        /// 对SQL进行合法性验证，并修改为正确的SQL
        /// </summary>
        protected virtual void GetValidSQL(ref string sql)
        {
            //当SQL以等号结尾，则加上''
            if (sql.Trim().EndsWith("="))
            {
                sql = sql + "''";
            }
        }
        #endregion

        #region 将数据集转成Html字符串

        #region 将DataSet转成Html字符串
        /// <summary>
        /// 将DataSet转成Html字符串
        /// </summary>
        /// <param name="ds">数据集</param>
        public static string DataSetToHtml(DataSet ds)
        {
            return DataTableToHtml(ds.Tables[0]);
        }
        #endregion

        #region 将DataView转成Html字符串
        /// <summary>
        /// 将DataView转成Html字符串
        /// </summary>
        /// <param name="dv">数据集</param>
        public static string DataViewToHtml(DataView dv)
        {
            return DataTableToHtml(dv.Table);
        }
        #endregion

        #region 将DataTable转成Html字符串
        /// <summary>
        /// 将DataTable转成Html字符串
        /// </summary>
        /// <param name="dt">数据集</param>
        public static string DataTableToHtml(DataTable dt)
        {
            //创建要导出的HTML字符串
            StringBuilder strExport = new StringBuilder();
            strExport.Append("<table border='1' cellspacing='0' cellpadding='0' >");

            //============================================= 拼接表头 =====================================================
            strExport.Append("<tr style='FONT-WEIGHT: bold;FONT-SIZE: 20px;FONT-FAMILY: 宋体'>");
            //遍历列,添加列名
            foreach (DataColumn column in dt.Columns)
            {
                strExport.AppendFormat("<td>{0}</td>", column.ColumnName);
            }
            strExport.Append("</tr>");

            //============================================ 拼接记录 ====================================================
            //遍历记录
            foreach (DataRow row in dt.Rows)
            {
                strExport.Append("<tr>");
                //遍历列,添加单元格的值
                foreach (DataColumn column in dt.Columns)
                {
                    strExport.AppendFormat("<td>{0}</td>", row[column.ColumnName].ToString());
                }
                strExport.Append("</tr>");
            }

            strExport.Append("</table>");

            //返回HTML字符串
            return strExport.ToString();
        }
        #endregion

        #endregion
    }
}
