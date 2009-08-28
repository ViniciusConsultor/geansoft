using Gean.Module.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Module.Chess.UnitTesting
{
    /*   FEN Dot
     * 
     *   8 |     1   2   3   4   5   6   7   8
     *   7 |     9  10  11  12  13  14  15  16
     *   6 |    17  18  19  20  21  22  23  24
     *   5 |    25  26  27  28  29  30  31  32
     *   4 |    33  34  35  36  37  38  39  40
     *   3 |    41  42  43  44  45  46  47  48
     *   2 |    49  50  51  52  53  54  55  56
     *   1 |    57  58  59  60  61  62  63  64
     *          ------------------------------
     *           1   2   3   4   5   6   7   8
     *           a   b   c   d   e   f   g   h
     *   
     *   this.Dot = (8 * (7 - _y)) + (_x + 1);
     */
    
    [TestClass()]
    public class ChessPositionTest
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

        [TestMethod()]
        public void GetPositionByDotTest()
        {
            Position actual;
            for (int i = 1; i <= 64; i++)
            {
                actual = Position.GetPositionByDot(i);
                Assert.AreEqual(i, actual.Dot);
            }
        }

        [TestMethod()]
        public void ParseTest()
        {
            string rank = "abcdefgh";
            int x = 0;
            for (int j = 8; j >= 1; j--)
            {
                for (int i = 1; i <= 8; i++)
                {
                    int dot = x * 8 + i;
                    string value = rank[i - 1].ToString() + j.ToString();
                    Position pos = Position.Parse(value);
                    Assert.AreEqual(value, pos.ToString());
                    Assert.AreEqual(dot, pos.Dot);
                }
                x++;
            }
        }
    }
}
