using Gean.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
namespace Gean.Data.UnitTesting
{
    
    
    /// <summary>
    ///这是 SQLWhereTextBuilderTest 的测试类，旨在
    ///包含所有 SQLWhereTextBuilderTest 单元测试
    ///</summary>
    [TestClass()]
    public class SQLWhereTextBuilderTest
    {

        /// <summary>
        ///ToSqlText 的测试
        ///</summary>
        [TestMethod()]
        public void ToSqlTextTest01()
        {
            string expected = string.Empty;
            string actual = string.Empty;
            SQLWhereTextBuilder target = null;

            target = new SQLWhereTextBuilder();
            target.Set("aaa", SQLText.CompareOperation.Equal, DbType.Boolean, true);
            expected = "aaa = True";
            actual = target.ToSqlText();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///ToSqlText 的测试
        ///</summary>
        [TestMethod()]
        public void ToSqlTextTest02()
        {
            string expected = string.Empty;
            string actual = string.Empty;
            SQLWhereTextBuilder target = null;

            target = new SQLWhereTextBuilder();
            target.Set("aaa", SQLText.CompareOperation.MoreThan, DbType.Single, 22);
            expected = "aaa > 22";
            actual = target.ToSqlText();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///ToSqlText 的测试
        ///</summary>
        [TestMethod()]
        public void ToSqlTextTest03()
        {
            string expected = string.Empty;
            string actual = string.Empty;

            SQLWhereTextBuilder target = new SQLWhereTextBuilder();
            target.Set("aaa", SQLText.CompareOperation.Equal, DbType.Boolean, true);

            SQLWhereTextBuilder target1 = new SQLWhereTextBuilder();
            target1.Set("bbb", SQLText.CompareOperation.Equal, DbType.Double, 222);

            SQLWhereTextBuilder target2 = new SQLWhereTextBuilder();
            target2.Set("ccc", SQLText.CompareOperation.Equal, DbType.DateTime, DateTime.Parse("1999-9-9 9:9:9"));

            SQLWhereTextBuilder target3 = new SQLWhereTextBuilder();
            target3.Set("ddd", SQLText.CompareOperation.Equal, DbType.String, "DDD");

            target.Add(SQLText.LogicOperation.And, target1);
            target.Add(SQLText.LogicOperation.Or, target2);
            target.Add(SQLText.LogicOperation.Or, target3);

            expected = "(aaa = True AND bbb = 222 OR ccc = '1999-09-09 09:09:09.000' OR ddd = 'DDD')";
            actual = target.ToSqlText();
            Assert.AreEqual(expected, actual);
        }
    }
}
