using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 UtilityTest 的测试类，旨在
    ///包含所有 UtilityTest 单元测试
    ///</summary>
    [TestClass()]
    public class UtilityTest
    {


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


        /// <summary>
        ///StringToInt 的测试
        ///</summary>
        [TestMethod()]
        public void StringToIntTest()
        {
            string str = string.Empty; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Utility.StringToInt(str);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Rand64 的测试
        ///</summary>
        [TestMethod()]
        public void Rand64Test()
        {
            long expected = 0; // TODO: 初始化为适当的值
            long actual;
            actual = Utility.Rand64();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ParseAppendantString 的测试
        ///</summary>
        [TestMethod()]
        public void ParseAppendantStringTest()
        {
            string value = string.Empty; // TODO: 初始化为适当的值
            char flag = '\0'; // TODO: 初始化为适当的值
            int number = 0; // TODO: 初始化为适当的值
            int numberExpected = 0; // TODO: 初始化为适当的值
            string username = string.Empty; // TODO: 初始化为适当的值
            string usernameExpected = string.Empty; // TODO: 初始化为适当的值
            string record = string.Empty; // TODO: 初始化为适当的值
            string recordExpected = string.Empty; // TODO: 初始化为适当的值
            Utility.ParseAppendantString(value, flag, out number, out username, out record);
            Assert.AreEqual(numberExpected, number);
            Assert.AreEqual(usernameExpected, username);
            Assert.AreEqual(recordExpected, record);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///IntToString 的测试
        ///</summary>
        [TestMethod()]
        public void IntToStringTest()
        {
            int i = 0; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = Utility.IntToString(i);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IntToChar 的测试
        ///</summary>
        [TestMethod()]
        public void IntToCharTest()
        {
            int i = 0; // TODO: 初始化为适当的值
            char expected = '\0'; // TODO: 初始化为适当的值
            char actual;
            actual = Utility.IntToChar(i);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///CharToInt 的测试
        ///</summary>
        [TestMethod()]
        public void CharToIntTest()
        {
            char c = '\0'; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Utility.CharToInt(c);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
