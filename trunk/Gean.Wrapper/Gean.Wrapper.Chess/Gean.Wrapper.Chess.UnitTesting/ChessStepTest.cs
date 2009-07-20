using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessStepTest 的测试类，旨在
    ///包含所有 ChessStepTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessStepTest
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

        /*
        1. c4 Nf6       2. d3 e5        3. Nf3 Nc6      4. e3 d5         
        5. cxd5 Nxd5    6. a3 Bg4       7. b4 a6        8. Be2 Bd6 
        9. Bb2 0–0      10.Nbd2 Re8     11.Rc1 Qf6      12.0–0 Rad8     
        13.Re1 Qh6      14.Ne4 Nf6      15.Nxd6 Rxd6    16.Qb3 Qg6 
        17.Kh1 e4       18.dxe4 Nxe4    19.Kg1 Qh5      20.Red1 Rg6     
        21.Kf1 Bh3      22.gxh3 Qxh3+   23.Ke1 Qg2      24.Rd7 Qh1+
        25.Bf1 Qxf3     26.Rc2 Rg1      27.Qd5 Rf8      28.Qd3 Qg2      
        29.Rxc7 Rd8     30.Bd4 Ne5      31.Bxe5 Rxf1+   32.Qxf1 Rd1+
        33.Kxd1 Qxf1# 0–1 
        */

        ///<summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string str = string.Empty;
            Enums.ChessmanSide manSide = Enums.ChessmanSide.Black;
            ChessStep expected;
            ChessStep actual;

            str = "O-O";
            expected = new ChessStep(Enums.ChessmanSide.Black, Enums.Castling.KingSide);
            actual = ChessStep.Parse(str, manSide);
            Assert.AreEqual(expected, actual);

            str = "O-O-O";
            expected = new ChessStep(Enums.ChessmanSide.Black, Enums.Castling.QueenSide);
            actual = ChessStep.Parse(str, manSide);
            Assert.AreEqual(expected, actual);

            str = "O - O - O";
            expected = new ChessStep(Enums.ChessmanSide.Black, Enums.Castling.QueenSide);
            actual = ChessStep.Parse(str, manSide);
            Assert.AreEqual(expected, actual);

            str = "Qxh3+";//后杀死h3的棋子，走到h3棋格，并将军
            expected = new ChessStep(Enums.ChessmanSide.Black, Enums.ChessmanType.Queen, new ChessSquare('h', 3), new ChessSquare(), Enums.AccessorialAction.KillAndCheck);
            actual = ChessStep.Parse(str, manSide);
            Assert.AreEqual(expected, actual);
        }
    }
}
