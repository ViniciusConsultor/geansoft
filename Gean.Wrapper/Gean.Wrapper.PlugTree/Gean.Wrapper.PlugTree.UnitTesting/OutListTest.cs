using Gean.Wrapper.PlugTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gean.Wrapper.PlugTree.UnitTesting
{
    
    
    /// <summary>
    ///这是 OutListTest 的测试类，旨在
    ///包含所有 OutListTest 单元测试
    ///</summary>
    [TestClass()]
    public class OutListTest
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

        class OutListTestClass_1 : ReadOnlyList<StringPair>
        {
            public OutListTestClass_1()
            {
                for (int i = 1; i < 10; i++)
                {
                    this.Add(new StringPair("Name" + i.ToString(), (i * 1234).ToString()));
                }
            }
        }

        class OutListTestClass_2 : ReadOnlyList<int>
        {
            public OutListTestClass_2()
            {
                for (int i = 1; i < 10; i++)
                {
                    this.Add(i);
                }
            }
        }

        class StringPair
        {
            public StringPair(string name, string value)
            {
                this.Name = name;
                this.OutValue = value;
            }
            public string Name { get; set; }
            public string OutValue { get; set; }
        }

        [TestMethod()]
        public void TryGetValueTest()
        {
            OutListTestClass_1 class1 = new OutListTestClass_1();
            
            StringPair expected;
            StringPair actual;
            int i;

            i = 3;
            expected = new StringPair("Name" + i.ToString(), (i * 1234).ToString());
            Assert.IsFalse(!class1.TryGetValue("Name" + i.ToString(), out actual));
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.OutValue, actual.OutValue);
            Assert.IsFalse(class1.TryGetValue("abcd", out actual));

            OutListTestClass_2 class2 = new OutListTestClass_2();

            int value;
            //class2中的值是int，所以肯定没有Name属性，返回false
            Assert.IsFalse(class2.TryGetValue("abcd", out value));
        }
    }
}
