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
        
        ///<summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string comment;
            ChessComment expected;
            ChessComment actual;

            comment = "#ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD";
            actual = ChessComment.Parse(comment);
            Assert.AreEqual(comment.Substring(1), actual.Comment);
            Assert.AreEqual("", actual.UserID);
            Assert.IsTrue(actual.Number > 0 && actual.Number < int.MaxValue);

            comment = "#324234#ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD";
            actual = ChessComment.Parse(comment);
            Assert.AreEqual("ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD", actual.Comment);
            Assert.AreEqual("", actual.UserID);
            Assert.AreEqual(324234, actual.Number);

            comment = "#324234#*************#ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD";
            actual = ChessComment.Parse(comment);
            Assert.AreEqual("ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD_ABCD", actual.Comment);
            Assert.AreEqual("", actual.UserID);
            Assert.AreEqual(324234, actual.Number);

            comment = "#232#myemail1234@usa.com.cn#///包含所有# ChessCommentTest 单元测试";
            expected = new ChessComment("myemail1234@usa.com.cn", "///包含所有# ChessCommentTest 单元测试", 232);
            actual = ChessComment.Parse(comment);
            Assert.AreEqual(expected, actual);

            comment = "#987654321#sim123sim456@51.public.net.cn#ABCD#EFGH#IJKLMNOPQRSTUVWXYZ";
            expected = new ChessComment("sim123sim456@51.public.net.cn", "ABCD#EFGH#IJKLMNOPQRSTUVWXYZ", 987654321);
            actual = ChessComment.Parse(comment);
            Assert.AreEqual(expected, actual);

            comment = "#88#a@a.b.c.d#————comment————";
            expected = new ChessComment("a@a.b.c.d", "————comment————", 88);
            actual = ChessComment.Parse(comment);
            Assert.AreEqual(expected, actual);

        }
    }
}
