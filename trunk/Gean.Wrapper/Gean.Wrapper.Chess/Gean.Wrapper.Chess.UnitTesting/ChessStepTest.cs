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

        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ChessStep target = null;
            string expected = null;
            string actual = null;

            target = new ChessStep(Enums.ChessmanType.Rook, Enums.Action.KillAndCheck, new ChessGrid('e', 3), new ChessGrid('e', 5));
            target.CommentIndexs.Add(11);
            target.CommentIndexs.Add(12);
            target.CommentIndexs.Add(13);
            target.ChoiceStepsIndexs.Add(34);
            target.ChoiceStepsIndexs.Add(35);
            target.ChoiceStepsIndexs.Add(36);
            target.ChoiceStepsIndexs.Add(37);
            
            expected = "Rxe5+(11,12,13)[34,35,36,37]";
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            target = new ChessStep(Enums.ChessmanType.Pawn, Enums.Action.KillAndCheck, new ChessGrid('f', 2), new ChessGrid('g', 3));
            target.CommentIndexs.Add(11);
            target.ChoiceStepsIndexs.Add(34);
            target.ChoiceStepsIndexs.Add(35);

            expected = "fxg3+(11)[34,35]";
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

        }

        ///<summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string value = string.Empty;
            ChessStep expected;
            ChessStep actual;

            value = "Rxb4+(33,35,36)[12,13,14,15]";
            expected = new ChessStep
                (Enums.ChessmanType.Rook, Enums.Action.KillAndCheck, ChessGrid.Empty, new ChessGrid('b', 4));
            expected.CommentIndexs.Add(33);
            expected.CommentIndexs.Add(35);
            expected.CommentIndexs.Add(36);
            expected.ChoiceStepsIndexs.Add(12);
            expected.ChoiceStepsIndexs.Add(13);
            expected.ChoiceStepsIndexs.Add(14);
            expected.ChoiceStepsIndexs.Add(15);
            actual = ChessStep.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "O-O";
            expected = new ChessStep(Enums.Castling.KingSide);
            actual = ChessStep.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "O-O-O";
            expected = new ChessStep(Enums.Castling.QueenSide);
            actual = ChessStep.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "O - O - O";
            expected = new ChessStep(Enums.Castling.QueenSide);
            actual = ChessStep.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "Qxh3+";//后杀死h3的棋子，走到h3棋格，并将军
            expected = new ChessStep(
                Enums.ChessmanType.Queen, Enums.Action.KillAndCheck, ChessGrid.Empty, new ChessGrid('h', 3));
            actual = ChessStep.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "b5+";
            expected = new ChessStep(
                Enums.ChessmanType.Pawn, Enums.Action.Check, ChessGrid.Empty, new ChessGrid('b', 5));
            actual = ChessStep.Parse(value);
            Assert.AreEqual(expected, actual);

        }


    }
}
