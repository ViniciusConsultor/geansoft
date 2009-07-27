using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Gean.DataBase
{
    /// <summary>
    /// 操作SQL Server数据库的公共类
    /// </summary>    
    public sealed class SQLHelper : DatabaseHelper
    {
        #region 字段定义
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _connectionString;
        /// <summary>
        /// 连接对象
        /// </summary>
        private SqlConnection _con;
        /// <summary>
        /// 命令对象
        /// </summary>
        private SqlCommand _cmd;
        /// <summary>
        /// 数据集
        /// </summary>
        private DataSet _ds;
        /// <summary>
        /// 数据适配器
        /// </summary>
        private SqlDataAdapter _dataAdapter;
        /// <summary>
        /// 事务对象
        /// </summary>
        private SqlTransaction _transaction;
        /// <summary>
        /// 是否开启事务操作
        /// </summary>
        private bool _isBeginTransaction;
        #endregion

        #region 构造函数
        /// <summary>
        /// 操作SQL Server数据库的公共类的构造函数。
        /// </summary>
        public SQLHelper()
        {
        }

        /// <summary>
        /// 构造函数，对连接字符串进行初始化。
        /// </summary>
        /// <param name="conStr">连接字符串</param>
        public SQLHelper(string conStr)
        {
            _connectionString = conStr;
        }
        #endregion

        #region 获取连接字符串
        /// <summary>
        /// 获取连接字符串,如果没有设置连接字符串，就使用默认的
        /// </summary>
        private string GetConnectionString()
        {
            if (ValidationHelper.IsNullOrEmpty(this._connectionString))
            {
                return _conStr;
            }
            else
            {
                return this._connectionString;
            }
        }
        #endregion

        #region 执行SQL语句

        #region 执行增，删，改操作

        #region ExecSQL重载1
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override int ExecSQL(string sql, params IDataParameter[] sqlParams)
        {
            if (_isBeginTransaction)
            {
                //========================== 使用事务机制 ===========================
                return ExecSQL_Transaction(sql, sqlParams);
            }
            else
            {
                //========================== 不使用事务机制 ===========================
                return ExecSQL_NoTransaction(sql, sqlParams);
            }
        }

        #region ExecSQL重载1( 使用事务机制 )
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        private int ExecSQL_Transaction(string sql, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建命令对象
                _cmd = CreateSQLCommand(_con, _transaction, sql, sqlParams);

                //执行增，删，改操作
                return _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                try
                {
                    //回滚事务
                    _transaction.Rollback();

                    //设置开启事务标识
                    _isBeginTransaction = false;

                    //释放非托管资源
                    if (_con.State == ConnectionState.Open)
                    {
                        _con.Close();
                        _transaction.Dispose();
                    }

                    //记录错误日志
                    LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                    throw ex;
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
            }
        }
        #endregion

        #region ExecSQL重载1( 不使用事务机制 )
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        private int ExecSQL_NoTransaction(string sql, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = CreateSQLCommand(_con, sql, sqlParams);

                    //执行增，删，改操作
                    return _cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }
        #endregion

        #endregion

        #region ExecSQL重载2
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        public override int ExecSQL(string sql, string varName, byte[] buffer)
        {
            if (_isBeginTransaction)
            {
                //========================== 使用事务机制 ===========================
                return ExecSQL_Transaction(sql, varName, buffer);
            }
            else
            {
                //========================== 不使用事务机制 ===========================
                return ExecSQL_NoTransaction(sql, varName, buffer);
            }
        }

        #region ExecSQL重载2( 使用事务机制 )
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        private int ExecSQL_Transaction(string sql, string varName, byte[] buffer)
        {
            try
            {
                //创建命令对象
                _cmd = new SqlCommand(sql, _con, _transaction);

                //给二进制字段赋值
                _cmd.Parameters.Add(varName, SqlDbType.Image);
                _cmd.Parameters[varName].Value = buffer;

                //执行增，删，改操作
                return _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                try
                {
                    //回滚事务
                    _transaction.Rollback();

                    //设置开启事务标识
                    _isBeginTransaction = false;

                    //释放非托管资源
                    if (_con.State == ConnectionState.Open)
                    {
                        _con.Close();
                        _transaction.Dispose();
                    }

                    //记录错误日志
                    LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                    throw ex;
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
            }
        }
        #endregion

        #region ExecSQL重载2( 不使用事务机制 )
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        private int ExecSQL_NoTransaction(string sql, string varName, byte[] buffer)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = new SqlCommand(sql, _con);

                    //给二进制字段赋值
                    _cmd.Parameters.Add(varName, SqlDbType.Image);
                    _cmd.Parameters[varName].Value = buffer;

                    //执行增，删，改操作
                    return _cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }
        #endregion

        #endregion

        #region ExecSQL重载3
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override int ExecSQL(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return ExecSQL(sql.ToString(), sqlParams);
        }
        #endregion

        #region ExecSQL重载4
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句,
        /// 范例：insert into 表(image类型字段名) values(@变量名)</param>
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        public override int ExecSQL(StringBuilder sql, string varName, byte[] buffer)
        {
            return ExecSQL(sql.ToString(), varName, buffer);
        }
        #endregion

        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataSet GetDataSet(string sql, params IDataParameter[] sqlParams)
        {
            //对SQL进行合法性验证，并修改为正确的SQL
            base.GetValidSQL(ref sql);

            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = CreateSQLCommand(_con, sql, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回DataSet
                    return this._ds;
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataSet GetDataSet(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return GetDataSet(sql.ToString(), sqlParams);
        }
        #endregion

        #region 获取一个DataView集合
        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataView GetDataView(string sql, params IDataParameter[] sqlParams)
        {
            //对SQL进行合法性验证，并修改为正确的SQL
            base.GetValidSQL(ref sql);

            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = CreateSQLCommand(_con, sql, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回DataSet
                    return this._ds.Tables[0].DefaultView;
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataView GetDataView(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return GetDataView(sql.ToString(), sqlParams);
        }
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataTable GetDataTable(string sql, params IDataParameter[] sqlParams)
        {
            //对SQL进行合法性验证，并修改为正确的SQL
            base.GetValidSQL(ref sql);

            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = CreateSQLCommand(_con, sql, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回DataSet
                    return this._ds.Tables[0];
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataTable GetDataTable(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return GetDataTable(sql.ToString(), sqlParams);
        }
        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataRow GetDataRow(string sql, params IDataParameter[] sqlParams)
        {
            //对SQL进行合法性验证，并修改为正确的SQL
            base.GetValidSQL(ref sql);

            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = CreateSQLCommand(_con, sql, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回第一行数据
                    if (_ds.Tables[0].Rows.Count != 0)
                    {
                        return _ds.Tables[0].Rows[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override DataRow GetDataRow(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return GetDataRow(sql.ToString(), sqlParams);
        }
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override string ExecScalar(string sql, params IDataParameter[] sqlParams)
        {
            //对SQL进行合法性验证，并修改为正确的SQL
            base.GetValidSQL(ref sql);

            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = CreateSQLCommand(_con, sql, sqlParams);

                    //返回第一行第一列的值
                    if (!ValidationHelper.IsNullOrEmpty(_cmd.ExecuteScalar()))
                    {
                        return _cmd.ExecuteScalar().ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override T ExecScalar<T>(string sql, params IDataParameter[] sqlParams)
        {
            //对SQL进行合法性验证，并修改为正确的SQL
            base.GetValidSQL(ref sql);

            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = CreateSQLCommand(_con, sql, sqlParams);

                    //返回第一行第一列的值
                    return ConvertHelper.ConvertTo<T>(_cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override string ExecScalar(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return ExecScalar(sql.ToString(), sqlParams);
        }

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override T ExecScalar<T>(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return ExecScalar<T>(sql.ToString(), sqlParams);
        }
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override string Lookup(string sql, string columnName, params IDataParameter[] sqlParams)
        {
            //获取数据集
            DataView dv = GetDataView(sql, sqlParams);

            //返回第一行指定列的值
            return dv[0][columnName].ToString();
        }

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override T Lookup<T>(string sql, string columnName, params IDataParameter[] sqlParams)
        {
            //获取数据集
            DataView dv = GetDataView(sql, sqlParams);

            //返回第一行指定列的值
            return ConvertHelper.ConvertTo<T>(dv[0][columnName]);
        }

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override string Lookup(StringBuilder sql, string columnName, params IDataParameter[] sqlParams)
        {
            return Lookup(sql.ToString(), columnName, sqlParams);
        }

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override T Lookup<T>(StringBuilder sql, string columnName, params IDataParameter[] sqlParams)
        {
            return Lookup<T>(sql.ToString(), columnName, sqlParams);
        }
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override bool HasData(string sql, params IDataParameter[] sqlParams)
        {
            //定义变量 
            bool isHasData = false;

            try
            {
                //创建DataReader
                IDataReader reader = ExecReader(sql, sqlParams);

                //判断是否有数据
                isHasData = reader.Read();

                //关闭DataReader
                reader.Close();

                //返回结果
                return isHasData;
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override bool HasData(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return HasData(sql.ToString(), sqlParams);
        }
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override IDataReader ExecReader(string sql, params IDataParameter[] sqlParams)
        {
            //对SQL进行合法性验证，并修改为正确的SQL
            base.GetValidSQL(ref sql);

            try
            {
                //创建连接对象
                _con = new SqlConnection(GetConnectionString());

                //打开连接
                _con.Open();

                //创建命令对象
                _cmd = CreateSQLCommand(_con, sql, sqlParams);

                //获取DataReader对象
                return _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                //记录错误日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + SysHelper.NewLine + "SQL: " + sql);
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个DataReader游标
        /// </summary>
        /// , <par, am name="sql">select语句</param>
        /// <param name="sqlParams">SQL参数集合</param>
        public override IDataReader ExecReader(StringBuilder sql, params IDataParameter[] sqlParams)
        {
            return ExecReader(sql.ToString(), sqlParams);
        }
        #endregion

        #region 获取一个递增的数字ID
        /// <summary>
        /// 获取一个递增的数字ID
        /// </summary>
        /// <param name="ID_Field">表的ID字段名</param>
        /// <param name="tableName">表名</param>      
        public override int GetID(string ID_Field, string tableName)
        {
            //拼接SQL语句
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select isnull( max( {0} ),0) from {1}", ID_Field, tableName);

            //返回ID
            return this.ExecScalar<int>(sql.ToString()) + 1;
        }
        #endregion

        #region 判断指定数据库是否存在
        /// <summary>
        /// 判断指定数据库是否存在,存在返回true,不存在返回false
        /// </summary>
        /// <param name="databaseName">数据库名</param>        
        public override bool HasDataBase(string databaseName)
        {
            string sql = "SELECT name FROM sys.databases WHERE name = N'" + databaseName + "';";
            return HasData(sql);
        }
        #endregion

        #region 创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        public override void CreateDataBase(string databaseName)
        {
            if (!HasDataBase(databaseName))
            {
                string sql = "create database [" + databaseName + "]";
                ExecSQL(sql);
            }
        }
        #endregion

        #endregion

        #region 执行事务

        #region ExecTransaction重载方法1
        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// 注意: 该方法单独使用，不与BeginTransaction和CommitTransaction方法配合。
        /// </summary>
        /// <param name="sqls">多条SQL语句</param>
        public override bool ExecTransaction(params string[] sqls)
        {
            //创建连接对象
            using (_con = new SqlConnection(GetConnectionString()))
            {
                //打开连接
                _con.Open();

                //开始事务
                _transaction = _con.BeginTransaction();

                //创建命令对象
                _cmd = _con.CreateCommand();
                //为命令对象指定连接和事务
                _cmd.Connection = _con;
                _cmd.Transaction = _transaction;

                //执行事务
                try
                {
                    //循环执行SQL语句
                    for (int i = 0; i < sqls.Length; i++)
                    {
                        //如果取出的sql是空的,就执行下一条
                        if (string.IsNullOrEmpty(sqls[i]))
                        {
                            continue;
                        }

                        //对SQL进行合法性验证，并修改为正确的SQL
                        base.GetValidSQL(ref sqls[i]);

                        //执行SQL语句
                        _cmd.CommandText = sqls[i];
                        _cmd.ExecuteNonQuery();
                    }

                    //提交事务
                    _transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        //回滚事务
                        _transaction.Rollback();

                        //记录错误日志
                        LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + " \r\nSQL: " + sqls[0]);
                        throw ex;
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
            }
        }
        #endregion

        #region ExecTransaction重载方法2
        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// 该方法用于执行只有一条SQL需要处理二进制数据的情况.
        /// 注意: 该方法单独使用，不与BeginTransaction和CommitTransaction方法配合。
        /// </summary>        
        /// <param name="varName">变量名,形式：@变量名</param>
        /// <param name="buffer">二进制数据</param>
        /// <param name="errMsg">如果执行失败,返回的错误消息</param>
        /// <param name="sqls">多条SQL语句</param>
        public override bool ExecTransaction(string varName, byte[] buffer, out string errMsg, params string[] sqls)
        {
            //创建连接对象
            using (_con = new SqlConnection(GetConnectionString()))
            {
                //打开连接
                _con.Open();

                //开始事务
                _transaction = _con.BeginTransaction();

                //创建命令对象
                _cmd = _con.CreateCommand();
                //为命令对象指定连接和事务
                _cmd.Connection = _con;
                _cmd.Transaction = _transaction;

                //执行事务
                try
                {
                    //循环执行SQL语句
                    for (int i = 0; i < sqls.Length; i++)
                    {
                        //如果取出的sql是空的,就执行下一条
                        if (string.IsNullOrEmpty(sqls[i]))
                        {
                            continue;
                        }

                        //对SQL进行合法性验证，并修改为正确的SQL
                        base.GetValidSQL(ref sqls[i]);

                        //sql语句
                        string sqlStr = sqls[i];

                        //查找存在"@"字符的sql,表示该SQL需要处理二进制值
                        if (sqlStr.IndexOf("@") != -1)
                        {
                            //给二进制字段赋值
                            _cmd.Parameters.Add(varName, SqlDbType.Image);
                            _cmd.Parameters[varName].Value = buffer;
                        }
                        _cmd.CommandText = sqlStr;

                        //执行SQL
                        _cmd.ExecuteNonQuery();
                    }

                    //提交事务
                    _transaction.Commit();

                    //设置错误消息
                    errMsg = "执行成功";
                    return true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        //回滚事务
                        _transaction.Rollback();

                        //设置错误消息
                        errMsg = ex.Message;

                        //记录错误日志
                        LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + " \r\nSQL: " + sqls[0]);
                        throw ex;
                    }
                    catch (Exception ex2)
                    {
                        //设置错误消息
                        errMsg = ex.Message + "," + ex2.Message;
                        throw ex2;
                    }
                }
            }
        }
        #endregion

        #region ExecTransaction重载方法3
        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// 该方法用于每条SQL都需要处理二进制数据的情况.
        /// 注意: 该方法单独使用，不与BeginTransaction和CommitTransaction方法配合。
        /// </summary>   
        /// <param name="sqls">多条SQL语句</param>
        /// <param name="varNames">变量名的数组,数组元素的形式：@变量名.注意:数组中的变量名不能重名!</param>
        /// <param name="buffers">二进制数据的数组</param>        
        public override bool ExecTransaction(string[] sqls, string[] varNames, List<byte[]> buffers)
        {
            //创建连接对象
            using (_con = new SqlConnection(GetConnectionString()))
            {
                //打开连接
                _con.Open();

                //开始事务
                _transaction = _con.BeginTransaction();

                //创建命令对象
                _cmd = _con.CreateCommand();
                //为命令对象指定连接和事务
                _cmd.Connection = _con;
                _cmd.Transaction = _transaction;

                //执行事务
                try
                {
                    //循环执行SQL语句
                    for (int i = 0; i < sqls.Length; i++)
                    {
                        //如果取出的sql是空的,就执行下一条
                        if (string.IsNullOrEmpty(sqls[i]))
                        {
                            continue;
                        }

                        //对SQL进行合法性验证，并修改为正确的SQL
                        base.GetValidSQL(ref sqls[i]);

                        //给二进制字段赋值
                        _cmd.Parameters.Add(varNames[i], SqlDbType.Image);
                        _cmd.Parameters[varNames[i]].Value = buffers[i];
                        //设置SQL
                        _cmd.CommandText = sqls[i];

                        //执行SQL
                        _cmd.ExecuteNonQuery();
                    }

                    //提交事务
                    _transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        //回滚事务
                        _transaction.Rollback();
                        //记录错误日志
                        LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message + " \r\nSQL: " + sqls[0]);
                        throw ex;
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
            }
        }
        #endregion

        #region 开始事务
        /// <summary>
        /// 开始事务。注意：本方法必须配合ExecSQL和CommitTransaction方法使用.
        /// </summary>
        public override void BeginTransaction()
        {
            try
            {
                //创建连接对象
                _con = new SqlConnection(GetConnectionString());

                //打开连接
                if (_con.State == ConnectionState.Closed)
                {
                    _con.Open();
                }

                //开始事务
                _transaction = _con.BeginTransaction();

                //设置开启事务标识
                _isBeginTransaction = true;
            }
            catch (Exception ex)
            {
                //写日志
                LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);

                //释放非托管资源
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                    _transaction.Dispose();
                }

                //设置开启事务标识
                _isBeginTransaction = false;

                throw ex;
            }
        }
        #endregion

        #region 提交事务
        /// <summary>
        /// 提交事务,本方法必须配合BeginTransaction和ExecSQL方法使用.
        /// </summary>
        public override bool CommitTransaction()
        {
            try
            {
                //提交事务
                if (!ValidationHelper.IsNullOrEmpty<SqlTransaction>(_transaction))
                {
                    _transaction.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    //回滚事务
                    _transaction.Rollback();

                    //记录错误日志
                    LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                    throw ex;
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
            }
            finally
            {
                //设置开启事务标识
                _isBeginTransaction = false;

                //释放非托管资源
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                    _transaction.Dispose();
                }
            }
        }
        #endregion

        #endregion

        #region 执行存储过程

        #region 执行增，删，改操作
        /// <summary>
        /// 执行增，删，改操作,返回影响的行数
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override int ExecProc(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = this.CreateCommand(_con, proc, sqlParams);

                    //执行增，删，改操作
                    return _cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override DataSet GetDataSetByProc(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = this.CreateCommand(_con, proc, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回DataSet
                    return this._ds;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取一个DataView集合
        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override DataView GetDataViewByProc(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = this.CreateCommand(_con, proc, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回DataSet
                    return this._ds.Tables[0].DefaultView;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override DataTable GetDataTableByProc(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = this.CreateCommand(_con, proc, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回DataSet
                    return this._ds.Tables[0];
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用DataReader的Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override IDataReader ExecReaderByProc(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                _con = new SqlConnection(GetConnectionString());

                //打开连接
                _con.Open();

                //创建命令对象
                _cmd = this.CreateCommand(_con, proc, sqlParams);

                //获取DataReader对象
                return _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override DataRow GetDataRowByProc(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = this.CreateCommand(_con, proc, sqlParams);

                    //填充数据集
                    FillDataSet();

                    //返回第一行数据
                    if (_ds.Tables[0].Rows.Count != 0)
                    {
                        return _ds.Tables[0].Rows[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>        
        public override string ExecScalarByProc(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = this.CreateCommand(_con, proc, sqlParams);

                    //返回第一行第一列的值
                    return _cmd.ExecuteScalar().ToString();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override T ExecScalarByProc<T>(string proc, params IDataParameter[] sqlParams)
        {
            try
            {
                //创建连接对象
                using (_con = new SqlConnection(GetConnectionString()))
                {
                    //打开连接
                    _con.Open();

                    //创建命令对象
                    _cmd = this.CreateCommand(_con, proc, sqlParams);

                    //返回第一行第一列的值
                    return ConvertHelper.ConvertTo<T>(_cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override string LookupByProc(string proc, string columnName, params IDataParameter[] sqlParams)
        {
            //获取数据集
            DataView dv = GetDataViewByProc(proc, sqlParams);

            //返回第一行指定列的值
            return dv[0][columnName].ToString();
        }

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        /// <returns></returns>
        public override T LookupByProc<T>(string proc, string columnName, params IDataParameter[] sqlParams)
        {
            //获取数据集
            DataView dv = GetDataViewByProc(proc, sqlParams);

            //返回第一行指定列的值
            return ConvertHelper.ConvertTo<T>(dv[0][columnName]);
        }
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams">存储过程的参数集合</param>
        public override bool HasDataByProc(string proc, params IDataParameter[] sqlParams)
        {
            //定义变量 
            bool isHasData = false;

            try
            {
                //创建DataReader
                IDataReader reader = ExecReaderByProc(proc, sqlParams);

                //判断是否有数据
                isHasData = reader.Read();

                //关闭DataReader
                reader.Close();

                //返回结果
                return isHasData;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region 填充数据集
        /// <summary>
        /// 填充数据集
        /// </summary>        
        private void FillDataSet()
        {
            //初始化数据集
            this._ds = new DataSet();

            //初始化数据适配器
            this._dataAdapter = new SqlDataAdapter(_cmd);

            //填充数据集
            this._dataAdapter.Fill(_ds);
        }
        #endregion

        #region 创建一个用于存储过程的命令对象
        /// <summary>
        /// 创建一个用于存储过程的命令对象
        /// </summary>
        /// <param name="con">数据库连接</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        private SqlCommand CreateCommand(SqlConnection con, string procName, params IDataParameter[] parameters)
        {
            //创建命令对象
            SqlCommand command = new SqlCommand(procName, con);

            //设置命令类型为存储过程
            command.CommandType = CommandType.StoredProcedure;

            //将参数添加到命令对象中
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// 创建一个用于执行SQL参数的命令对象
        /// </summary>
        /// <param name="con">数据库连接</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        private SqlCommand CreateSQLCommand(SqlConnection con, string sql, params IDataParameter[] parameters)
        {
            //创建命令对象
            SqlCommand command = new SqlCommand(sql, con);

            //设置命令类型为存储过程
            command.CommandType = CommandType.Text;

            //将参数添加到命令对象中
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// 创建一个用于执行事务的命令对象
        /// </summary>
        /// <param name="con">数据库连接</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        private SqlCommand CreateSQLCommand(SqlConnection con, SqlTransaction transaction, string sql, params IDataParameter[] parameters)
        {
            //创建命令对象
            SqlCommand command = new SqlCommand(sql, con, transaction);

            //设置命令类型为存储过程
            command.CommandType = CommandType.Text;

            //将参数添加到命令对象中
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
        #endregion

        #region 创建传入存储过程的参数
        /// <summary>
        /// 创建传入存储过程的参数
        /// </summary>
        /// <param name="paramName">存储过程中的参数名,形式： @参数名 </param>
        /// <param name="type">参数的类型</param>
        /// <param name="value">参数的值</param>        
        public override DbParameter CreateParam(string paramName, DbType type, object value)
        {
            //创建一个SqlParameter对象
            SqlParameter param = new SqlParameter();
            //设置参数名
            param.ParameterName = paramName;
            //设置参数类型
            param.DbType = type;
            //设置参数的值
            param.Value = value;

            //返回参数
            return param;
        }

        /// <summary>
        /// 创建传入存储过程的输出参数
        /// </summary>
        /// <param name="paramName">存储过程中的参数名</param>
        /// <param name="type">参数的类型</param>
        public override DbParameter CreateOutParam(string paramName, DbType type)
        {
            //创建一个SqlParameter对象
            SqlParameter param = new SqlParameter();
            //设置参数名
            param.ParameterName = paramName;
            //设置参数类型
            param.DbType = type;
            //设置参数的方向 
            param.Direction = ParameterDirection.Output;

            //返回参数
            return param;
        }
        #endregion

        #region 获取存储过程中返回的输出参数
        /// <summary>
        /// 获取存储过程中返回的输出参数
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="paramName">输出参数的名称</param>
        public override T GetOutParamValue<T>(string paramName)
        {
            //返回输出参数
            return ConvertHelper.ConvertTo<T>(this._cmd.Parameters[paramName].Value);
        }
        #endregion

        #region 更改数据库
        /// <summary>
        /// 更改数据库
        /// </summary>
        /// <param name="database">数据库名称</param>
        public override void ChangeDataBase(string database)
        {
            _con.ChangeDatabase(database);
        }
        #endregion
    }
}