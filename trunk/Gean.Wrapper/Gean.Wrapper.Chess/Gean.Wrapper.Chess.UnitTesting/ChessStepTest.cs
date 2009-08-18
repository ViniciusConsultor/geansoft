using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gean.Utility;

namespace Gean.Wrapper.Chess.UnitTesting
{

    /// <summary>
    ///这是 ChessStepTest 的测试类，旨在包含所有 ChessStepTest 单元测试
    ///</summary>
    [TestClass()]
    public class ChessStepTest
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

        public ChessStepTest()
        {
            this.DemoFilePath = Path.GetFullPath(@"..\..\..\..\Gean.Wrapper.Chess\Gean.Wrapper.Chess.UnitTesting\CaseFiles\");
            this.PGNFiles = Directory.GetFiles(this.DemoFilePath, "*.pgn");
        }

        private string DemoFilePath { get; set; }
        private string[] PGNFiles { get; set; }

        ///<summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest1()
        {

            string value = string.Empty;
            ChessStep expected;
            ChessStep actual;

            value = "O-O";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.None, ChessPosition.Empty, ChessPosition.Empty, Enums.Action.KingSideCastling);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "O-O-O";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.None, ChessPosition.Empty, ChessPosition.Empty, Enums.Action.QueenSideCastling);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "O - O - O";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.None, ChessPosition.Empty, ChessPosition.Empty, Enums.Action.QueenSideCastling);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "b5+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, ChessPosition.Empty, new ChessPosition(2, 5), Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Qxh3+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Queen, ChessPosition.Empty, new ChessPosition(8, 3), Enums.Action.Capture, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Rxb4+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Rook, ChessPosition.Empty, new ChessPosition(2, 4), Enums.Action.Capture, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "axb5+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition(1, 4), new ChessPosition(2, 5), Enums.Action.Capture, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "axb5+";
            expected = new ChessStep(Enums.ChessmanSide.Black, Enums.ChessmanType.Pawn, new ChessPosition(1, 6), new ChessPosition(2, 5), Enums.Action.Capture, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.Black);
            Assert.AreEqual(expected, actual);

            value = "Qh3";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Queen, ChessPosition.Empty, new ChessPosition(8, 3), Enums.Action.General);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "a3";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, ChessPosition.Empty, new ChessPosition(1, 3), Enums.Action.General);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "e8=Q";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, ChessPosition.Empty, new ChessPosition('e', 8), Enums.Action.Promotion);
            expected.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "fxe8=Q+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('f', 7), new ChessPosition('e', 8), Enums.Action.Promotion, Enums.Action.Check, Enums.Action.Capture);
            expected.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "cxd8=N+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('c', 7), new ChessPosition('d', 8), Enums.Action.Capture, Enums.Action.Check, Enums.Action.Promotion);
            expected.PromotionChessmanType = Enums.ChessmanType.Knight;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "cxd1=Q+";
            expected = new ChessStep(Enums.ChessmanSide.Black, Enums.ChessmanType.Pawn, new ChessPosition('c', 2), new ChessPosition('d', 1), Enums.Action.Promotion, Enums.Action.Capture, Enums.Action.Check);
            expected.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.Black);
            Assert.AreEqual(expected, actual);

            value = "gxf1=Q+";
            expected = new ChessStep(Enums.ChessmanSide.Black, Enums.ChessmanType.Pawn, new ChessPosition('g', 2), new ChessPosition('f', 1), Enums.Action.Check, Enums.Action.Capture, Enums.Action.Promotion);
            expected.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.Black);
            Assert.AreEqual(expected, actual);

            value = "fxe5+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('f', 4), new ChessPosition('e', 5), Enums.Action.Capture, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Bfe5";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Bishop, new ChessPosition('f', 5), new ChessPosition('e', 5), Enums.Action.General);
            expected.HasSame = Enums.SameOrientation.Horizontal;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Nac7";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Knight, new ChessPosition('a', 7), new ChessPosition('c', 7), Enums.Action.General);
            expected.HasSame = Enums.SameOrientation.Horizontal;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "N5c7";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Knight, new ChessPosition('c', 5), new ChessPosition('c', 7), Enums.Action.General);
            expected.HasSame = Enums.SameOrientation.Vertical;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "ac7";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('a', 7), new ChessPosition('c', 7), Enums.Action.General);
            expected.HasSame = Enums.SameOrientation.Horizontal;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "axb7+";
            expected = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('a', 6), new ChessPosition('b', 7), Enums.Action.Check, Enums.Action.Capture);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void ParseTest2()
        {
            Duration duration = new Duration();
            duration.Start();
            StringBuilder error = new StringBuilder();
            string filename = Path.Combine(this.DemoFilePath, "__ChessStep.txt");

            if (!File.Exists(filename))//因测试太耗时，需测试时将上行代码中的文件名中的“_”删除。
            {
                return;
            }

            string[] steps = File.ReadAllLines(filename);
            ChessStep step = null;

            foreach (string value in steps)
            {
                try
                {
                    step = ChessStep.Parse(value, Enums.ChessmanSide.White);
                }
                catch
                {
                    error.AppendLine(value);
                }
                Assert.IsNotNull(step);
            }

            duration.Stop();
            error.AppendLine().Append("共 " + steps.Length.ToString() + " 个 ChessStep，")
                .Append("总时间：").AppendLine(duration.DurationValue.ToString())
                .AppendLine("每ChessStep的解析时间为：" + (duration.DurationValue / steps.Length).ToString());

            string newFile = filename + ".error.txt";
            File.Delete(newFile);
            StreamWriter file = File.CreateText(newFile);
            file.Write(error.ToString());
            file.Flush();
            file.Close();
            using (Process notepad = new Process())
            {
                notepad.StartInfo.FileName = "notepad.exe";
                notepad.StartInfo.Arguments = newFile;
                notepad.Start();
            }
        }

        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string expected;
            string actual;
            ChessStep step;

            expected = "fe8";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('f', 8), new ChessPosition('e', 8), Enums.Action.General);
            step.HasSame = Enums.SameOrientation.Horizontal;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

            expected = "Rfe8";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Rook, new ChessPosition('f', 8), new ChessPosition('e', 8), Enums.Action.General);
            step.HasSame = Enums.SameOrientation.Horizontal;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

            expected = "Nfe8";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Knight, new ChessPosition('f', 8), new ChessPosition('e', 8), Enums.Action.General);
            step.HasSame = Enums.SameOrientation.Horizontal;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

            expected = "Qfe8";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Queen, new ChessPosition('f', 8), new ChessPosition('e', 8), Enums.Action.General);
            step.HasSame = Enums.SameOrientation.Horizontal;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

            expected = "Nfxe8+";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Knight, new ChessPosition('f', 8), new ChessPosition('e', 8), Enums.Action.Check, Enums.Action.Capture);
            step.HasSame = Enums.SameOrientation.Horizontal;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

            expected = "Rfxe8+";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Rook, new ChessPosition('f', 8), new ChessPosition('e', 8), Enums.Action.Check, Enums.Action.Capture);
            step.HasSame = Enums.SameOrientation.Horizontal;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

            expected = "cxd1=Q+";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('c', 2), new ChessPosition('d', 1), Enums.Action.Promotion, Enums.Action.Capture, Enums.Action.Check);
            step.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

            expected = "axb8=Q+";
            step = new ChessStep(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn, new ChessPosition('a', 7), new ChessPosition('b', 8), Enums.Action.Promotion, Enums.Action.Capture, Enums.Action.Check);
            step.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = step.ToString();
            Assert.AreEqual(expected, actual);

        }

    }
}

/* 生成ChessStep步数的测试文件

Regex regex = new Regex(@"\[.*\]");
string filename = Path.Combine(this.DemoFilePath, "ChessStepL.txt");
string[] strs = File.ReadAllLines(filename);
StringBuilder sb = new StringBuilder();
foreach (string item in strs)
{
    if (string.IsNullOrEmpty(item))
        continue;
    string newItem = item.Trim();
    if (regex.IsMatch(newItem))
        continue;
    if (newItem == "\r\n")
        continue;
    sb.Append(newItem).Append(' ');
}
strs = null;
string[] pairs = (new Regex(@"\b\d+.")).Split(sb.ToString());
sb = new StringBuilder();
foreach (var item in pairs)
{
    string[] ss = item.Split(' ');
    foreach (var subitem in ss)
    {
        if (string.IsNullOrEmpty(subitem))
            continue;
        string newItem = subitem.Trim();
        if (newItem == "\r\n")
            continue;
        sb.AppendLine(subitem);
    }
}
string newFile = filename + ".new.txt";
File.Delete(newFile);
StreamWriter file = File.CreateText(newFile);
file.Write(sb.ToString());
file.Flush();
file.Close();
*/