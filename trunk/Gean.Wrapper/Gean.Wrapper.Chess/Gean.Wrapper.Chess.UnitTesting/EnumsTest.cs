using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 EnumsTest 的测试类，旨在
    ///包含所有 EnumsTest 单元测试
    ///</summary>
    [TestClass()]
    public class EnumsTest
    {
        #region MyRegion

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

        #region 附加测试属性
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


        #endregion

        /// <summary>
        ///ClearFlag 的测试
        ///</summary>
        [TestMethod()]
        public void ClearFlagTest()
        {
            Enums.AccessorialAction value = Enums.AccessorialAction.KillAndCheck;
            Enums.AccessorialAction flag = Enums.AccessorialAction.Kill;
            Enums.AccessorialAction expected = Enums.AccessorialAction.Check;
            
            Enums.AccessorialAction actual = Enums.GetFlag(value, flag);
            Assert.AreEqual(expected, actual);
        }
    }
}
