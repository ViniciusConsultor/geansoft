using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Gean.Data;
using Runner.Entity;
using Runner.DAL;
using Gean.Data.Resources;
using System.Collections;

namespace Runner.DAL.SQLServer
{
    /// <summary>
    /// 针对Employee表中的记录实体(<see cref="Employee"/>)的数据访问封装类型。
    /// </summary>
    public class EmployeeDataAcess : IEmployeeDataAcess
    {
        /// <summary>
        /// 构造函数。Initializes a new instance of the <see cref="EmployeeDataAcess"/> class.
        /// </summary>
        public EmployeeDataAcess()
        {
            //在这里添加构造函数逻辑
        }

        #region Parameters
        public class Parameters
        {
            public static SqlParameter Id
            {
                get
                {
                    SqlParameter sp = new SqlParameter("@Id", DbType.String);
                    sp.SourceColumn = "Id";
                    return sp;
                }
            }
            public static SqlParameter Guid
            {
                get { return new SqlParameter("@Guid", DbType.String); }
            }
            public static SqlParameter MemberName
            {
                get { return new SqlParameter("@MemberName", DbType.String); }
            }
            public static SqlParameter Password
            {
                get { return new SqlParameter("@Password", DbType.String); }
            }
            public static SqlParameter AppendDatetime
            {
                get { return new SqlParameter("@AppendDatetime", DbType.String); }
            }
            public static SqlParameter LoginDatetime
            {
                get { return new SqlParameter("@LoginDatetime", DbType.String); }
            }
            public static SqlParameter LoginCount
            {
                get { return new SqlParameter("@LoginCount", DbType.String); }
            }
        }
        #endregion

        #region IDataAcess<Employee> 成员

        /// <summary>
        /// Gets 一个数据库访问助手类
        /// </summary>
        /// <value>The acess helper.</value>
        public IAcessHelper AcessHelper
        {
            get { return MsSqlHelper.GetMsSqlHelper(); }
        }

        /// <summary>
        /// 实体对应的表名
        /// </summary>
        /// <value></value>
        public string TableName
        {
            get { return "dbo.Employee"; }
        }

        /// <summary>
        /// 实体对应的表中的主键字段名
        /// </summary>
        /// <value></value>
        public string PrimaryKey
        {
            get { return "EmployeeId"; }
        }

        /// <summary>
        /// 实体对应的表中的主键字段的数据类型。
        /// </summary>
        /// <value>The type of the primary key.</value>
        public Type PrimaryKeyType
        {
            get { return typeof(Int32); }
        }

        /// <summary>
        /// Gets 一个更新命令。
        /// </summary>
        /// <value>The update command.</value>
        public String UpdateCommandText
        {
            get
            {
                return @"UPDATE [Northwind].[dbo].[Employees]
                               SET [LastName] = <LastName, nvarchar(20),>
                                  ,[FirstName] = <FirstName, nvarchar(10),>
                                  ,[Title] = <Title, nvarchar(30),>
                                  ,[TitleOfCourtesy] = <TitleOfCourtesy, nvarchar(25),>
                                  ,[BirthDate] = <BirthDate, datetime,>
                                  ,[HireDate] = <HireDate, datetime,>
                                  ,[Address] = <Address, nvarchar(60),>
                                  ,[City] = <City, nvarchar(15),>
                                  ,[Region] = <Region, nvarchar(15),>
                                  ,[PostalCode] = <PostalCode, nvarchar(10),>
                                  ,[Country] = <Country, nvarchar(15),>
                                  ,[HomePhone] = <HomePhone, nvarchar(24),>
                                  ,[Extension] = <Extension, nvarchar(4),>
                                  ,[Photo] = <Photo, image,>
                                  ,[Notes] = <Notes, ntext,>
                                  ,[ReportsTo] = <ReportsTo, int,>
                                  ,[PhotoPath] = <PhotoPath, nvarchar(255),>
                             WHERE 
                                  [Id]=@Id";
            }
        }

        /// <summary>
        /// Gets 一个插入命令。
        /// </summary>
        /// <value>The insert command.</value>
        public String InsertCommandText
        {
            get
            {
                return @"INSERT INTO [Northwind].[dbo].[Employees]
                               ([LastName]
                               ,[FirstName]
                               ,[Title]
                               ,[TitleOfCourtesy]
                               ,[BirthDate]
                               ,[HireDate]
                               ,[Address]
                               ,[City]
                               ,[Region]
                               ,[PostalCode]
                               ,[Country]
                               ,[HomePhone]
                               ,[Extension]
                               ,[Photo]
                               ,[Notes]
                               ,[ReportsTo]
                               ,[PhotoPath])
                         VALUES
                               (@LastName
                               ,@FirstName
                               ,@Title
                               ,@TitleOfCourtesy
                               ,@BirthDate
                               ,@HireDate
                               ,@Address
                               ,@City
                               ,@Region
                               ,@PostalCode
                               ,@Country
                               ,@HomePhone
                               ,@Extension
                               ,@Photo
                               ,@Notes
                               ,@ReportsTo
                               ,@PhotoPath)";
            }
        }

        /// <summary>
        /// Gets 一个删除命令。
        /// </summary>
        /// <value>The delete command.</value>
        public String DeleteCommandText
        {
            get
            {
                return @"DELETE FROM 
                                [Northwind].[dbo].[Employees]
                              WHERE 
                                [Id]=@Id";
            }
        }

        /// <summary>
        /// Gets 一组 SQL 命令和一个数据库连接的封装类型<see cref="DataAdapter"/>。
        /// </summary>
        /// <value>The data adapter.</value>
        public DataAdapter DataAdapter
        {
            get
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand(this.UpdateCommandText);
                da.DeleteCommand = new SqlCommand(this.DeleteCommandText);
                da.InsertCommand = new SqlCommand(this.InsertCommandText);
                return da;
            }
        }

        public ICollection<Employee> GetAll()
        {
            string sql = string.Format(SQLString.GetAll, this.TableName);

            Employee entity = null;
            EmployeeList list = new EmployeeList();

            using (IDataReader dr = this.AcessHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                    list.Add(entity);
                }
            }
            return list;
        }

        public Employee FindById(object id)
        {
            string sql = string.Format(SQLString.FindById, TableName, PrimaryKey);
            SqlParameter param = new SqlParameter("@ID", id);
            Employee entity = null;
            using (IDataReader dr = this.AcessHelper.ExecuteReader(CommandType.Text, sql, param))
            {
                if (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                }
            }
            return entity;
        }

        public Employee DataReaderToEntity(IDataReader dr)
        {
            Employee employee = new Employee();
            employee.Address = (string)dr["Address"];
            employee.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
            employee.City = (string)dr["City"];

            return employee;
        }

        #endregion

        #region IDataCreate 成员

        public bool Create(IEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataQuery 成员

        public bool Query(IEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataUpdate 成员

        public bool Update(IEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataDelete 成员

        public bool Delete(object id)
        {
            return false;
        }

        #endregion

        #region ColumnNames
        public class ColumnNames
        {
            public const string Id = "Id";
            public const string Guid = "Guid";
            public const string MemberName = "MemberName";
            public const string Password = "Password";
            public const string AppendDatetime = "AppendDatetime";
            public const string LoginDatetime = "LoginDatetime";
            public const string LoginCount = "LoginCount";

            static public string ToPropertyName(string columnName)
            {
                if (ht == null)
                {
                    ht = new Hashtable();

                    ht[Id] = EmployeeDataAcess.PropertyNames.Id;
                    ht[Guid] = EmployeeDataAcess.PropertyNames.Guid;
                    ht[MemberName] = EmployeeDataAcess.PropertyNames.MemberName;
                    ht[Password] = EmployeeDataAcess.PropertyNames.Password;
                    ht[AppendDatetime] = EmployeeDataAcess.PropertyNames.AppendDatetime;
                    ht[LoginDatetime] = EmployeeDataAcess.PropertyNames.LoginDatetime;
                    ht[LoginCount] = EmployeeDataAcess.PropertyNames.LoginCount;

                }
                return (string)ht[columnName];
            }

            static private Hashtable ht = null;
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string Id = "Id";
            public const string Guid = "Guid";
            public const string MemberName = "MemberName";
            public const string Password = "Password";
            public const string AppendDatetime = "AppendDatetime";
            public const string LoginDatetime = "LoginDatetime";
            public const string LoginCount = "LoginCount";

            static public string ToColumnName(string propertyName)
            {
                if (ht == null)
                {
                    ht = new Hashtable();

                    ht[Id] = EmployeeDataAcess.ColumnNames.Id;
                    ht[Guid] = EmployeeDataAcess.ColumnNames.Guid;
                    ht[MemberName] = EmployeeDataAcess.ColumnNames.MemberName;
                    ht[Password] = EmployeeDataAcess.ColumnNames.Password;
                    ht[AppendDatetime] = EmployeeDataAcess.ColumnNames.AppendDatetime;
                    ht[LoginDatetime] = EmployeeDataAcess.ColumnNames.LoginDatetime;
                    ht[LoginCount] = EmployeeDataAcess.ColumnNames.LoginCount;

                }
                return (string)ht[propertyName];
            }

            static private Hashtable ht = null;
        }
        #endregion

        public override string ToString()
        {
            return this.TableName + " Data Acess Type.";
        }

    }
}
