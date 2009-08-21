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


        /// <summary>
        ///Peek 的测试
        ///</summary>
        [TestMethod()]
        public void PeekTest()
        {
            ChessSequence target = new ChessSequence(); // TODO: 初始化为适当的值
            ChessStep expected = null; // TODO: 初始化为适当的值
            ChessStep actual;
            actual = target.Peek();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///PeekPair 的测试
        ///</summary>
        [TestMethod()]
        public void PeekPairTest()
        {
            ChessSequence target = new ChessSequence(); // TODO: 初始化为适当的值
            ChessStep[] expected = null; // TODO: 初始化为适当的值
            ChessStep[] actual;
            actual = target.PeekPair();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ChessSequence target = new ChessSequence(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Add 的测试
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            ChessSequence target = new ChessSequence(); // TODO: 初始化为适当的值
            ISequenceItem item = null; // TODO: 初始化为适当的值
            target.Add(item);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }
    }
}
