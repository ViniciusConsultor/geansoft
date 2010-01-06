using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Gean.Data.DAL;
using System.Data.Common;

namespace Gean.Data.Demo
{
    public class EmployeeDataAcess : DataAcess, IDataAcess<Employee>
    {
        public EmployeeDataAcess()
            : base("dbo.Employee", "EmployeeId")
        {
            
        }

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
                             WHERE <搜索条件,,>";
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
                return @"DELETE FROM [Northwind].[dbo].[Employees]
                              WHERE <搜索条件,,>";
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

        #endregion

        #region IDataCRUD 成员

        public bool Create(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Retrieve(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
