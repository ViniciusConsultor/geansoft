using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessSequenceTest 的测试类，旨在
    ///包含所有 ChessSequenceTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessSequenceTest
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
            string sequence = string.Empty;
            ChessMainSequence expected = null;
            ChessMainSequence actual = null;


            sequence = "1. c4 Nf6 2. d3 e5 3. cxd5 Nxd5 4. a3 Bg4 5. b4 a6";
            expected = new ChessMainSequence();
            ChessPosition e = ChessPosition.Empty;
            ChessStep w = null;
            ChessStep b = null;
            ChessStepPair pair = null;
            //1. c4 Nf6
            w = new ChessStep(Enums.Action.General, Enums.ChessmanType.Pawn, e, new ChessPosition(3, 4));
            b = new ChessStep(Enums.Action.General, Enums.ChessmanType.Knight, e, new ChessPosition(6, 6));
            pair = new ChessStepPair(1, w, b);
            expected.Add(pair);
            //2. d3 e5
            w = new ChessStep(Enums.Action.General, Enums.ChessmanType.Pawn, e, new ChessPosition(4, 3));
            b = new ChessStep(Enums.Action.General, Enums.ChessmanType.Pawn, e, new ChessPosition(5, 5));
            pair = new ChessStepPair(2, w, b);
            expected.Add(pair);
            //3. cxd5 Nxd5
            ChessPosition tmp = new ChessPosition('c', 4);
            w = new ChessStep(Enums.Action.Kill, Enums.ChessmanType.Pawn, tmp, new ChessPosition(4, 5));
            b = new ChessStep(Enums.Action.Kill, Enums.ChessmanType.Knight, e, new ChessPosition(4, 5));
            pair = new ChessStepPair(3, w, b);
            expected.Add(pair);
            //4. a3 Bg4
            w = new ChessStep(Enums.Action.General, Enums.ChessmanType.Pawn, e, new ChessPosition(1, 3));
            b = new ChessStep(Enums.Action.General, Enums.ChessmanType.Bishop, e, new ChessPosition(7, 4));
            pair = new ChessStepPair(4, w, b);
            expected.Add(pair);
            //5. b4 a6
            w = new ChessStep(Enums.Action.General, Enums.ChessmanType.Pawn, e, new ChessPosition(2, 4));
            b = new ChessStep(Enums.Action.General, Enums.ChessmanType.Pawn, e, new ChessPosition(1, 6));
            pair = new ChessStepPair(5, w, b);
            expected.Add(pair);

            actual = ChessMainSequence.Parse(sequence);
            Assert.AreEqual(expected, actual);
        }
    }
}
