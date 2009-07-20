using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Gean.Wrapper.Chess.UnitTesting
{


    /// <summary>
    ///这是 SquareTest 的测试类，旨在
    ///包含所有 SquareTest 单元测试
    ///</summary>
    [TestClass()]
    public class SquareTest
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
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ChessSquare target = new ChessSquare(3, 4);
            string expected = "c4";

            string actual = target.ToString();
            Assert.AreEqual(expected, actual);

            target = new ChessSquare(8, 8);
            expected = "h8";
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            target = new ChessSquare(1, 1);
            expected = "a1";
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Check 的测试
        ///</summary>
        [TestMethod()]
        public void CheckTest()
        {
            int x = 0;
            int y = 0;
            bool expected;
            bool actual;

            expected = false;
            actual = ChessSquare.Check(x, y);
            Assert.AreEqual(expected, actual);

            x = 9; y = 9;
            expected = false;
            actual = ChessSquare.Check(x, y);
            Assert.AreEqual(expected, actual);

            x = -1; y = -1;
            expected = false;
            actual = ChessSquare.Check(x, y);
            Assert.AreEqual(expected, actual);

            x = 1; y = 1;
            expected = true;
            actual = ChessSquare.Check(x, y);
            Assert.AreEqual(expected, actual);

            x = 8; y = 8;
            expected = true;
            actual = ChessSquare.Check(x, y);
            Assert.AreEqual(expected, actual);

            x = 2; y = 7;
            expected = true;
            actual = ChessSquare.Check(x, y);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Square 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void SquareConstructorTest()
        {
            int x = 4; 
            int y = 4; 
            ChessSquare target = new ChessSquare(x, y);
            Assert.IsNotNull(target);

            try { target = new ChessSquare(0, 0); }
            catch (Exception e) { Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException)); }

            try { target = new ChessSquare(9, 9); }
            catch (Exception e) { Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException)); }

            try { target = new ChessSquare(-1, -1); }
            catch (Exception e) { Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException)); }

            try { target = new ChessSquare(-2, 10); }
            catch (Exception e) { Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException)); }

            try { target = new ChessSquare(4, 10); }
            catch (Exception e) { Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException)); }
            
            try { target = new ChessSquare(-9, 5); }
            catch (Exception e) { Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException)); }
        }
    }
}
