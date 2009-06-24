using Gean;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;
using System;
namespace Gean.FrameworkUnitTesting
{


    /// <summary>
    ///这是 DefinerTest 的测试类，旨在
    ///包含所有 DefinerTest 单元测试
    ///</summary>
    [TestClass()]
    public class DefinerTest
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

        private string _BasePath;
        private string _CaseFilePath;

        public DefinerTest()
        {
            _BasePath = Path.GetFullPath("..\\..\\..\\" + this.GetType().Assembly.FullName.Substring(0, this.GetType().Assembly.FullName.IndexOf(',')));
            _CaseFilePath = Path.Combine(_BasePath, "CaseFiles\\");
        }

        /// <summary>
        ///Item 的测试
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            Definer target = new Definer();

            target.Set<Font>("Font", new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))));
            target.Set<string>("Google", "谷歌正在颠覆确定新产品开发和优先次序的方式，为员工开辟与高层对话的渠道，此举是由于担心优秀人才和金点子会流向初创公司。");
            target.Set<DateTime>("Time", DateTime.Now);
            target.Set<Rectangle>("Rectangle", new Rectangle(new Point(108, 108), new Size(320, 320)));
            for (int i = 99; i < 150; i++)
            {
                target.Set<int>(i.ToString(), i * 99);
            }

            target.Save(Path.Combine(_CaseFilePath, "Writer.xml"));

            File.Copy(Path.Combine(_CaseFilePath, "Writer.xml"), Path.Combine(_CaseFilePath, "Read.xml"));
        }
    }
}
