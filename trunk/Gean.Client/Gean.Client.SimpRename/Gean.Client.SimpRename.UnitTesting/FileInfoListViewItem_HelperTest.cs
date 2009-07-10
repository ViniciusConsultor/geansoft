using Gean.Client.SimpRename;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Client.SimpRename.UnitTesting
{
    
    
    /// <summary>
    ///这是 FileInfoListViewItem_HelperTest 的测试类，旨在
    ///包含所有 FileInfoListViewItem_HelperTest 单元测试
    ///</summary>
    [TestClass()]
    public class FileInfoListViewItem_HelperTest
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

        /// <summary>
        ///BuildNewFileName 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Gean.Client.SimpRename.exe")]
        public void BuildNewFileNameTest()
        {
            string rule;
            int serial;
            string extensionName;
            string expected;//目标
            string actual;//实际

            rule = "abc__##__xyz";
            serial = 0;
            extensionName = ".doc";
            expected = "abc__00__xyz.doc";
            actual = FileInfoListViewItem_Accessor.Helper.BuildNewFileName(rule, serial, extensionName);
            Assert.AreEqual(expected, actual);

            rule = "abc__###__xyz";
            serial = 0;
            extensionName = ".doc";
            expected = "abc__000__xyz.doc";
            actual = FileInfoListViewItem_Accessor.Helper.BuildNewFileName(rule, serial, extensionName);
            Assert.AreEqual(expected, actual);

            rule = "abc__#__xyz";
            serial = 0;
            extensionName = ".doc";
            expected = "abc__0__xyz.doc";
            actual = FileInfoListViewItem_Accessor.Helper.BuildNewFileName(rule, serial, extensionName);
            Assert.AreEqual(expected, actual);
        }

        ///// <summary>
        /////IntToString 的测试
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("Gean.Client.SimpRename.exe")]
        //public void IntToStringTest()
        //{
        //    int digit = 0; // TODO: 初始化为适当的值
        //    int number = 0; // TODO: 初始化为适当的值
        //    string expected = string.Empty; // TODO: 初始化为适当的值
        //    string actual;
        //    actual = FileInfoListViewItem_Accessor.Helper.IntToString(digit, number);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("验证此测试方法的正确性。");
        //}

        ///// <summary>
        /////GetDigit 的测试
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("Gean.Client.SimpRename.exe")]
        //public void GetDigitTest()
        //{
        //    long value = 0; // TODO: 初始化为适当的值
        //    int expected = 0; // TODO: 初始化为适当的值
        //    int actual;
        //    actual = FileInfoListViewItem_Accessor.Helper.GetDigit(value);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("验证此测试方法的正确性。");
        //}

    }
}
