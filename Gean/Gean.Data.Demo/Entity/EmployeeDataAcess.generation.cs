using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Gean.Data.DAL;

namespace Gean.Data.Demo
{
    public class EmployeeDataAcess : DataAcess, IDataAcess<Employee>
    {
        public EmployeeDataAcess()
            : base("dbo.Employee", "EmployeeId")
        {
        }

        #region IDataAcess<Employee> 成员

        public IAcessHelper AcessHelper
        {
            get { return MsSqlHelper.GetMsSqlHelper(); }
        }

        #endregion

        void zzzz()
        { 
            
        }

    }

    class SqlOperateInfo
    {
        //Suppose your ServerName is "aa",DatabaseName is "bb",UserName is "cc", Password is "dd"
        private string sqlConnectionCommand = "Data Source=aa;Initial Catalog=bb;User ID=cc;Pwd=dd";
        //This table contains two columns:KeywordID int not null,KeywordName varchar(100) not null
        private string dataTableName = "Basic_Keyword_Test";

        private string storedProcedureName = "Sp_InertToBasic_Keyword_Test";
        private string sqlSelectCommand = "Select KeywordID, KeywordName From Basic_Keyword_Test";
        //sqlUpdateCommand could contain "insert" , "delete" , "update" operate
        private string sqlUpdateCommand = "Delete From Basic_Keyword_Test Where KeywordID = 1";

        public void UseSqlReader()
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionCommand);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = sqlSelectCommand;

            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                //Get KeywordID and KeywordName , You can do anything you like. Here I just output them.
                int keywordid = (int)sqlDataReader[0];
                //the same as: int keywordid = (int)sqlDataReader["KeywordID"]
                string keywordName = (string)sqlDataReader[1];
                //the same as: string keywordName = (int)sqlDataReader["KeywordName"]
                Console.WriteLine("KeywordID = " + keywordid + " , KeywordName = " + keywordName);
            }

            sqlDataReader.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();
        }
        public void UseSqlStoredProcedure()
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionCommand);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = storedProcedureName;

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            //you can use reader here,too.as long as you modify the sp and let it like select * from ....

            sqlCommand.Dispose();
            sqlConnection.Close();
        }
        public void UseSqlDataSet()
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionCommand);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = sqlSelectCommand;

            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet dataSet = new DataSet();
            //sqlCommandBuilder is for update the dataset to database
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.Fill(dataSet, dataTableName);

            //Do something to dataset then you can update it to 　Database.Here I just add a row
            DataRow row = dataSet.Tables[0].NewRow();
            row[0] = 10000;
            row[1] = "new row";
            dataSet.Tables[0].Rows.Add(row);

            sqlDataAdapter.Update(dataSet, dataTableName);

            sqlCommand.Dispose();
            sqlDataAdapter.Dispose();
            sqlConnection.Close();
        }
    }
}
