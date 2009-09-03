using Gean.Module.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gean.Module.Chess.UnitTesting
{
    
    [TestClass()]
    public class StepTest
    {
        #region

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        private TestContext testContextInstance;

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
            string value = "";
            Step expected;
            Step step = new Step();

            value = "b4";
            step.Parse(value);
            expected = new Step();
            expected.Number = 0;
            expected.PieceType = Enums.PieceType.WhitePawn;
            expected.SourcePosition = Position.Empty;
            expected.TargetPosition = new Position('b', 4);
            expected.Actions.Add(Enums.Action.General);
            expected.PromotionPieceType = Enums.PieceType.None;
            Assert.AreEqual(expected, step);
        }
    }
}
