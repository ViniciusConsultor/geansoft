using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
   
    /// <summary>
    ///这是 FENBuilderTest 的测试类，旨在
    ///包含所有 FENBuilderTest 单元测试
    ///</summary>
    [TestClass()]
    public class FENBuilderTest
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
        ///Equals 的测试
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            ChessGame game = new ChessGame();
            game.LoadGrids();
            FENBuilder builder;
            builder = FENBuilder.CreateFENBuilder(game);
            Assert.IsTrue(builder.Equals(builder));
        }

        /// <summary>
        ///GetHashCode 的测试
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            ChessGame game = new ChessGame();
            game.LoadGrids();
            FENBuilder builder;
            builder = FENBuilder.CreateFENBuilder(game);
            Assert.AreEqual(builder.GetHashCode(), builder.GetHashCode());
        }

        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string expected;
            string actual;

            ChessGame game = new ChessGame();
            game.LoadGrids();
            FENBuilder builder;

            builder = FENBuilder.CreateFENBuilder(game);
            expected = "11111111/11111111/11111111/11111111/11111111/11111111/11111111/11111111"; 
            actual = builder.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string str;
            FENBuilder fen;

            str = "r1bq1rk1/pp2ppbp/2np1np1/8/2PNP3/2N1B3/PP2BPPP/R2QK2R w KQ - 0 9";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "r2qkbnr/pp1n1ppp/2p1p3/3pPb2/3P4/5N2/PPP1BPPP/RNBQK2R w KQkq - 0 6";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "2rqkb1r/1p1R1pp1/p3p2p/4P3/8/1Q2BN2/PPn1BPPP/R5K1 b k - 0 18";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "2r5/1p1k1pp1/p3p2p/4P3/1b2NP2/1P2B3/1P2B1PP/6K1 w - - 0 27";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "2r5/1p2kpp1/p3p2p/4P3/1b2NP2/1P1BB3/1P3KPP/8 b - - 0 28";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "rnbq1rk1/p4ppp/1p1p1n2/4p3/2PPP3/P2B4/2Q1NPPP/R1B1K2R w KQ - 0 11";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "r1bq1rk1/p4pp1/1pnp1n1p/4p1B1/2PPP3/P2B4/2Q1NPPP/R4RK1 w - - 0 13";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

            str = "rnbqkb1r/pppppppp/5n2/8/2P5/5N2/PP1PPPPP/RNBQKB1R b KQkq c3 0 2";
            fen = new FENBuilder();
            fen.Parse(str);
            Assert.AreEqual(str, fen.ToFENString());

        }
    }
}
