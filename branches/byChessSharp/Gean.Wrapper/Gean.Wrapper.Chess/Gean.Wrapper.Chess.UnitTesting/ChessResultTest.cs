using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    /// <summary>
    ///这是 ChessResultTest 的测试类，旨在
    ///包含所有 ChessResultTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessResultTest
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
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string      value;
            ChessResult expected;
            ChessResult actual;

            value = "1 - 0";
            expected = new ChessResult();
            expected.Result = Enums.Result.WhiteWin;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "0 - 1";
            expected = new ChessResult();
            expected.Result = Enums.Result.BlackWin;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "1-1";
            expected = new ChessResult();
            expected.Result = Enums.Result.Draw;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = " 1/2 - 1/2 ";
            expected = new ChessResult();
            expected.Result = Enums.Result.Draw;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = " UnKnown ";
            expected = new ChessResult();
            expected.Result = Enums.Result.UnKnown;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "???";
            expected = new ChessResult();
            expected.Result = Enums.Result.UnKnown;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "?";
            expected = new ChessResult();
            expected.Result = Enums.Result.UnKnown;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = " ";
            expected = new ChessResult();
            expected.Result = Enums.Result.UnKnown;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "";
            expected = new ChessResult();
            expected.Result = Enums.Result.UnKnown;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);

            value = null;
            expected = new ChessResult();
            expected.Result = Enums.Result.UnKnown;
            actual = ChessResult.Parse(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ChessResult target;
            string expected;
            string actual;

            expected = "1/2-1/2";
            target = new ChessResult();
            target.Result = Enums.Result.Draw;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            expected = "1-0";
            target = new ChessResult();
            target.Result = Enums.Result.WhiteWin;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            expected = "0-1";
            target = new ChessResult();
            target.Result = Enums.Result.BlackWin;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            expected = "?";
            target = new ChessResult();
            target.Result = Enums.Result.UnKnown;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

        }
    }
}
