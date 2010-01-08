using Gean.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Data.UnitTesting
{
    
    
    /// <summary>
    ///这是 SQLWhereTextBuilder_InTextBuilderTest 的测试类，旨在
    ///包含所有 SQLWhereTextBuilder_InTextBuilderTest 单元测试
    ///</summary>
    [TestClass()]
    public class SQLWhereTextBuilder_InTextBuilderTest
    {

        /// <summary>
        ///ToSqlText 的测试
        ///</summary>
        [TestMethod()]
        public void ToSqlTextTest01()
        {
            SQLWhereTextBuilder target = new SQLWhereTextBuilder();
            SQLWhereTextBuilder.InTextBuilder inb = new SQLWhereTextBuilder.InTextBuilder();
            inb.Set("a", "b", "c", "d");

            target.Set("abcd", SQLText.CompareOperation.In, System.Data.DbType.String, inb);

            string expected = "abcd IN ('a','b','c','d')";
            string actual;
            actual = target.ToSqlText();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ToSqlTextTest02()
        {
            SQLTextBuilder stb = new SQLTextBuilder();
            stb.Action = SQLText.Action.Select;
            stb.Field.Set("field1");
            stb.From.Set("DemoTable");

            SQLWhereTextBuilder target = new SQLWhereTextBuilder();
            SQLWhereTextBuilder.InTextBuilder inb = new SQLWhereTextBuilder.InTextBuilder();
            inb.Set(stb);

            target.Set("abcd", SQLText.CompareOperation.In, System.Data.DbType.String, inb);

            string expected = "abcd IN (SELECT field1 FROM DemoTable)";
            string actual;
            actual = target.ToSqlText();
            Assert.AreEqual(expected, actual);
        }
    }
}
