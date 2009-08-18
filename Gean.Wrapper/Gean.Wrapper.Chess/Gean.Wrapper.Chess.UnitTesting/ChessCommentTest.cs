using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessCommentTest 的测试类，旨在
    ///包含所有 ChessCommentTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessCommentTest
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
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string value;
            ChessComment expected = new ChessComment();

            value = "{ <ab1234cd@ggmail.com.cn> 这是一个测试的注释，This is a Comment! }";
            expected.UserID = "ab1234cd@ggmail.com.cn";
            expected.Comment = "这是一个测试的注释，This is a Comment!";
            ChessComment actual = ChessComment.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "{ <ab1234cd@ggmail> 这是一个测试的注释，This is a Comment! }";
            expected.UserID = "";
            expected.Comment = "<ab1234cd@ggmail> 这是一个测试的注释，This is a Comment!";
            actual = ChessComment.Parse(value);
            Assert.AreEqual(expected, actual);
        }
    }
}
