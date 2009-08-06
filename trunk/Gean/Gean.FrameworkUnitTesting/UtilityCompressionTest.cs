using Gean.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
namespace Gean.FrameworkUnitTesting
{
    
    
    /// <summary>
    ///这是 UtilityCompressionTest 的测试类，旨在
    ///包含所有 UtilityCompressionTest 单元测试
    ///</summary>
    [TestClass()]
    public class UtilityCompressionTest
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

        public UtilityCompressionTest()
        {
            _BasePath = Path.GetFullPath("..\\..\\..\\" + this.GetType().Assembly.FullName.Substring(0, this.GetType().Assembly.FullName.IndexOf(',')));
            _CaseFilePath = Path.Combine(_BasePath, "CaseFiles\\");
        }


        /// <summary>
        ///UnRar 的测试
        ///</summary>
        [TestMethod()]
        public void UnRarTest()
        {
            string rar = @"C:\Program Files\WinRAR\UnRAR.exe";
            UtilityCompression.Initialization(rar);

            string compressionFile = Path.Combine(_CaseFilePath, "rarFiletest.rar");
            string expected = "解压完成，共解压出：0个目录，6个文件";

            string actual = UtilityCompression.UnRar(compressionFile);
            Assert.AreEqual(expected, actual);
        }
    }
}
