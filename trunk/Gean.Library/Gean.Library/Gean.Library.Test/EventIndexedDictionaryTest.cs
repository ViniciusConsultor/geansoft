using Gean;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace Gean.Library.Test
{
   
    /// <summary>
    ///这是 EventIndexedDictionaryTest 的测试类，旨在
    ///包含所有 EventIndexedDictionaryTest 单元测试
    ///</summary>
    [TestClass()]
    public class EventIndexedDictionaryTest
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

        #endregion

        private static EventIndexedDictionary<int, int> _EIDic;
        private static string _TestValueAfter;
        private static string _TestValue = "abcd";

        //使用 TestInitialize 在运行每个测试前先运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _EIDic = new EventIndexedDictionary<int, int>();
        }
        
        //使用 TestCleanup 在运行完每个测试后运行代码
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _TestValueAfter = string.Empty;
            _EIDic = null;
        }

        [TestMethod()]
        public void AddTest()
        {
            _EIDic.AfterAdd += new EventIndexedDictionaryAfterDelegate<int, int>(_EIDic_AfterAdd);
            _EIDic.Add(1, 1);
            Assert.AreEqual(_TestValue, _TestValueAfter);
        }

        void _EIDic_AfterAdd(object sender, EventIndexedDictionaryEventArgs<int, int> e)
        {
            _TestValueAfter = _TestValue;
        }

        [TestMethod()]
        public void GetValueByIndexTest()
        {
            EventIndexedDictionary<TestClassHelper, TestClassHelper> target =
                new EventIndexedDictionary<TestClassHelper, TestClassHelper>();
            for (int i = 0; i < 20; i++)
            {
                TestClassHelper tch = new TestClassHelper(i, i + 0.12345, i.ToString());
                target.Add(tch, tch);
            }

            int index;
            TestClassHelper value;
            TestClassHelper actual;

            index = 15;
            value = new TestClassHelper(index, index + 0.12345, index.ToString());
            actual = target[index];
            Assert.AreEqual(value, actual);

            index = 0;
            value = new TestClassHelper(index, index + 0.12345, index.ToString());
            actual = target[index];
            Assert.AreEqual(value, actual);

            index = 19;
            value = new TestClassHelper(index, index + 0.12345, index.ToString());
            actual = target[index];
            Assert.AreEqual(value, actual);
        }

        class TestClassHelper
        {
            public TestClassHelper(int i, double d, string str)
            {
                this.IntInt = i;
                this.DoubleDouble = d;
                this.StringString = str;
            }
            public int IntInt { get; private set; }
            public double DoubleDouble { get; private set; }
            public string StringString { get; private set; }
            public override bool Equals(object obj)
            {
                TestClassHelper tch = (TestClassHelper)obj;
                if (tch.IntInt != this.IntInt)
                {
                    return false;
                }
                if (tch.DoubleDouble != this.DoubleDouble)
                {
                    return false;
                }
                if (tch.StringString != this.StringString)
                {
                    return false;
                }
                return true;
            }
            public override int GetHashCode()
            {
                return this.StringString.GetHashCode() + this.IntInt.GetHashCode() + this.DoubleDouble.GetHashCode();
            }
        }
    }
}
