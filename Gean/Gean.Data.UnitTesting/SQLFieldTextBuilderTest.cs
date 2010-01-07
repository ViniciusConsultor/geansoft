using Gean.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Gean.Data.Exceptions;
namespace Gean.Data.UnitTesting
{
    
    
    /// <summary>
    ///这是 SQLFieldTextBuilderTest 的测试类，旨在
    ///包含所有 SQLFieldTextBuilderTest 单元测试
    ///</summary>
    [TestClass()]
    public class SQLFieldTextBuilderTest
    {

        /// <summary>
        ///ToSqlText 的测试
        ///</summary>
        [TestMethod()]
        public void ToSqlTextTest()
        {
            string expected = string.Empty; 
            string actual = string.Empty; 
            SQLFieldTextBuilder target = null;

            target = new SQLFieldTextBuilder();
            target.Set("a", "b", "c", "d");
            expected = "a, b, c, d";
            actual = target.ToSqlText();
            Assert.AreEqual(expected, actual);

            target = new SQLFieldTextBuilder();
            target.Set(
                new Pair<string, string>("aaa", "AAA"),
                new Pair<string, string>("bbb", "BBB"),
                new Pair<string, string>("ccc", "CCC")
                );
            target.Set(
                new Pair<string, string>("ddd", "DDD"),
                new Pair<string, string>("eee", "EEE")
                );
            expected = "aaa AS \"AAA\", bbb AS \"BBB\", ccc AS \"CCC\", ddd AS \"DDD\", eee AS \"EEE\"";
            actual = target.ToSqlText();
            Assert.AreEqual(expected, actual);

            target = new SQLFieldTextBuilder();
            List<string> a = new List<string>();
            a.Add("aaa");
            a.Add("bbb");
            List<string> b = new List<string>();
            b.Add("AAA");
            target.Set(a.ToArray(), b.ToArray());
            //Assert.IsInstanceOfType(target.Set(a.ToArray(), b.ToArray()), typeof(SQLTextBuilderException)); ;
        }
    }
}
