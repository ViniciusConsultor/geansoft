using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace Gean.Wrapper.Chess.UnitTesting
{


    /// <summary>
    ///这是 ChessPositionTest 的测试类，旨在
    ///包含所有 ChessPositionTest 单元测试
    ///</summary>
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

        /*   FEN Dot
         * 
         *   8 |     1   2   3   4   5   6   7   8 = (9-8)*8
         *   7 |     9  10  11  12  13  14  15  16 = (9-7)*8
         *   6 |    17  18  19  20  21  22  23  24 = (9-6)*8
         *   5 |    25  26  27  28  29  30  31  32 = (9-5)*8
         *   4 |    33  34  35  36  37  38  39  40 = (9-4)*8
         *   3 |    41  42  43  44  45  46  47  48 = (9-3)*8
         *   2 |    49  50  51  52  53  54  55  56 = (9-2)*8
         *   1 |    57  58  59  60  61  62  63  64 = (9-1)*8
         *          ------------------------------
         *           1   2   3   4   5   6   7   8
         *           a   b   c   d   e   f   g   h
         *   
         *   this.Dot = (9 - y) * x;
         *   
         */
        #region
        /// <summary>
        ///GetKnightPositions 的测试
        ///</summary>
        [TestMethod()]
        public void GetKnightPositionsTest()
        {
            ChessPosition pos; ChessPosition[] actual; List<int> dots; int[] ints;

            pos = ChessPosition.GetPositionByDot(25);
            ints = new int[] { 10, 19, 35, 42 };
            actual = pos.GetKnightPositions();
            Assert.AreEqual(4, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
                dots.Add(p.Dot);
            foreach (int i in ints)
                Assert.IsTrue(dots.Contains(i));

            pos = ChessPosition.GetPositionByDot(20);
            ints = new int[] { 3, 5, 10, 14, 26, 30, 35, 37 };
            actual = pos.GetKnightPositions();
            Assert.AreEqual(8, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
                dots.Add(p.Dot);
            foreach (int i in ints)
                Assert.IsTrue(dots.Contains(i));

            pos = ChessPosition.GetPositionByDot(58);
            ints = new int[] { 41, 43, 52 };
            actual = pos.GetKnightPositions();
            Assert.AreEqual(3, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
                dots.Add(p.Dot);
            foreach (int i in ints)
                Assert.IsTrue(dots.Contains(i));

            pos = ChessPosition.GetPositionByDot(64);
            ints = new int[] { 54, 47 };
            actual = pos.GetKnightPositions();
            Assert.AreEqual(2, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
                dots.Add(p.Dot);
            foreach (int i in ints)
                Assert.IsTrue(dots.Contains(i));
        }
        /// <summary>
        ///GetQueenPositions 的测试
        ///</summary>
        [TestMethod()]
        public void GetQueenPositionsTest()
        {
            ChessPosition pos; ChessPosition[] actual; List<int> dots; int[] ints;

            pos = ChessPosition.GetPositionByDot(28);
            actual = pos.GetQueenPositions();
            Assert.AreEqual(13 + 14, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
                dots.Add(p.Dot);
            ints = new int[] { 4, 12, 20, 36, 44, 52, 60, 25, 26, 27, 29, 30, 31, 32, 1, 10, 19, 37, 46, 55, 64, 49, 42, 35, 21, 14, 7 };
            foreach (int i in ints)
                Assert.IsTrue(dots.Contains(i));

            pos = ChessPosition.GetPositionByDot(1);
            actual = pos.GetQueenPositions();
            Assert.AreEqual(21, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
                dots.Add(p.Dot);
            ints = new int[] { 10, 19, 28, 37, 46, 55, 64, 2, 3, 4, 5, 6, 7, 8, 9, 17, 25, 33, 41, 49, 57 };
            foreach (int i in ints)
                Assert.IsTrue(dots.Contains(i));
        }
        /// <summary>
        ///GetBishopPositions 的测试
        ///</summary>
        [TestMethod()]
        public void GetBishopPositionsTest()
        {
            ChessPosition pos; ChessPosition[] actual; List<int> dots; int[] ints;

            pos = ChessPosition.GetPositionByDot(37);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(13, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 1, 10, 19, 28, 46, 55, 64, 58, 51, 44, 30, 23, 16 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(34);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(9, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 41, 27, 20, 13, 6, 25, 43, 52, 61 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(55);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(9, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 1, 10, 19, 28, 37, 46, 64, 62, 48 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(50);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(9, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 57, 43, 36, 29, 22, 15, 8, 41, 59 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(15);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(9, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 8, 22, 29, 36, 43, 50, 57, 6, 24 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(10);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(9, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 1, 19, 28, 37, 46, 55, 64, 3, 17 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(64);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(7, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 55, 46, 37, 28, 19, 10, 1 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(57);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(7, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 50, 43, 36, 29, 22, 15, 8 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(8);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(7, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 15, 22, 29, 36, 43, 50, 57 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(1);
            actual = pos.GetBishopPositions();
            Assert.AreEqual(7, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 10, 19, 28, 37, 46, 55, 64 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }
        }
        /// <summary>
        ///GetRookPositions 的测试
        ///</summary>
        [TestMethod()]
        public void GetRookPositionsTest()
        {
            ChessPosition pos = new ChessPosition(2, 7);
            ChessPosition[] actual;

            pos = new ChessPosition(4, 4);
            actual = pos.GetRookPositions();
            Assert.AreEqual(14, actual.Length);

            pos = new ChessPosition(1, 1);
            actual = pos.GetRookPositions();
            Assert.AreEqual(14, actual.Length);

            pos = new ChessPosition(1, 8);
            actual = pos.GetRookPositions();
            Assert.AreEqual(14, actual.Length);

            pos = new ChessPosition(8, 1);
            actual = pos.GetRookPositions();
            Assert.AreEqual(14, actual.Length);

            pos = new ChessPosition(8, 8);
            actual = pos.GetRookPositions();
            Assert.AreEqual(14, actual.Length);
        }
        /// <summary>
        ///GetPawnPositions 的测试
        ///</summary>
        [TestMethod()]
        public void GetPawnPositionsTest()
        {
            ChessPosition pos;
            Enums.ChessmanSide side = Enums.ChessmanSide.Black;
            ChessPosition[] actual;

            pos = new ChessPosition(2, 7);
            side = Enums.ChessmanSide.Black;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(4, actual.Length);
            Assert.AreEqual(18, actual[0].Dot);
            Assert.AreEqual(26, actual[1].Dot);
            Assert.AreEqual(17, actual[2].Dot);
            Assert.AreEqual(19, actual[3].Dot);

            pos = ChessPosition.GetPositionByDot(50);
            side = Enums.ChessmanSide.White;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(4, actual.Length);
            Assert.AreEqual(42, actual[0].Dot);
            Assert.AreEqual(34, actual[1].Dot);
            Assert.AreEqual(41, actual[2].Dot);
            Assert.AreEqual(43, actual[3].Dot);

            pos = ChessPosition.GetPositionByDot(25);

            side = Enums.ChessmanSide.White;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(17, actual[0].Dot);
            Assert.AreEqual(18, actual[1].Dot);

            side = Enums.ChessmanSide.Black;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(33, actual[0].Dot);
            Assert.AreEqual(34, actual[1].Dot);

            pos = ChessPosition.GetPositionByDot(40);

            side = Enums.ChessmanSide.White;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(32, actual[0].Dot);
            Assert.AreEqual(31, actual[1].Dot);

            side = Enums.ChessmanSide.Black;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(48, actual[0].Dot);
            Assert.AreEqual(47, actual[1].Dot);

            pos = ChessPosition.GetPositionByDot(9);

            side = Enums.ChessmanSide.Black;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(17, actual[0].Dot);
            Assert.AreEqual(25, actual[1].Dot);
            Assert.AreEqual(18, actual[2].Dot);

            pos = ChessPosition.GetPositionByDot(16);

            side = Enums.ChessmanSide.Black;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(24, actual[0].Dot);
            Assert.AreEqual(32, actual[1].Dot);
            Assert.AreEqual(23, actual[2].Dot);

            pos = ChessPosition.GetPositionByDot(49);

            side = Enums.ChessmanSide.White;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(41, actual[0].Dot);
            Assert.AreEqual(33, actual[1].Dot);
            Assert.AreEqual(42, actual[2].Dot);

            pos = ChessPosition.GetPositionByDot(56);

            side = Enums.ChessmanSide.White;
            actual = pos.GetPawnPositions(side);
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(48, actual[0].Dot);
            Assert.AreEqual(40, actual[1].Dot);
            Assert.AreEqual(47, actual[2].Dot);

        }
        /// <summary>
        ///GetKingPositions 的测试
        ///</summary>
        [TestMethod()]
        public void GetKingPositionsTest()
        {
            ChessPosition pos;
            ChessPosition[] actual;
            List<int> dots;
            int[] ints;

            pos = ChessPosition.GetPositionByDot(10);
            actual = pos.GetKingPositions();
            Assert.AreEqual(8, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 1, 2, 3, 17, 18, 19, 9, 11 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(28);
            actual = pos.GetKingPositions();
            Assert.AreEqual(8, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 19, 20, 21, 27, 29, 35, 36, 37 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(55);
            actual = pos.GetKingPositions();
            Assert.AreEqual(8, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 46, 47, 48, 62, 63, 64, 54, 56 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(1);
            actual = pos.GetKingPositions();
            Assert.AreEqual(3, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 2, 9, 10 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(8);
            actual = pos.GetKingPositions();
            Assert.AreEqual(3, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 7, 15, 16 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(57);
            actual = pos.GetKingPositions();
            Assert.AreEqual(3, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 49, 50, 58 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(64);
            actual = pos.GetKingPositions();
            Assert.AreEqual(3, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 55, 56, 63 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(4);
            actual = pos.GetKingPositions();
            Assert.AreEqual(5, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 3, 5, 11, 12, 13 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(25);
            actual = pos.GetKingPositions();
            Assert.AreEqual(5, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 17, 18, 26, 33, 34 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(60);
            actual = pos.GetKingPositions();
            Assert.AreEqual(5, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 51, 52, 53, 59, 61 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }

            pos = ChessPosition.GetPositionByDot(40);
            actual = pos.GetKingPositions();
            Assert.AreEqual(5, actual.Length);
            dots = new List<int>();
            foreach (ChessPosition p in actual)
            {
                dots.Add(p.Dot);
            }
            ints = new int[] { 31, 32, 47, 48, 39 };
            foreach (int i in ints)
            {
                Assert.IsTrue(dots.Contains(i));
            }


        }

        /// <summary>
        ///GetPositionByDot 的测试
        ///</summary>
        [TestMethod()]
        public void GetPositionByDotTest()
        {
            int dot = 0;
            ChessPosition expected;
            ChessPosition actual;

            //四个角
            dot = 1;
            expected = new ChessPosition(1, 8);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 8;
            expected = new ChessPosition(8, 8);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 57;
            expected = new ChessPosition(1, 1);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 64;
            expected = new ChessPosition(8, 1);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 10;
            expected = new ChessPosition(2, 7);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 33;
            expected = new ChessPosition(1, 4);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 44;
            expected = new ChessPosition(4, 3);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 55;
            expected = new ChessPosition(7, 2);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 40;
            expected = new ChessPosition(8, 4);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

            dot = 48;
            expected = new ChessPosition(8, 3);
            actual = ChessPosition.GetPositionByDot(dot);
            Assert.AreEqual(expected, actual);

        }
        #endregion

    }
}
