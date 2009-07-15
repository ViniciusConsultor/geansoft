using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessmanRookTest 的测试类，旨在
    ///包含所有 ChessmanRookTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessmanRookTest
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
        ///GetGridsByPath 的测试
        ///</summary>
        [TestMethod()]
        public void GetGridsByPathTest()
        {
            ChessboardGrid[] expected = null;
            ChessboardGrid[] actual;

            Chessboard board = new Chessboard();

            ChessboardGrid currGrid = board.GetGrid(6, 6);
            ChessmanRook rook = new ChessmanRook(currGrid, Enums.ChessmanSide.Black);

            ChessboardGrid leftGrid = board.GetGrid(3, 6);
            ChessboardGrid rightGrid = board.GetGrid(8, 6);
            ChessboardGrid topGrid = board.GetGrid(6, 2);
            ChessboardGrid bottonGrid = board.GetGrid(6, 8);

            ChessmanPawn pawn1 = new ChessmanPawn(leftGrid, Enums.ChessmanSide.White);
            ChessmanPawn pawn2 = new ChessmanPawn(rightGrid, Enums.ChessmanSide.White);
            ChessmanPawn pawn3 = new ChessmanPawn(topGrid, Enums.ChessmanSide.White);
            ChessmanPawn pawn4 = new ChessmanPawn(bottonGrid, Enums.ChessmanSide.White);

            pawn1.RegistChessman(leftGrid);
            pawn2.RegistChessman(rightGrid);
            pawn3.RegistChessman(topGrid);
            pawn4.RegistChessman(bottonGrid);

            actual = rook.GetGridsByPath();
            Assert.AreEqual(expected, actual);
        }
    }
}
