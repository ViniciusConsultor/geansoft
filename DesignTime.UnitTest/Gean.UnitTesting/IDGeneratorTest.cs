using Gean;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gean.UnitTesting
{
    /// <summary>
    ///这是 IDGeneratorTest 的测试类，旨在
    ///包含所有 IDGeneratorTest 单元测试
    ///</summary>
    [TestClass()]
    public class IDGeneratorTest
    {
        #region 附加测试属性


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        ///// <summary>
        /////Parse 的测试
        /////</summary>
        //[TestMethod()]
        //public void ParseTest()
        //{
        //    string id = "UF-TABLE-ACK9-1-996";
        //    int userflag = 2;
        //    int sequence = 5;
        //    int second = 1;
        //    int count = 3;

        //    DateTime expected = new DateTime();
        //    DateTime actual = IDGenerator.Parse(id.Replace("-", ""), userflag, sequence, second, count);
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
