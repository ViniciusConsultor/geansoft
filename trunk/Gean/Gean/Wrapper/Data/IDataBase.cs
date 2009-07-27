/** 1. 功能：操作数据库的接口
 *  2. 作者：何平 
 *  3. 创建日期：2008-3-18
 *  4. 最后修改日期：2008-10-7
**/
#region 引用命名空间
using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Gean.DataBase
{
    /// <summary>
    /// 操作数据库的接口
    /// </summary>    
    public interface IDataBase
    {
        #region 执行SQL语句

        #region 执行增，删，改操作
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        int ExecSQL( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        int ExecSQL( string sql, string varName, byte[] buffer );

        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        int ExecSQL( StringBuilder sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        int ExecSQL( StringBuilder sql, string varName, byte[] buffer );

        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataSet GetDataSet( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataSet GetDataSet( StringBuilder sql, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个DataView集合
        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataView GetDataView( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataView GetDataView( StringBuilder sql, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataTable GetDataTable( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataTable GetDataTable( StringBuilder sql, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用DataReader的Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        IDataReader ExecReader( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用DataReader的Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        IDataReader ExecReader( StringBuilder sql, params IDataParameter[] sqlParams );
        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataRow GetDataRow( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        DataRow GetDataRow( StringBuilder sql, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        string ExecScalar( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        T ExecScalar<T>( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        string ExecScalar( StringBuilder sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        T ExecScalar<T>( StringBuilder sql, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        string Lookup( string sql, string columnName, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        T Lookup<T>( string sql, string columnName, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        string Lookup( StringBuilder sql, string columnName, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        T Lookup<T>( StringBuilder sql, string columnName, params IDataParameter[] sqlParams );
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        bool HasData( string sql, params IDataParameter[] sqlParams );

        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        bool HasData( StringBuilder sql, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个递增的数字ID
        /// <summary>
        /// 获取一个递增的数字ID
        /// </summary>
        /// <param name="ID_Field">表的ID字段名</param>
        /// <param name="tableName">表名</param>
        int GetID( string ID_Field, string tableName );
        #endregion

        #region 判断指定数据库是否存在
        /// <summary>
        /// 判断指定数据库是否存在,存在返回true,不存在返回false
        /// </summary>
        /// <param name="databaseName">数据库名</param>        
        bool HasDataBase( string databaseName );
        #endregion

        #region 创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        void CreateDataBase( string databaseName );
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
        bool ExecTransaction( string varName, byte[] buffer, out string errMsg, params string[] sqls );

        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// 该方法用于每条SQL都需要处理二进制数据的情况.
        /// </summary>   
        /// <param name="sqls">多条SQL语句</param>
        /// <param name="varNames">变量名的数组,数组元素的形式：@变量名.注意:数组中的变量名不能重名!</param>
        /// <param name="buffers">二进制数据的数组</param>        
        bool ExecTransaction( string[] sqls, string[] varNames, List<byte[]> buffers );

        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// </summary>
        /// <param name="sql">多条SQL语句</param>
        bool ExecTransaction( params string[] sql );

        /// <summary>
        /// 开始事务。注意1：本方法必须配合ExecSQL和CommitTransaction方法使用.
        /// 注意2:执行完所有SQL后,必须调用CommitTransaction方法,否则会导致数据连接未关闭,后果严重!!!
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务。注意：本方法必须配合BeginTransaction和ExecSQL方法使用.
        /// </summary>
        bool CommitTransaction();
        #endregion

        #region 执行存储过程

        #region 执行增，删，改操作
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数.(执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        int ExecProc( string proc, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        DataSet GetDataSetByProc( string proc, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个DataView集合 
        /// <summary>
        /// 获取一个DataView集合 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        DataView GetDataViewByProc( string proc, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        DataTable GetDataTableByProc( string proc, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用DataReader的Close方法进行关闭，同时将自动关闭数据库连接。
        /// (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        IDataReader ExecReaderByProc( string proc, params IDataParameter[] sqlParams );
        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        DataRow GetDataRowByProc( string proc, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。(执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>        
        string ExecScalarByProc( string proc, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。 (执行存储过程)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>        
        T ExecScalarByProc<T>( string proc, params IDataParameter[] sqlParams );
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        string LookupByProc( string proc, string columnName, params IDataParameter[] sqlParams );

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。(执行存储过程)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        /// <returns></returns>
        T LookupByProc<T>( string proc, string columnName, params IDataParameter[] sqlParams );
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        bool HasDataByProc( string proc, params IDataParameter[] sqlParams );
        #endregion

        #endregion

        #region 创建传入存储过程的参数
        /// <summary>
        /// 创建传入存储过程的参数
        /// </summary>
        /// <param name="paramName">存储过程中的参数名</param>
        /// <param name="type">参数的类型</param>
        /// <param name="value">参数的值</param>        
        DbParameter CreateParam( string paramName, DbType type, object value );

        /// <summary>
        /// 创建传入存储过程的输出参数
        /// </summary>
        /// <param name="paramName">存储过程中的参数名</param>
        /// <param name="type">参数的类型</param>
        DbParameter CreateOutParam( string paramName, DbType type );
        #endregion

        #region 获取存储过程中返回的输出参数
        /// <summary>
        /// 获取存储过程中返回的输出参数
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="paramName">输出参数的名称</param>
        T GetOutParamValue<T>( string paramName );
        #endregion

        #region 更改数据库
        /// <summary>
        /// 更改数据库
        /// </summary>
        /// <param name="database">数据库名称</param>
        void ChangeDataBase( string database );
        #endregion
    }
}





