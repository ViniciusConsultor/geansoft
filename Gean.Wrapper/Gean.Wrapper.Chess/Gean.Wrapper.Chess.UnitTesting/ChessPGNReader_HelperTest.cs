using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;

namespace Gean.Wrapper.Chess.UnitTesting
{


    /// <summary>
    ///这是 ChessPGNReader_HelperTest 的测试类，旨在
    ///包含所有 ChessPGNReader_HelperTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessPGNReader_HelperTest
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

        private string _BasePath;
        private string _CaseFilePath;

        public ChessPGNReader_HelperTest()
        {
            _BasePath = Path.GetFullPath("..\\..\\..\\" + this.GetType().Assembly.FullName.Substring(0, this.GetType().Assembly.FullName.IndexOf(',')));
            _CaseFilePath = Path.Combine(_BasePath, "CaseFiles\\");
        }


        /// <summary>
        ///GetPGNsByTextReader 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Gean.Wrapper.Chess.dll")]
        public void GetPGNsByTextReaderTest()
        {
            string filepath = Path.Combine(_CaseFilePath, "_Test.pgn");
            Debug.Assert(File.Exists(filepath), "");
            TextReader reader = new StreamReader(filepath);
            ChessRecord[] expected = null; 
            ChessRecord[] actual;
            actual = ChessPGNReader_Accessor.Helper.GetPGNsByTextReader(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ParseLine 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Gean.Wrapper.Chess.dll")]
        public void ParseLineTest()
        {
            string line;
            ChessPGNReader_Accessor.StringPair expected;
            ChessPGNReader_Accessor.StringPair actual;

            line = "[Event \"Wch U16\"]\r\n";
            expected = new ChessPGNReader_Accessor.StringPair("Event", "Wch U16");
            actual = ChessPGNReader_Accessor.Helper.ParseLine(line);
            Assert.AreEqual(expected.Key, actual.Key);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected, actual);

            line = "Site \"Beijing\"";
            expected = new ChessPGNReader_Accessor.StringPair("Site", "Beijing");
            actual = ChessPGNReader_Accessor.Helper.ParseLine(line);
            Assert.AreEqual(expected.Key, actual.Key);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected, actual);

            line = "Date \"1993.??.??\"";
            expected = new ChessPGNReader_Accessor.StringPair("Date", "1993.??.??");
            actual = ChessPGNReader_Accessor.Helper.ParseLine(line);
            Assert.AreEqual(expected.Key, actual.Key);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected, actual);

            line = "Result \"1/2-1/2\"";
            expected = new ChessPGNReader_Accessor.StringPair("Result", "1/2-1/2");
            actual = ChessPGNReader_Accessor.Helper.ParseLine(line);
            Assert.AreEqual(expected.Key, actual.Key);
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected, actual);
        }

    }
}
/*
[Event "WCh"]
[Site "Bonn GER"]
[Date "2008.10.14"]
[Round "1"]
[White "Kramnik,V"]
[Black "Anand,V"]
[Result "1/2-1/2"]
[WhiteELO "2772"]
[BlackELO "2783"]

1. d4 d5 2. c4 {c4冲击中心} 2... c6 3. Nc3 Nf6 { 快出子,保护d5兵,防御白e4兵的挺进} 4. cxd5
cxd5 5. Bf4 Nc6 6. e3 {保护d4兵,通白格象路} 6... Bf5 7. Nf3 e6 8. Qb3 Bb4 9. Bb5 O-O
10. Bxc6 Bxc3+ 11. Qxc3 Rc8 {黑车好棋,牵制白后} 12. Ne5 Ng4 13. Nxg4 Bxg4 14. Qb4
Rxc6 15. Qxb7 Qc8 16. Qxc8 Rfxc8 17. O-O a5 18. f3 Bf5 19. Rfe1 Bg6 20. b3 f6
21. e4 dxe4 22. fxe4 Rd8 23. Rad1 Rc2 24. e5 fxe5 25. Bxe5 Rxa2 26. Ra1 Rxa1
27. Rxa1 Rd5 28. Rc1 Rd7 29. Rc5 Ra7 30. Rc7 Rxc7 31. Bxc7 Bc2 32. Bxa5 Bxb3
1/2-1/2
*/