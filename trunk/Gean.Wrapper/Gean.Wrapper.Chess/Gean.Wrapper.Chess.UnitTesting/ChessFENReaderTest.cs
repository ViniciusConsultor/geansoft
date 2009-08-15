using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessFENReaderTest 的测试类，旨在
    ///包含所有 ChessFENReaderTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessFENReaderTest
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
            ChessFENReader reader = new ChessFENReader();

            string fen01 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            reader = new ChessFENReader();
            reader.Parse(fen01);
            string actual01 = reader.ToString();

            string fen02 = "rnbqkb1r/pp1ppppp/5n2/2p5/2P5/2N2N2/PP1PPPPP/R1BQKB1R b KQkq - 0 3";
            reader = new ChessFENReader();
            reader.Parse(fen02);
            string actual02 = reader.ToString();

            string fen03 = "rn1qkb1r/pb1p1ppp/1p2pn2/2p5/2P5/2N2NP1/PP1PPPBP/R1BQ1RK1 b kq - 0 6";
            reader = new ChessFENReader();
            reader.Parse(fen03);
            string actual03 = reader.ToString();

            string fen04 = "rn1q1rk1/p2pbppp/1p2p3/2p5/2PPb3/5NP1/PP2PPBP/R1BQR1K1 w - - 0 10";
            reader = new ChessFENReader();
            reader.Parse(fen04);
            string actual04 = reader.ToString();

            string fen05 = "rn3rk1/pq2bppp/1p2p3/2pp4/2PPbB2/4QNP1/PP2PPBP/3RR1K1 w - - 0 14";
            reader = new ChessFENReader();
            reader.Parse(fen05);
            string actual05 = reader.ToString();

            string fen06 = "r4rk1/pq1nb1pp/1p2p3/3b1p2/1P3B2/P2Q1NP1/4PPBP/3RR1K1 w - - 0 19";
            reader = new ChessFENReader();
            reader.Parse(fen06);
            string actual06 = reader.ToString();

            string fen07 = "rnbqkbnr/pp1ppppp/2p5/8/3PP3/8/PPP2PPP/RNBQKBNR b KQkq d3 0 2";
            reader = new ChessFENReader();
            reader.Parse(fen07);
            string actual07 = reader.ToString();

            string fen08 = "2R1kb1r/1p3pp1/p3p2p/4P3/8/1q2BN2/PP2BPPP/6K1 b k - 0 22";
            reader = new ChessFENReader();
            reader.Parse(fen08);
            string actual08 = reader.ToString();

            string fen09 = "7r/5p2/4k2p/1p2PN1P/pb6/1P1BB3/1P3K2/8 w - - 0 36";
            reader = new ChessFENReader();
            reader.Parse(fen09);
            string actual09 = reader.ToString();

            string fen10 = "8/8/7k/2p3Qp/1P1bq3/8/6RP/7K b - - 0 48";
            reader = new ChessFENReader();
            reader.Parse(fen10);
            string actual10 = reader.ToString();

            string fen11 = "rnbqkbnr/ppp1ppp1/7p/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6c4 0 3";
            reader = new ChessFENReader();
            reader.Parse(fen11);
            string actual11 = reader.ToString();


            Assert.AreEqual(fen01, actual01);
            Assert.AreEqual(fen02, actual02);
            Assert.AreEqual(fen03, actual03);
            Assert.AreEqual(fen04, actual04);
            Assert.AreEqual(fen05, actual05);
            Assert.AreEqual(fen06, actual06);
            Assert.AreEqual(fen07, actual07);
            Assert.AreEqual(fen08, actual08);
            Assert.AreEqual(fen09, actual09);
        }
    }
}
