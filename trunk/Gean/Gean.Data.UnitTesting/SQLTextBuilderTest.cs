using Gean.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Data;

namespace Gean.Data.UnitTesting
{
    
    
    /// <summary>
    ///这是 SQLTextBuilderTest 的测试类，旨在
    ///包含所有 SQLTextBuilderTest 单元测试
    ///</summary>
    [TestClass()]
    public class SQLTextBuilderTest
    {

        /// <summary>
        /// ToSqlText 的测试
        /// </summary>
        [TestMethod()]
        public void ToSqlTextTest01()
        {
            string expected = "SELECT Field00, Field01 FROM Customs WHERE Id = '0011'";

            SQLTextBuilder target = new SQLTextBuilder();
            target.Action = SQLText.Action.Select;
            target.Field.Set("Field00", "Field01");
            target.From.Set("Customs");
            target.Where.Set("Id", SQLText.CompareOperation.Equal, DbType.String, "0011");

            Assert.AreEqual(expected, target.ToSqlText());
        }
        [TestMethod()]
        public void ToSqlTextTest02()
        {
            string expected = "SELECT CustomerID, CompanyName, ContactTitle, City FROM Customers WHERE CustomerID IN (SELECT CustomerID FROM Orders)";

            SQLTextBuilder target = new SQLTextBuilder();
            target.Action = SQLText.Action.Select;
            target.Field.Set("CustomerID", "CompanyName", "ContactTitle", "City");
            target.From.Set("Customers");

            SQLTextBuilder ta = new SQLTextBuilder();
            ta.Action = SQLText.Action.Select;
            ta.Field.Set("CustomerID");
            ta.From.Set("Orders");

            SQLWhereTextBuilder.InTextBuilder inb = new SQLWhereTextBuilder.InTextBuilder();
            inb.Set(ta);

            target.Where.Set("CustomerID", SQLText.CompareOperation.In, DbType.String, inb);

            Assert.AreEqual(expected, target.ToSqlText());
        }

        ///// <summary>
        /////ToSqlText 的测试
        /////</summary>
        //[TestMethod()]
        //public void ToSqlTextTest()
        //{
        //    SQLTextBuilder target = new SQLTextBuilder(); 
        //    string expected = string.Empty; 
        //    string actual;
        //    actual = target.ToSqlText();
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        /////GetDbParameters 的测试
        /////</summary>
        //[TestMethod()]
        //public void GetDbParametersTest()
        //{
        //    SQLTextBuilder target = new SQLTextBuilder(); 
        //    DbParameter[] expected = null; 
        //    DbParameter[] actual;
        //    actual = target.GetDbParameters();
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
