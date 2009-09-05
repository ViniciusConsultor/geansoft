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
    public class PositionTest
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
        public void CalculateDotTest()
        {
            Assert.AreEqual(1, Position.CalculateDot(1, 8));
            Assert.AreEqual(2, Position.CalculateDot(2, 8));
            Assert.AreEqual(3, Position.CalculateDot(3, 8));
            Assert.AreEqual(4, Position.CalculateDot(4, 8));
            Assert.AreEqual(5, Position.CalculateDot(5, 8));
            Assert.AreEqual(6, Position.CalculateDot(6, 8));
            Assert.AreEqual(7, Position.CalculateDot(7, 8));
            Assert.AreEqual(8, Position.CalculateDot(8, 8));
            Assert.AreEqual(9, Position.CalculateDot(1, 7));
            Assert.AreEqual(10, Position.CalculateDot(2, 7));
            Assert.AreEqual(11, Position.CalculateDot(3, 7));
            Assert.AreEqual(12, Position.CalculateDot(4, 7));
            Assert.AreEqual(13, Position.CalculateDot(5, 7));
            Assert.AreEqual(14, Position.CalculateDot(6, 7));
            Assert.AreEqual(15, Position.CalculateDot(7, 7));
            Assert.AreEqual(16, Position.CalculateDot(8, 7));
            Assert.AreEqual(17, Position.CalculateDot(1, 6));
            Assert.AreEqual(18, Position.CalculateDot(2, 6));
            Assert.AreEqual(19, Position.CalculateDot(3, 6));
            Assert.AreEqual(20, Position.CalculateDot(4, 6));
            Assert.AreEqual(21, Position.CalculateDot(5, 6));
            Assert.AreEqual(22, Position.CalculateDot(6, 6));
            Assert.AreEqual(23, Position.CalculateDot(7, 6));
            Assert.AreEqual(24, Position.CalculateDot(8, 6));
            Assert.AreEqual(25, Position.CalculateDot(1, 5));
            Assert.AreEqual(26, Position.CalculateDot(2, 5));
            Assert.AreEqual(27, Position.CalculateDot(3, 5));
            Assert.AreEqual(28, Position.CalculateDot(4, 5));
            Assert.AreEqual(29, Position.CalculateDot(5, 5));
            Assert.AreEqual(30, Position.CalculateDot(6, 5));
            Assert.AreEqual(31, Position.CalculateDot(7, 5));
            Assert.AreEqual(32, Position.CalculateDot(8, 5));
            Assert.AreEqual(33, Position.CalculateDot(1, 4));
            Assert.AreEqual(34, Position.CalculateDot(2, 4));
            Assert.AreEqual(35, Position.CalculateDot(3, 4));
            Assert.AreEqual(36, Position.CalculateDot(4, 4));
            Assert.AreEqual(37, Position.CalculateDot(5, 4));
            Assert.AreEqual(38, Position.CalculateDot(6, 4));
            Assert.AreEqual(39, Position.CalculateDot(7, 4));
            Assert.AreEqual(40, Position.CalculateDot(8, 4));
            Assert.AreEqual(41, Position.CalculateDot(1, 3));
            Assert.AreEqual(42, Position.CalculateDot(2, 3));
            Assert.AreEqual(43, Position.CalculateDot(3, 3));
            Assert.AreEqual(44, Position.CalculateDot(4, 3));
            Assert.AreEqual(45, Position.CalculateDot(5, 3));
            Assert.AreEqual(46, Position.CalculateDot(6, 3));
            Assert.AreEqual(47, Position.CalculateDot(7, 3));
            Assert.AreEqual(48, Position.CalculateDot(8, 3));
            Assert.AreEqual(49, Position.CalculateDot(1, 2));
            Assert.AreEqual(50, Position.CalculateDot(2, 2));
            Assert.AreEqual(51, Position.CalculateDot(3, 2));
            Assert.AreEqual(52, Position.CalculateDot(4, 2));
            Assert.AreEqual(53, Position.CalculateDot(5, 2));
            Assert.AreEqual(54, Position.CalculateDot(6, 2));
            Assert.AreEqual(55, Position.CalculateDot(7, 2));
            Assert.AreEqual(56, Position.CalculateDot(8, 2));
            Assert.AreEqual(57, Position.CalculateDot(1, 1));
            Assert.AreEqual(58, Position.CalculateDot(2, 1));
            Assert.AreEqual(59, Position.CalculateDot(3, 1));
            Assert.AreEqual(60, Position.CalculateDot(4, 1));
            Assert.AreEqual(61, Position.CalculateDot(5, 1));
            Assert.AreEqual(62, Position.CalculateDot(6, 1));
            Assert.AreEqual(63, Position.CalculateDot(7, 1));
            Assert.AreEqual(64, Position.CalculateDot(8, 1));
        }

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

        [TestMethod()]
        public void CalculateXYTest()
        {
            int x = 0; 
            int y = 0;

            Position.CalculateXY(1, out x, out y);
            Assert.AreEqual(1, x); Assert.AreEqual(8, y);
            Position.CalculateXY(8, out x, out y);
            Assert.AreEqual(8, x); Assert.AreEqual(8, y);
            Position.CalculateXY(57, out x, out y);
            Assert.AreEqual(1, x); Assert.AreEqual(1, y);
            Position.CalculateXY(64, out x, out y);
            Assert.AreEqual(8, x); Assert.AreEqual(1, y);
            Position.CalculateXY(25, out x, out y);
            Assert.AreEqual(1, x); Assert.AreEqual(5, y);
            Position.CalculateXY(32, out x, out y);
            Assert.AreEqual(8, x); Assert.AreEqual(5, y);
            Position.CalculateXY(50, out x, out y);
            Assert.AreEqual(2, x); Assert.AreEqual(2, y);
            Position.CalculateXY(15, out x, out y);
            Assert.AreEqual(7, x); Assert.AreEqual(7, y);
            Position.CalculateXY(22, out x, out y);
            Assert.AreEqual(6, x); Assert.AreEqual(6, y);
            Position.CalculateXY(4, out x, out y);
            Assert.AreEqual(4, x); Assert.AreEqual(8, y);
            Position.CalculateXY(62, out x, out y);
            Assert.AreEqual(6, x); Assert.AreEqual(1, y);
        }
    }
}
