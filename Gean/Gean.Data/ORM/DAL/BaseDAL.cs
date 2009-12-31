using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Gean.Data.Entity;
using System.Data.SqlClient;

namespace Gean.Data.DAL
{
    /// <summary>
    /// BaseDAL 的摘要说明。
    /// </summary>
    public abstract class BaseDAL : IDatabase, ICommonDAL
    {

        #region 基类变量和构造函数

        /// <summary>
        /// 需要初始化的对象表名
        /// </summary>
        public virtual string TableName { get; protected set; }

        /// <summary>
        /// 数据库的主键字段名
        /// </summary>
        public virtual string PrimaryKey { get; protected set; }

        public BaseDAL()
        { }

        /// <summary>
        /// 指定表名以及主键,对基类进构造
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">表主键</param>
        public BaseDAL(string tableName, string primaryKey)
        {
            this.TableName = tableName;
            this.PrimaryKey = primaryKey;
        }

        #endregion

        #region 通用操作方法

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        protected int Insert(Hashtable recordField, IDbTransaction trans)
        {
            return this.Insert(recordField, TableName, trans);
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        protected int Insert(Hashtable recordField, string targetTable, IDbTransaction trans)
        {
            int iret = 0;
            string fields = ""; // 字段名
            string vals = ""; // 字段值
            if (recordField == null || recordField.Count < 1)
            {
                return iret;
            }

            SqlParameter[] param = new SqlParameter[recordField.Count];
            IEnumerator eKeys = recordField.Keys.GetEnumerator();

            int i = 0;
            while (eKeys.MoveNext())
            {
                string field = eKeys.Current.ToString();
                fields += field + ",";
                vals += string.Format("@{0},", field);
                object val = recordField[eKeys.Current.ToString()];
                param[i] = new SqlParameter("@" + field, val);

                i++;
            }

            fields = fields.Trim(',');//除去前后的逗号
            vals = vals.Trim(',');//除去前后的逗号
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2}); SELECT SCOPE_IDENTITY() AS ID", targetTable, fields, vals);

            if (trans != null)
            {
                object obj = this.AdoHelper.ExecuteScalar(trans, CommandType.Text, sql, param);
                iret = int.Parse(obj.ToString());
            }
            else
            {
                object obj = this.AdoHelper.ExecuteScalar(this.Connection, CommandType.Text, sql, param);
                iret = int.Parse(obj.ToString());
            }

            return iret;
        }

        /// <summary>
        /// 更新某个表一条记录(只适用于用单键,用int类型作键值的表)
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        protected int Update(int id, Hashtable recordField, IDbTransaction trans)
        {
            return this.Update(id, recordField, TableName, trans);
        }

        /// <summary>
        /// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        protected int Update(string id, Hashtable recordField, IDbTransaction trans)
        {
            return this.Update(id, recordField, TableName, trans);
        }

        /// <summary>
        /// 更新某个表一条记录(只适用于用单键,用int类型作键值的表)
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        protected int Update(int id, Hashtable recordField, string targetTable, IDbTransaction trans)
        {
            return Update(id, recordField, targetTable, trans);
        }

        /// <summary>
        /// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        protected int Update(string id, Hashtable recordField, string targetTable, IDbTransaction trans)
        {
            string field = ""; // 字段名
            object val = null; // 值
            string setValue = ""; // 更新Set () 中的语句

            if (recordField == null || recordField.Count < 1)
            {
                return 0;
            }

            SqlParameter[] param = new SqlParameter[recordField.Count];
            int i = 0;

            IEnumerator eKeys = recordField.Keys.GetEnumerator();
            while (eKeys.MoveNext())
            {
                field = eKeys.Current.ToString();
                val = recordField[eKeys.Current.ToString()];
                setValue += string.Format("{0} = @{0},", field);
                param[i] = new SqlParameter(string.Format("@{0}", field), val);

                i++;
            }

            string sql = string.Format("UPDATE {0} SET {1} WHERE {2} = '{3}' ", targetTable, setValue.Substring(0, setValue.Length - 1), PrimaryKey, id);

            int count;
            if (trans != null)
            {
                count = this.AdoHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
            }
            else
            {
                count = this.AdoHelper.ExecuteNonQuery(this.Connection, CommandType.Text, sql, param);
            }

            return count;
        }


        #endregion

        #region 对象添加、修改、查询接口

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        protected virtual int Insert(BaseEntity obj)
        {
            Checker.CheckForNullReference(obj, "传入的对象obj为空");

            Hashtable hash = GetHashByEntity(obj);
            return Insert(hash, null);
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        protected virtual bool Update(BaseEntity obj)
        {
            Checker.CheckForNullReference(obj, "传入的对象obj为空");

            Hashtable hash = GetHashByEntity(obj);
            int count = Update(obj.PrimaryKey, hash, null);
            return UtilityConvert.IntToBoolean(count, UtilityConvert.ConvertMode.Relaxed);
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于整型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        protected virtual BaseEntity BaseFindByID(int key)
        {
            return BaseFindByID(key.ToString());
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于字符型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        protected virtual BaseEntity BaseFindByID(string key)
        {
            string sql = string.Format("Select * From dbo.{0} Where ({1} = @ID)", TableName, PrimaryKey);

            SqlParameter param = new SqlParameter("@ID", key);

            BaseEntity entity = null;
            using (IDataReader dr = this.AdoHelper.ExecuteReader(this.ConnectionString, CommandType.Text, sql, param))
            {
                if (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                }
            }
            return entity;
        }

        #endregion

        #region 返回集合的接口

        /// <summary>
        /// 根据ID字符串(逗号分隔)获取对象列表
        /// </summary>
        /// <param name="idString">ID字符串(逗号分隔)</param>
        /// <returns>符合条件的对象列表</returns>
        protected virtual ArrayList BaseFindByIDs(string idString)
        {
            string condition = string.Format("{0} in({1})", PrimaryKey, idString);
            return this.BaseFind(condition);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        protected virtual ArrayList BaseFind(string condition)
        {
            //串连条件语句为一个完整的Sql语句
            string sql = string.Format("Select * From dbo.{0} Where ", TableName);
            sql += condition;

            object entity = null;
            ArrayList list = new ArrayList();

            using (IDataReader dr = this.AdoHelper.ExecuteReader(this.ConnectionString, CommandType.Text, sql))
            {
                while (dr.Read())
                {
                    entity = DataReaderToEntity(dr);

                    list.Add(entity);
                }
            }
            return list;
        }

        ///// <summary>
        ///// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        ///// </summary>
        ///// <param name="condition">查询的条件</param>
        ///// <param name="info">分页实体</param>
        ///// <returns>指定对象的集合</returns>
        //protected virtual ArrayList BaseFind(string condition, PagerInfo info)
        //{
        //    ArrayList list = new ArrayList();

        //    PagerHelper helper = new PagerHelper(_tableName, condition, ConnectionString);
        //    info.RecordCount = helper.GetCount();

        //    PagerHelper helper2 = new PagerHelper(_tableName, false, " * ", _primaryKey,
        //        info.PageSize, info.CurrenetPageIndex, false, condition, ConnectionString);

        //    using (IDataReader dr = helper2.GetDataReader())
        //    {
        //        while (dr.Read())
        //        {
        //            list.Add(this.DataReaderToEntity(dr));
        //        }
        //    }
        //    return list;
        //}

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns>指定对象的集合</returns>
        protected virtual ArrayList BaseGetAll()
        {
            string sql = string.Format("Select * From dbo.{0}", TableName);

            object entity = null;
            ArrayList list = new ArrayList();

            using (IDataReader dr = this.AdoHelper.ExecuteReader(this.ConnectionString, CommandType.Text, sql))
            {
                while (dr.Read())
                {
                    entity = DataReaderToEntity(dr);

                    list.Add(entity);
                }
            }
            return list;
        }

        ///// <summary>
        ///// 返回数据库所有的对象集合(用于分页数据显示)
        ///// </summary>
        ///// <param name="info">分页实体信息</param>
        ///// <returns>指定对象的集合</returns>
        //protected virtual ArrayList BaseGetAll(PagerInfo info)
        //{
        //    ArrayList list = new ArrayList();
        //    string condition = "";

        //    PagerHelper helper = new PagerHelper(_tableName, condition, ConnectionString);
        //    info.RecordCount = helper.GetCount();

        //    PagerHelper helper2 = new PagerHelper(_tableName, false, " * ", _primaryKey,
        //        info.PageSize, info.CurrenetPageIndex, false, condition, ConnectionString);

        //    using (IDataReader dr = helper2.GetDataReader())
        //    {
        //        while (dr.Read())
        //        {
        //            list.Add(this.DataReaderToEntity(dr));
        //        }
        //    }
        //    return list;
        //}

        #endregion

        #region 子类必须实现的函数(用于更新或者插入)

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected abstract BaseEntity DataReaderToEntity(IDataReader dr);

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值(用于插入或者更新操作)
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected abstract Hashtable GetHashByEntity(BaseEntity obj);

        #endregion

        #region IDatabase 成员

        public string ConnectionString
        {
            get { throw new NotImplementedException(); }
        }

        public IDbConnection Connection
        {
            get { throw new NotImplementedException(); }
        }

        public IAdoHelper AdoHelper
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICommonDAL 成员

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="recordTable">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(Hashtable recordTable)
        {
            SqlParameter[] param = new SqlParameter[recordTable.Count];
            IEnumerator eKeys = recordTable.Keys.GetEnumerator();

            string fields = "";// 字段名
            int i = 0;

            while (eKeys.MoveNext())
            {
                string field = eKeys.Current.ToString();
                fields += string.Format(" {0} = @{1} AND", field, field);

                string val = recordTable[eKeys.Current.ToString()].ToString();
                param[i] = new SqlParameter(string.Format("@{0}", field), val);

                i++;
            }

            fields = fields.Substring(0, fields.Length - 3);//除去最后的AND
            string sql = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", TableName, fields);

            int count = (int)this.AdoHelper.ExecuteScalar(this.ConnectionString, CommandType.Text, sql, param);
            return UtilityConvert.IntToBoolean(count, UtilityConvert.ConvertMode.Relaxed);
        }


        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(string fieldName, object key)
        {
            Hashtable table = new Hashtable();
            table.Add(fieldName, key);

            return IsExistKey(table);
        }


        /// <summary>
        /// 获取数据库中该对象的最大ID值
        /// </summary>
        /// <returns>最大ID值</returns>
        public virtual int GetMaxID()
        {
            string sql = string.Format("SELECT MAX({0}) AS MaxID FROM dbo.{1}", PrimaryKey, TableName);

            object obj = this.AdoHelper.ExecuteScalar(this.ConnectionString, CommandType.Text, sql);
            if (Convert.IsDBNull(obj))
            {
                return 0;//没有记录的时候为0
            }
            return Convert.ToInt32(obj);
        }


        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(int key)
        {
            string condition = string.Format("{0} = '{1}'", PrimaryKey, key);
            return Delete(condition);
        }


        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(string condition)
        {
            string sql = string.Format("DELETE FROM dbo.{0} WHERE {1} ", TableName, condition);

            int count = this.AdoHelper.ExecuteNonQuery(this.ConnectionString, CommandType.Text, sql);

            return UtilityConvert.IntToBoolean(count, UtilityConvert.ConvertMode.Relaxed);
        }


        #endregion

    }
}
