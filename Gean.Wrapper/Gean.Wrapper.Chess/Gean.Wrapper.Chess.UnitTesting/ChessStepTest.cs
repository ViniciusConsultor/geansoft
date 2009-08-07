using Gean.Wrapper.Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
namespace Gean.Wrapper.Chess.UnitTesting
{
    
    
    /// <summary>
    ///这是 ChessStepTest 的测试类，旨在
    ///包含所有 ChessStepTest 单元测试
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
            expected = new ChessStep(Enums.ChessmanType.None, ChessPosition.Empty, ChessPosition.Empty, Enums.Action.KingSideCastling);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "O-O-O";
            expected = new ChessStep(Enums.ChessmanType.None, ChessPosition.Empty, ChessPosition.Empty, Enums.Action.QueenSideCastling);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "O - O - O";
            expected = new ChessStep(Enums.ChessmanType.None, ChessPosition.Empty, ChessPosition.Empty, Enums.Action.QueenSideCastling);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "b5+";
            expected = new ChessStep(Enums.ChessmanType.Pawn, ChessPosition.Empty, new ChessPosition(2, 5), Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Qxh3+";
            expected = new ChessStep(Enums.ChessmanType.Queen, ChessPosition.Empty, new ChessPosition(8, 3), Enums.Action.Kill,Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Rxb4+";
            expected = new ChessStep(Enums.ChessmanType.Rook, ChessPosition.Empty, new ChessPosition(2, 4), Enums.Action.Kill, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "axb5+";
            expected = new ChessStep(Enums.ChessmanType.Pawn, new ChessPosition(1, 4), new ChessPosition(2, 5), Enums.Action.Kill, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "axb5+";
            expected = new ChessStep(Enums.ChessmanType.Pawn, new ChessPosition(1, 6), new ChessPosition(2, 5), Enums.Action.Kill, Enums.Action.Check);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.Black);
            Assert.AreEqual(expected, actual);

            value = "Qh3";
            expected = new ChessStep(Enums.ChessmanType.Queen, ChessPosition.Empty, new ChessPosition(8, 3), Enums.Action.General);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "a3";
            expected = new ChessStep(Enums.ChessmanType.Pawn, ChessPosition.Empty, new ChessPosition(1, 3), Enums.Action.General);
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "e8=Q";
            expected = new ChessStep(Enums.ChessmanType.Pawn, ChessPosition.Empty, new ChessPosition('e', 8), Enums.Action.Promotion);
            expected.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "fxe8=Q+";
            expected = new ChessStep(Enums.ChessmanType.Pawn, new ChessPosition('f', 7), new ChessPosition('e', 8), Enums.Action.Promotion, Enums.Action.Check, Enums.Action.Kill);
            expected.PromotionChessmanType = Enums.ChessmanType.Queen;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            //cxd1=Q
            //cxd1=Q+
            //gxf1=Q+
            //bxc1=Q
            //exf1=Q+

            value = "cxd8=N+";//bxc8=Q+
            expected = new ChessStep(Enums.ChessmanType.Pawn, new ChessPosition('c', 7), new ChessPosition('d', 8), Enums.Action.Kill, Enums.Action.Check, Enums.Action.Promotion);
            expected.PromotionChessmanType = Enums.ChessmanType.Knight;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Rfb8";
            expected = new ChessStep(Enums.ChessmanType.Rook, ChessPosition.Empty, new ChessPosition('b', 8), Enums.Action.General);
            expected.HasSame = true;
            actual = ChessStep.Parse(value, Enums.ChessmanSide.White);
            Assert.AreEqual(expected, actual);

            value = "Rfxe1+";
            expected = new ChessStep(Enums.ChessmanType.Rook, ChessPosition.Empty, new ChessPosition('e', 1), Enums.Action.Kill, Enums.Action.Check);
                //new ChessPosition('f', 1), new ChessPosition('e', 1));
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
            string filename = Path.Combine(this.DemoFilePath, "ChessStep.txt");
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

        /*
        /// <summary>
        ///ToString 的测试
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
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
        }
        */
    }
}
