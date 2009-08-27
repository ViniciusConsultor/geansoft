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

        /// <summary>
        ///WhiteCastleQueen 的测试
        ///</summary>
        [TestMethod()]
        public void WhiteCastleQueenTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            target.WhiteQueenCastlingAvailability = expected;
            actual = target.WhiteQueenCastlingAvailability;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///WhiteCastleKing 的测试
        ///</summary>
        [TestMethod()]
        public void WhiteCastleKingTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            target.WhiteKingCastlingAvailability = expected;
            actual = target.WhiteKingCastlingAvailability;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///HalfMove 的测试
        ///</summary>
        [TestMethod()]
        public void HalfMoveTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            target.HalfMoveClock = expected;
            actual = target.HalfMoveClock;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///FullMove 的测试
        ///</summary>
        [TestMethod()]
        public void FullMoveTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            target.FullMoveNumber = expected;
            actual = target.FullMoveNumber;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Enpassant 的测试
        ///</summary>
        [TestMethod()]
        public void EnpassantTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            ChessPosition expected = ChessPosition.Empty; // TODO: 初始化为适当的值
            ChessPosition actual;
            target.EnPassantTargetPosition = expected;
            actual = target.EnPassantTargetPosition;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Color 的测试
        ///</summary>
        [TestMethod()]
        public void ColorTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            char expected = '\0'; // TODO: 初始化为适当的值
            char actual;
            target.ActiveColor = expected;
            actual = target.ActiveColor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///BlackCastleQueen 的测试
        ///</summary>
        [TestMethod()]
        public void BlackCastleQueenTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            target.BlackQueenCastlingAvailability = expected;
            actual = target.BlackQueenCastlingAvailability;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///BlackCastleKing 的测试
        ///</summary>
        [TestMethod()]
        public void BlackCastleKingTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            target.BlackKingCastlingAvailability = expected;
            actual = target.BlackKingCastlingAvailability;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest1()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ToFENString 的测试
        ///</summary>
        [TestMethod()]
        public void ToFENStringTest()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.ToFENString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest1()
        {
            FENBuilder target = new FENBuilder(); // TODO: 初始化为适当的值
            string str = string.Empty; // TODO: 初始化为适当的值
            target.Parse(str);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }
    }
}
