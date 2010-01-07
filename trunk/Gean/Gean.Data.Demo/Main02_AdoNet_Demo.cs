using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.ApplicationBlocks.Data;
using System.Text;

namespace Gean.Data.Demo
{
    public class Main02_AdoNet_Demo
    {
        internal static void Do()
        {
            SqlConnectionStringBuilder sqlConnSb = new SqlConnectionStringBuilder();
            sqlConnSb.DataSource = @"NSIMPLE-P4MAN\SQLEXPRESS";
            sqlConnSb.InitialCatalog = "Northwind";
            sqlConnSb.IntegratedSecurity = true;
            SqlConnection cn = new SqlConnection(sqlConnSb.ConnectionString);

            SelectDemo(cn);
            UpdateDemo(cn);
        }

        private static void UpdateDemo(SqlConnection cn)
        {
//            Employee emp = new Employee();
//            emp.Address = "";
//            emp.BirthDate = DateTime.Now;
//            emp.City = "shanghai";
//            emp.Country = "";
//            emp.EmployeeID = 1;
//            emp.Extension = "";
//            emp.FirstName = "zhao";
//            emp.HireDate = DateTime.Now;
//            emp.HomePhone = "12222";
//            emp.LastName = "shangzho";
//            emp.Notes = "notes";
//            emp.Photo = UtilityConvert.StringToBytes("abcd");
//            emp.PhotoPath = "abcd";
//            emp.PostalCode = "postalcode";
//            emp.Region = "region";
//            emp.ReportsTo = 6;
//            emp.Title = "title";
//            emp.TitleOfCourtesy = "fahuiqu";
//            string cmd = @"
//                    UPDATA
//
//                    ";
//            MsSqlHelper.GetMsSqlHelper(cn.ConnectionString).ExecuteNonQuery("", CommandType.Text, "");
        }

        private static void SelectDemo(SqlConnection cn)
        {
            SqlCommand command = new SqlCommand();
            string cmd = @"
                    SELECT
                        *
                    FROM
                        dbo.Employees
                    ";
            SqlDataReader reader = MsSqlHelper.GetMsSqlHelper(cn.ConnectionString).ExecuteReader(CommandType.Text, cmd);
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
            }

            reader.Close();
        }

        SqlDataAdapter ab()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = GetSelectCommand();

            adapter.InsertCommand = GetInsertCommand();

            adapter.UpdateCommand = GetUpdateCommand();

            adapter.DeleteCommand = GetDeleteCommand();

            return adapter;

        }

        static SqlConnection connection = new SqlConnection();

        private static SqlCommand GetDeleteCommand()
        {
            // Create the DeleteCommand.
            SqlCommand command = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @CustomerID", connection);

            // Add the parameters for the DeleteCommand.
            SqlParameter parameter = command.Parameters.Add("@CustomerID", SqlDbType.NChar, 5, "CustomerID");
            parameter.SourceVersion = DataRowVersion.Original;
            return command;
        }

        private static SqlCommand GetUpdateCommand()
        {
            // Create the UpdateCommand.
            SqlCommand command = new SqlCommand("UPDATE Customers SET CustomerID = @CustomerID, CompanyName = @CompanyName " + "WHERE CustomerID = @oldCustomerID", connection);

            // Add the parameters for the UpdateCommand.
            command.Parameters.Add("@CustomerID", SqlDbType.NChar, 5, "CustomerID");
            command.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40, "CompanyName");

            SqlParameter parameter = command.Parameters.Add("@oldCustomerID", SqlDbType.NChar, 5, "CustomerID");
            parameter.SourceVersion = DataRowVersion.Original;
            return command;
        }

        private static SqlCommand GetInsertCommand()
        {
            // Create the InsertCommand.
            SqlCommand command = new SqlCommand("INSERT INTO Customers (CustomerID, CompanyName) " + "VALUES (@CustomerID, @CompanyName)", connection);

            // Add the parameters for the InsertCommand.
            command.Parameters.Add("@CustomerID", SqlDbType.NChar, 5, "CustomerID");
            command.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40, "CompanyName");
            return command;
        }

        private static SqlCommand GetSelectCommand()
        {
            // Create the SelectCommand.
            SqlCommand command = new SqlCommand("SELECT * FROM Customers " + "WHERE Country = @Country AND City = @City", connection);

            // Add the parameters for the SelectCommand.
            command.Parameters.Add("@Country", SqlDbType.NVarChar, 15);
            command.Parameters.Add("@City", SqlDbType.NVarChar, 15);

            return command;
        }
    }
}