using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessboardTest 的测试类，旨在
    ///包含所有 ChessboardTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessboardTest
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
        ///Chessboard 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void ChessboardConstructorTest()
        {
            Chessboard target = new Chessboard();
            target.InitializeChessmans();
            Assert.IsNotNull(target);

            for (int x = 1; x <= 8; x++)//所有的格子ChessboardGrid类都应实例化
            {
                for (int y = 1; y <= 8; y++)
                {
                    Assert.IsNotNull(target.GetGrid(x, y));
                }
            }
            for (int y = 1; y <= 2; y++)//第1，2行的格子中应有棋子
            {
                for (int x = 1; x <= 8; x++)
                {
                    Assert.IsNotNull(target.GetGrid(x, y).ChessmanOwner, target.GetGrid(x, y).ToString());
                }
            }
            for (int y = 7; y <= 8; y++)//第7，8行的格子中应有棋子
            {
                for (int x = 1; x <= 8; x++)
                {
                    Assert.IsNotNull(target.GetGrid(x, y).ChessmanOwner);
                }
            }
            for (int y = 3; y <= 6; y++)//从第3行到第6行的格子中棋子应为Null
            {
                for (int x = 1; x <= 8; x++)
                {
                    Assert.IsNull(target.GetGrid(x, y).ChessmanOwner);
                }
            }
        }
    }
}
