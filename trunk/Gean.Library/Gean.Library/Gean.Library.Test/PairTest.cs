using Gean;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gean.Library.Test
{
    
    
    /// <summary>
    ///这是 PairTest 的测试类，旨在
    ///包含所有 PairTest 单元测试
    ///</summary>
    [TestClass()]
    public class PairTest
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


        /// <summary>
        ///Equals 的测试
        ///</summary>
        public void EqualsTestHelper<A, B>()
            where A : IEquatable<int>
            where B : IEquatable<string>
        {
            Pair<int, string> target = new Pair<int, string>(100, "test");
            Pair<int, string> other = new Pair<int, string>(100, "test");
            bool expected = true;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            this.EqualsTestHelper<int, string>();
        }

        /// <summary>
        ///op_Equality 的测试
        ///</summary>
        public void op_EqualityTestHelper<A, B>()
            where A : IEquatable<int>
            where B : IEquatable<string>
        {
            Pair<int, string> lhs = new Pair<int, string>(100, "test");
            Pair<int, string> rhs = new Pair<int, string>(100, "test");
            bool expected = true;
            bool actual;
            actual = (lhs == rhs);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void op_EqualityTest()
        {
            this.op_EqualityTestHelper<int, string>();
        }

        /// <summary>
        ///op_Inequality 的测试
        ///</summary>
        public void op_InequalityTestHelper<A, B>()
            where A : IEquatable<int>
            where B : IEquatable<string>
        {
            Pair<int, string> lhs = new Pair<int, string>(100, "test");
            Pair<int, string> rhs = new Pair<int, string>(100, "Test");
            bool expected = true; // TODO: 初始化为适当的值
            bool actual;
            actual = (lhs != rhs);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void op_InequalityTest()
        {
            this.op_InequalityTestHelper<int, string>();
        }
    }
}
