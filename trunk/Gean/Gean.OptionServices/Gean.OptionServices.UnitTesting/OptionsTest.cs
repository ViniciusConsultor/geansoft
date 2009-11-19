using Gean.OptionServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System;
namespace Gean.OptionServices.UnitTesting
{
    
    /// <summary>
    ///这是 OptionsTest 的测试类，旨在
    ///包含所有 OptionsTest 单元测试
    ///</summary>
    [TestClass()]
    public class OptionsTest
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

        public void CreatTestFile()
        {
            this.CreatTestStringDic();
            using (XmlTextWriter w = new XmlTextWriter(_configFile, Encoding.UTF8))
            {
                w.Formatting = Formatting.Indented;
                w.WriteStartDocument();
                w.WriteStartElement("configuration");
                    w.WriteAttributeString("created", DateTime.Now.ToString());
                    w.WriteAttributeString("applicationVersion", "1");
                    w.WriteAttributeString("modified", DateTime.Now.ToString());
                    foreach (var item in _stringDic)
                    {
                        w.WriteStartElement("section");
                        w.WriteAttributeString("name", item.Key);
                        w.WriteString(item.Value);
                        w.WriteEndElement();
                    }
                w.WriteEndElement();
                w.Flush();
            }
        }

        public void CreatTestStringDic()
        {
            for (int i = 1; i < 10; i++)
            {
                _stringDic.Add(i.ToString(), Guid.NewGuid().ToString());
            }
        }

        private string _configFile = "UnitTestting.gconfig";
        private Dictionary<string, string> _stringDic = new Dictionary<string, string>();

        /// <summary>
        ///GetOptionValue 的测试
        ///</summary>
        [TestMethod()]
        public void GetOptionValueTest()
        {
            this.CreatTestFile();
            Options.Initializes(_configFile);

            Options options = Options.Instance;
            foreach (var item in _stringDic)
            {
                Assert.AreEqual(item.Value, options[item.Key]);
            }
            string aaa;
            Assert.IsFalse(options.TryGetOptionValue("aaa", out aaa));
        }

        /// <summary>
        ///Save 的测试
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            this.CreatTestFile();
            Options.Initializes(_configFile);

            Options options = Options.Instance;
            options.SetOption("1", "1_value");
            options.SetOption("3", "3_value");
            options.SetOption("aaa", "aaa_value");
            options.Save();
        }
    }
}
