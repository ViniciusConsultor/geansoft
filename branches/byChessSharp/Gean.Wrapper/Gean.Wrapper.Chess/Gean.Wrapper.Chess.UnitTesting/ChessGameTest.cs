using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessGameTest 的测试类，旨在
    ///包含所有 ChessGameTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessGameTest
    {
        #region

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
        ///Item 的测试
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            ChessGame target = new ChessGame();

            int x = 0;
            int y = 0;

            ChessGrid expected = null;
            ChessGrid actual = null;

            x = 1;
            y = 1;
            expected = new ChessGrid(x, y);
            actual = target[x, y];
            Assert.AreEqual(expected, actual);

            x = 8;
            y = 8;
            expected = new ChessGrid(x, y);
            actual = target[x, y];
            Assert.AreEqual(expected, actual);

            x = 3;
            y = 6;
            expected = new ChessGrid(x, y);
            actual = target[x, y];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Record 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Gean.Wrapper.Chess.dll")]
        public void RecordTest()
        {
            ChessGame_Accessor target = new ChessGame_Accessor(); // TODO: 初始化为适当的值
            ChessRecord expected = null; // TODO: 初始化为适当的值
            ChessRecord actual;
            target.Record = expected;
            actual = target.Record;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///LoadGrids 的测试
        ///</summary>
        [TestMethod()]
        public void LoadGridsTest()
        {
            ChessGame target = new ChessGame(); // TODO: 初始化为适当的值
            target.LoadGrids();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///Item 的测试
        ///</summary>
        [TestMethod()]
        public void ItemTest2()
        {
            ChessGame target = new ChessGame(); // TODO: 初始化为适当的值
            int x = 0; // TODO: 初始化为适当的值
            int y = 0; // TODO: 初始化为适当的值
            ChessGrid actual;
            actual = target[x, y];
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Item 的测试
        ///</summary>
        [TestMethod()]
        public void ItemTest1()
        {
            ChessGame target = new ChessGame(); // TODO: 初始化为适当的值
            char c = '\0'; // TODO: 初始化为适当的值
            int y = 0; // TODO: 初始化为适当的值
            ChessGrid actual;
            actual = target[c, y];
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
