using Gean.Wrapper.PlugTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;

namespace Gean.Wrapper.PlugTree.UnitTesting
{
    
    /// <summary>
    ///这是 PlugTreeTest 的测试类，旨在
    ///包含所有 PlugTreeTest 单元测试
    ///</summary>
    [TestClass()]
    public class PlugTreeTest
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

        public PlugTreeTest()
        {
            _BasePath = Path.GetFullPath("..\\..\\..\\" + this.GetType().Assembly.FullName.Substring(0, this.GetType().Assembly.FullName.IndexOf(',')));
            _CaseFilePath = Path.Combine(_BasePath, "CaseFiles\\");
        }

        /// <summary>
        ///Initialization 的测试
        ///</summary>
        [TestMethod()]
        public void InitializationTest()
        {
            PlugTree pt = PlugTree.Initialization(this._CaseFilePath);
            Assert.AreEqual(8, pt.Producers.Count);
            Assert.AreEqual(5, pt.Conditions.Count);

            PlugPath pp0;
            pt.DocumentPath.PlugPathItems.TryGetValue("Gean", out pp0);
            PlugPath pp1;
            pp0.PlugPathItems.TryGetValue("MainMenu", out pp1);

            Assert.IsTrue(pp1.PlugItems.Count > 1);

            PlugPath pp2 = pt.DocumentPath.SelectSingerPath("/Gean/MainMenu");
            Assert.IsNotNull(pp2);
            PlugPath pp3 = pt.DocumentPath.SelectSingerPath("/SharpDevelop/Views/ProjectBrowser/ContextSpecificProperties");
            Assert.IsNotNull(pp3);
        }
    }
}
