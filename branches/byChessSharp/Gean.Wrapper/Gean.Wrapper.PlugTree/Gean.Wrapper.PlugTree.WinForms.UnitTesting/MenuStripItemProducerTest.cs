using Gean.Wrapper.PlugTree.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gean.Wrapper.PlugTree;

namespace Gean.Wrapper.PlugTree.WinForms.UnitTesting
{
    
    
    /// <summary>
    ///这是 MenuStripItemProducerTest 的测试类，旨在
    ///包含所有 MenuStripItemProducerTest 单元测试
    ///</summary>
    [TestClass()]
    public class MenuStripItemProducerTest
    {


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


        /// <summary>
        ///CreateObject 的测试
        ///</summary>
        [TestMethod()]
        public void CreateObjectTest()
        {
            MenuStripItemProducer target = new MenuStripItemProducer(); // TODO: 初始化为适当的值
            Plug plug = null; // TODO: 初始化为适当的值
            object caller = null; // TODO: 初始化为适当的值
            object expected = null; // TODO: 初始化为适当的值

            object actual;
            actual = target.CreateObject(plug, caller);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
