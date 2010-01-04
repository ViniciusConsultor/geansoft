using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;

namespace MyCampus.Component
{
    /// <summary>
    /// SqlResult 的摘要说明。
    /// 作者：刘志波
    /// 时间：2003-2-11
    /// 说明：
    /// 存储过程的返回值纪录类
    /// DataSet : 表示返回的表
    /// Output : 存储过程的输出参数
    /// Value : 存储过程的返回值
    /// </summary>
    public class SqlResult
    {
        public int Value;
        public Hashtable Output;
        public DataSet dataSet;

        public SqlResult()
        {
            Value = 0;
            Output = new Hashtable();
            dataSet = new DataSet();
        }
    }

    /// <summary>
    /// SqlProcedure 的摘要说明。
    /// 作者：刘志波
    /// 时间：2003-2-11
    /// 说明：
    /// 用于调用数据库中的存储过程，返回一个DataSet、Output、Value的SqlResult类
    /// </summary>
    public class SqlProcedure
    {
        private string sp_name;
        private SqlConnection myConnection;
        private SqlCommand myCommand;
        private SqlParameter myParameter;

        public string ProcedureName
        {
            get { return this.sp_name; }
            set { this.sp_name = value; }
        }

        public SqlProcedure()
            : this("")
        {
        }

        public SqlProcedure(string sp_name)
        {
            this.ProcedureName = sp_name;
        }

        public SqlResult Call(params object[] parameters)
        {
            SqlResult result = new SqlResult();

            myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);

            myCommand = new SqlCommand(this.ProcedureName, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);

            try
            {
                myConnection.Open();

                GetProcedureParameter(parameters);

                myAdapter.Fill(result.dataSet, "Table");

                GetOutputValue(result);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myAdapter.Dispose();
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }

        private void GetProcedureParameter(params object[] parameters)
        {
            SqlCommand myCommand2 = new SqlCommand();

            myCommand2.Connection = this.myConnection;
            myCommand2.CommandText = "select * from INFORMATION_SCHEMA.PARAMETERS where SPECIFIC_NAME='" + this.ProcedureName + "' order by ORDINAL_POSITION";

            SqlDataReader reader = null;
            try
            {
                reader = myCommand2.ExecuteReader();

                myParameter = new SqlParameter();
                myParameter.ParameterName = "@Value";
                myParameter.SqlDbType = SqlDbType.Int;
                myParameter.Direction = ParameterDirection.ReturnValue;

                myCommand.Parameters.Add(myParameter);

                int i = 0;
                while (reader.Read())
                {
                    myParameter = new SqlParameter();

                    myParameter.ParameterName = reader["PARAMETER_NAME"].ToString();
                    myParameter.Direction = reader["PARAMETER_MODE"].ToString() == "IN" ? ParameterDirection.Input : ParameterDirection.Output;

                    switch (reader["DATA_TYPE"].ToString())
                    {
                        case "bit":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (bool)parameters[i];
                            myParameter.SqlDbType = SqlDbType.Bit;
                            break;

                        case "bigint":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (int)parameters[i];
                            myParameter.SqlDbType = SqlDbType.BigInt;
                            break;

                        case "int":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (int)parameters[i];
                            myParameter.SqlDbType = SqlDbType.Int;
                            break;

                        case "decimal":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (double)parameters[i];
                            myParameter.SqlDbType = SqlDbType.Decimal;
                            myParameter.Precision = (byte)reader["NUMERIC_PRECISION"];
                            myParameter.Scale = (byte)reader["NUMERIC_SCALE"];
                            break;

                        case "nvarchar":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (string)parameters[i];
                            myParameter.Size = (int)reader["CHARACTER_MAXIMUM_LENGTH"];
                            myParameter.SqlDbType = SqlDbType.NVarChar;
                            break;

                        case "varchar":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (string)parameters[i];
                            myParameter.Size = (int)reader["CHARACTER_MAXIMUM_LENGTH"];
                            myParameter.SqlDbType = SqlDbType.VarChar;
                            break;

                        case "nchar":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (string)parameters[i];
                            myParameter.Size = (int)reader["CHARACTER_MAXIMUM_LENGTH"];
                            myParameter.SqlDbType = SqlDbType.NChar;
                            break;

                        case "char":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (string)parameters[i];
                            myParameter.Size = (int)reader["CHARACTER_MAXIMUM_LENGTH"];
                            myParameter.SqlDbType = SqlDbType.Char;
                            break;

                        case "ntext":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (string)parameters[i];
                            myParameter.SqlDbType = SqlDbType.NText;
                            break;

                        case "text":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (string)parameters[i];
                            myParameter.SqlDbType = SqlDbType.Text;
                            break;

                        case "datetime":
                            if (myParameter.Direction == ParameterDirection.Input)
                                myParameter.Value = (DateTime)parameters[i];
                            myParameter.SqlDbType = SqlDbType.DateTime;
                            break;

                        case "image":
                            if (myParameter.Direction == ParameterDirection.Input)
                            {
                                //HttpPostedFile PostedFile = (HttpPostedFile)parameters[i];

                                //Byte[] FileByteArray = new Byte[PostedFile.ContentLength];
                                //Stream StreamObject = PostedFile.InputStream;
                                //StreamObject.Read(FileByteArray, 0, PostedFile.ContentLength);

                                //myParameter.Value = FileByteArray;
                            }

                            myParameter.SqlDbType = SqlDbType.Image;
                            break;

                        case "uniqueidentifier":
                            //myParameter.Value = (string)parameters[i];
                            myParameter.SqlDbType = SqlDbType.UniqueIdentifier;
                            break;

                        default: break;
                    }
                    i++;

                    myCommand.Parameters.Add(myParameter);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null) reader.Close();
                myCommand2.Dispose();
            }
        }


        private void GetOutputValue(SqlResult result)
        {
            result.Value = (int)myCommand.Parameters["@Value"].Value;

            foreach (SqlParameter parameter in myCommand.Parameters)
            {
                if (parameter.Direction == ParameterDirection.Output)
                {
                    result.Output.Add(parameter.ParameterName, parameter.Value);
                }
            }
        }
    }
}