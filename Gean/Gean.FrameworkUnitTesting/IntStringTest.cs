using Gean;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.FrameworkUnitTesting
{
    
    
    /// <summary>
    ///这是 IntStringTest 的测试类，旨在
    ///包含所有 IntStringTest 单元测试
    ///</summary>
    [TestClass()]
    public class IntStringTest
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
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            IntString target = new IntString();
            uint digit = 5;
            string expected = "123456789";
            for (int i = 0; i < 123456789; i++)
            {
                target.Next();
            }
            string actual = target.ToString(digit);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///GetDigit 的测试
        ///</summary>
        [TestMethod()]
        public void GetDigitTest()
        {
            ulong value = 123456789;
            uint expected = 9;
            uint actual = IntString.GetDigit(value);
            Assert.AreEqual(expected, actual);

            value = 321;
            expected = 3;
            actual = IntString.GetDigit(value);
            Assert.AreEqual(expected, actual);

            value = 123444;
            expected = 6;
            actual = IntString.GetDigit(value);
            Assert.AreEqual(expected, actual);

            value = 999999999;
            expected = 9;
            actual = IntString.GetDigit(value);
            Assert.AreEqual(expected, actual);

            value = 0;
            expected = 1;
            actual = IntString.GetDigit(value);
            Assert.AreEqual(expected, actual);

            value = 1222222;
            expected = 7;
            actual = IntString.GetDigit(value);
            Assert.AreEqual(expected, actual);
        }

    }
}
