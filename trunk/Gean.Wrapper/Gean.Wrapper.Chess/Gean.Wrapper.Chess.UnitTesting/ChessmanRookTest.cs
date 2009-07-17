using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            List<ChessboardGrid> expected = new List<ChessboardGrid>();
            ChessboardGrid[] actual;

            Chessboard board = new Chessboard();

            ChessboardGrid currGrid  = board.GetGrid(6, 3);
            ChessmanRook rook = new ChessmanRook(currGrid, Enums.ChessmanSide.Black);
            Assert.IsNotNull(rook.GridOwner.Parent.GetGrid(6, 2).ChessmanOwner);
            Assert.IsNotNull(rook.GridOwner.Parent.GetGrid(6, 3).ChessmanOwner);

            ChessboardGrid leftGrid  = board.GetGrid(3, 3);
            ChessboardGrid rightGrid = board.GetGrid(8, 3);
            ChessboardGrid topGrid   = board.GetGrid(6, 7);

            ChessmanPawn pawn1 = new ChessmanPawn(leftGrid,  Enums.ChessmanSide.White);
            ChessmanPawn pawn2 = new ChessmanPawn(rightGrid, Enums.ChessmanSide.White);
            ChessmanPawn pawn3 = new ChessmanPawn(topGrid,   Enums.ChessmanSide.White);

            pawn1.RegistChessman(leftGrid);
            pawn2.RegistChessman(rightGrid);
            pawn3.RegistChessman(topGrid);

            for (int i = 2; i < 9; i++)
            {
                if (i==3)
                    continue;
                expected.Add(board.GetGrid(6, i));
            }
            for (int i = 3; i < 9; i++)
            {
                if (i == 3)
                    continue;
                ChessboardGrid buildGrid = board.GetGrid(i, 3);
                if (!expected.Contains(buildGrid))
                {
                    expected.Add(buildGrid);
                }
            }

            actual = rook.GetGridsByPath();

            foreach (ChessboardGrid item in actual)
            {
                Assert.IsTrue(expected.Contains(item), item.ToString());
            }
            Assert.AreEqual(actual.Length, expected.Count);
        }
    }
}
