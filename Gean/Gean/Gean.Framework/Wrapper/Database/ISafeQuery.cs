using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gean.Data
{
    /// <summary>
    /// 增强数据库操作接口
    /// </summary>    
    public interface ISafeQuery : IQuery
    {
        #region 执行SQL语句

        #region 执行增，删，改操作
        /// <summary>
        /// 执行增，删，改操作,成功返回true
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        bool ExecSQL(string sql);

        /// <summary>
        /// 执行增，删，改操作,成功返回true,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="paramName">参数名,不要添加变量前缀</param>
        /// <param name="buffer">二进制数据</param>
        bool ExecSQL(string sql, string paramName, byte[] buffer);

        /// <summary>
        /// 执行增，删，改操作,成功返回true
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        bool ExecSQL(StringBuilder sql);

        /// <summary>
        /// 执行增，删，改操作,成功返回true,该方法用于处理二进制数据的操作
        /// </summary>
        /// <param name="sql">要执行的insert,update,delete语句</param>
        /// <param name="paramName">参数名,不要添加变量前缀</param>
        /// <param name="buffer">二进制数据</param>
        bool ExecSQL(StringBuilder sql, string paramName, byte[] buffer);

        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        DataSet GetDataSet(string sql);

        /// <summary>
        /// 获取一个DataSet集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        DataSet GetDataSet(StringBuilder sql);
        #endregion

        #region 获取一个DataView集合
        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        DataView GetDataView(string sql);

        /// <summary>
        /// 获取一个DataView集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        DataView GetDataView(StringBuilder sql);
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        DataTable GetDataTable(string sql);

        /// <summary>
        /// 获取一个DataTable集合
        /// </summary>
        /// <param name="sql">执行的select语句</param>
        DataTable GetDataTable(StringBuilder sql);
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="sql">select语句</param>
        IDataReader ExecReader(string sql);

        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="sql">select语句</param>
        IDataReader ExecReader(StringBuilder sql);
        #endregion

        #region 获取一个SafeDataReader增强游标
        /// <summary>
        /// 获取一个SafeDataReader增强游标,使用完成后必须调用Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="sql">sql语句</param>
        SafeDataReader GetSafeReader(string sql);

        /// <summary>
        /// 获取一个SafeDataReader增强游标,使用完成后必须调用Close方法进行关闭，同时将自动关闭数据库连接。
        /// </summary>
        /// <param name="sql">sql语句</param>
        SafeDataReader GetSafeReader(StringBuilder sql);

        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        DataRow GetDataRow(string sql);

        /// <summary>
        /// 获取第一行数据
        /// </summary>
        /// <param name="sql">select语句</param>
        DataRow GetDataRow(StringBuilder sql);
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        object ExecScalar(string sql);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        T ExecScalar<T>(string sql);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        object ExecScalar(StringBuilder sql);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">该SQL语句只应返回一个值，如下形式：select 列名 from ...</param>
        T ExecScalar<T>(StringBuilder sql);
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        string Lookup(string sql, string columnName);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        T Lookup<T>(string sql, string columnName);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        string Lookup(StringBuilder sql, string columnName);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">select语句</param>
        /// <param name="columnName">列名</param>
        T Lookup<T>(StringBuilder sql, string columnName);
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        bool HasData(string sql);

        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false
        /// </summary>
        /// <param name="sql">select语句</param>
        bool HasData(StringBuilder sql);
        #endregion

        #region 获取数据集的行数
        /// <summary>
        /// 获取数据集的行数
        /// </summary>
        /// <param name="sql">select语句</param>
        int GetRowCountBySql(string sql);

        /// <summary>
        /// 获取数据集的行数
        /// </summary>
        /// <param name="sql">select语句</param>
        int GetRowCountBySql(StringBuilder sql);

        /// <summary>
        /// 获取数据集的行数
        /// </summary>
        /// <param name="tableName">表名或视图名</param>
        /// <param name="conditions">where条件，不要传入where关键字.
        /// 如果有多个条件，可分多次传入，默认通过and连接。
        /// 也可一次传入多个条件，必须指定连接运算符。</param>
        int GetRowCount(string tableName, params string[] conditions);
        #endregion

        #region 获取一个递增的数字ID
        /// <summary>
        /// 获取一个递增的数字ID
        /// </summary>
        /// <param name="idName">ID字段名</param>
        /// <param name="tableName">表名</param>
        int GetID(string idName, string tableName);
        #endregion

        #region 获取指定父节点的所有子节点
        /// <summary>
        /// 获取指定父节点下的所有子节点( 包括该父节点 ),
        /// 返回所有列和该节点的级数。
        /// 注意：级数字段名为 level ,父节点的级数为0,依次类推
        /// </summary>
        /// <typeparam name="T">子字段和父字段的数据类型，只能是int或Guid</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="columnParent">父字段名</param>
        /// <param name="columnChild">子字段名,不一定是主键，但子字段应能唯一标识该行</param>
        /// <param name="valueParent">父字段的值</param>
        DataTable GetChilds<T>(string tableName, string columnParent, string columnChild, T valueParent);
        #endregion

        #region 获取指定子节点的所有父节点
        /// <summary>
        /// 获取指定子节点的所有父节点( 包括该子节点 ),
        /// 返回所有列和该节点的级数。
        /// 注意：级数字段名为 level ,顶级节点的级数为0,依次类推
        /// </summary>
        /// <typeparam name="T">子字段和父字段的数据类型，只能是int或Guid</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="columnParent">父字段名</param>
        /// <param name="columnChild">子字段名,不一定是主键，但子字段应能唯一标识该行</param>
        /// <param name="valueChild">子字段的值</param>
        DataTable GetParents<T>(string tableName, string columnParent, string columnChild, T valueChild);
        #endregion

        #region 获取一个整数
        /// <summary>
        /// 获取一个整数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        int GetInt32(string sql);

        /// <summary>
        /// 获取一个整数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        int GetInt32(StringBuilder sql);
        #endregion

        #region 获取一个浮点值
        /// <summary>
        /// 获取一个浮点值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        double GetDouble(string sql);

        /// <summary>
        /// 获取一个浮点值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        double GetDouble(StringBuilder sql);
        #endregion

        #region 获取一个decimal值
        /// <summary>
        /// 获取一个decimal值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        decimal GetDecimal(string sql);

        /// <summary>
        /// 获取一个decimal值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        decimal GetDecimal(StringBuilder sql);
        #endregion

        #region 获取一个字符串值
        /// <summary>
        /// 获取一个字符串值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        string GetString(string sql);

        /// <summary>
        /// 获取一个字符串值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        string GetString(StringBuilder sql);
        #endregion

        #region 获取一个日期值
        /// <summary>
        /// 获取一个日期值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        DateTime GetDateTime(string sql);

        /// <summary>
        /// 获取一个日期值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        DateTime GetDateTime(StringBuilder sql);
        #endregion

        #region 获取一个SmartDate值
        /// <summary>
        /// 获取一个SmartDate值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        SmartDate GetSmartDate(string sql);

        /// <summary>
        /// 获取一个SmartDate值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        SmartDate GetSmartDate(StringBuilder sql);
        #endregion

        #region 获取一个GUID值
        /// <summary>
        /// 获取一个GUID值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        Guid GetGuid(string sql);

        /// <summary>
        /// 获取一个GUID值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        Guid GetGuid(StringBuilder sql);
        #endregion

        #region 获取一个枚举值
        /// <summary>
        /// 获取一个枚举值
        /// </summary>        
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="sql">SQL语句</param>
        T GetEnum<T>(string sql);

        /// <summary>
        /// 获取一个枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="sql">SQL语句</param>
        T GetEnum<T>(StringBuilder sql);
        #endregion

        #region 获取一个布尔值
        /// <summary>
        /// 获取一个布尔值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        bool GetBoolean(string sql);

        /// <summary>
        /// 获取一个布尔值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        bool GetBoolean(StringBuilder sql);
        #endregion

        #region 获取一个字节流
        /// <summary>
        /// 获取一个字节流
        /// </summary>
        /// <param name="sql">SQL语句</param>
        byte[] GetBytes(string sql);

        /// <summary>
        /// 获取一个字节流
        /// </summary>
        /// <param name="sql">SQL语句</param>
        byte[] GetBytes(StringBuilder sql);
        #endregion

        #endregion

        #region 执行事务

        #region 执行事务
        /// <summary>
        /// 执行事务,传入的多条SQL语句,要么全部执行成功,要么全部执行失败,执行成功返回true.
        /// 注意1: 该方法单独使用，不与BeginTransaction和CommitTransaction方法配合。
        /// 注意2：该方法不能执行带参数的SQL.
        /// </summary>
        /// <param name="sql">多条SQL语句</param>
        bool ExecTransaction(params string[] sql);
        #endregion

        #region 开始事务
        /// <summary>
        /// 开始事务。注意：本方法必须配合CommitTransaction方法使用.
        /// 执行完所有SQL后,必须调用CommitTransaction方法,否则会导致数据连接未关闭,后果严重!!!
        /// </summary>
        void BeginTransaction();
        #endregion

        #region 提交事务
        /// <summary>
        /// 提交事务。注意：本方法必须配合BeginTransaction使用.
        /// </summary>
        bool CommitTransaction();
        #endregion

        #region 回滚事务
        /// <summary>
        /// 回滚事务。
        /// </summary>
        void RollbackTransaction();
        #endregion

        #endregion

        #region 执行存储过程

        #region 执行增，删，改操作
        /// <summary>
        /// 执行增，删，改操作,成功返回true.(执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        bool ExecProc(string proc);
        #endregion

        #region 获取一个DataSet集合
        /// <summary>
        /// 获取一个DataSet集合 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        DataSet GetDataSetByProc(string proc);
        #endregion

        #region 获取一个DataView集合
        /// <summary>
        /// 获取一个DataView集合 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        DataView GetDataViewByProc(string proc);
        #endregion

        #region 获取一个DataTable集合
        /// <summary>
        /// 获取一个DataTable集合 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        DataTable GetDataTableByProc(string proc);
        #endregion

        #region 获取一个DataReader游标
        /// <summary>
        /// 获取一个DataReader游标,使用完成后必须调用DataReader的Close方法进行关闭，同时将自动关闭数据库连接。
        /// (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        IDataReader ExecReaderByProc(string proc);
        #endregion

        #region 获取第一行数据
        /// <summary>
        /// 获取第一行数据 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        DataRow GetDataRowByProc(string proc);
        #endregion

        #region 获取一个单值，该值是第一行第一列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。(执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        object ExecScalarByProc(string proc);

        /// <summary>
        /// 获取一个单值，该值是第一行第一列的值。 (执行存储过程)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        T ExecScalarByProc<T>(string proc);
        #endregion

        #region 获取一个单值，该值是第一行指定列的值。
        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。 (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        string LookupByProc(string proc, string columnName);

        /// <summary>
        /// 获取一个单值，该值是第一行指定列的值。(执行存储过程)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="columnName">列名</param>
        T LookupByProc<T>(string proc, string columnName);
        #endregion

        #region 判断查询结果是否有数据
        /// <summary>
        /// 判断查询结果是否有数据，有数据返回true,无数据返回false (执行存储过程)
        /// </summary>
        /// <param name="proc">存储过程名</param>
        bool HasDataByProc(string proc);
        #endregion

        #endregion

        #region 参数

        #region 添加输入参数
        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="paramName">参数名,不要添加变量前缀</param>
        /// <param name="type">参数的类型</param>
        /// <param name="value">参数的值</param>
        void AddInParam(string paramName, DbType type, object value);
        #endregion

        #region 添加输出参数
        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="paramName">参数名,不要添加变量前缀</param>
        /// <param name="type">参数的类型</param>
        /// <param name="paramSize">参数的最大长度</param>
        void AddOutParam(string paramName, DbType type, int paramSize);
        #endregion

        #region 添加返回参数
        /// <summary>
        /// 添加返回参数
        /// </summary>
        void AddReturnParam();
        #endregion

        #region 获取输出参数
        /// <summary>
        /// 获取输出参数( 存储过程中out参数 )
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="paramName">参数名,不要添加变量前缀</param>
        T GetOutParamValue<T>(string paramName);
        #endregion

        #region 获取存储过程中的返回值
        /// <summary>
        /// 获取返回值( 由return返回的值 )
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        T GetReturnValue<T>();
        #endregion

        #region 清除输入参数
        /// <summary>
        /// 清除输入参数
        /// </summary>
        void ClearInParams();
        #endregion

        #endregion

        #region 数据库管理

        #region 更改数据库
        /// <summary>
        /// 更改数据库
        /// </summary>
        /// <param name="database">数据库名称</param>
        void ChangeDataBase(string database);
        #endregion

        #region 判断指定数据库是否存在
        /// <summary>
        /// 判断指定数据库是否存在,存在返回true,不存在返回false
        /// </summary>
        /// <param name="databaseName">数据库名</param>        
        bool HasDataBase(string databaseName);
        #endregion

        #region 创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        void CreateDataBase(string databaseName);
        #endregion

        #region 获取所有数据库名称的列表
        /// <summary>
        /// 获取所有数据库名称的列表
        /// </summary>
        string[] GetDatabaseNames();
        #endregion

        #region 检测数据表是否存在
        /// <summary>
        /// 检测数据表是否存在
        /// </summary>
        /// <param name="tableName">数据表的名称</param>
        bool HasTable(string tableName);
        #endregion

        #endregion
    }
}