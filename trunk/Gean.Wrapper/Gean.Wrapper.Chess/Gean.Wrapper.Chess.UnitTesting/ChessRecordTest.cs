using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessRecordTest 的测试类，旨在
    ///包含所有 ChessRecordTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessRecordTest
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

        public ChessRecordTest()
        {
            this.DemoFilePath = Path.GetFullPath(@"..\..\..\..\Gean.Wrapper.Chess\Gean.Wrapper.Chess.UnitTesting\CaseFiles\");
            this.PGNFiles = Directory.GetFiles(this.DemoFilePath, "*.pgn");
        }

        private string DemoFilePath { get; set; }
        private string[] PGNFiles { get; set; }

        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ChessRecordForeachTest()
        {
            ChessRecordFile records = new ChessRecordFile();
            PGNReader target = new PGNReader();
            target.Filename = this.PGNFiles[0];
            target.AddEvents(records);
            target.Parse();

            ChessRecord record = records[0];

            List<ChessStep> steps = new List<ChessStep>();
            foreach (ChessStep step in record)
            {
                steps.Add(step);
                Assert.IsNotNull(step);
            }
            Assert.AreEqual(90, steps.Count);
        }
    }
}
