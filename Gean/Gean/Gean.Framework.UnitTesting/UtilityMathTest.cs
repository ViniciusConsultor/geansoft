using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Gean.Math;

namespace Gean.Framework.UnitTesting
{
    
    
    /// <summary>
    ///这是 PermutationsTest 的测试类，旨在
    ///包含所有 PermutationsTest 单元测试
    ///</summary>
    [TestClass()]
    public class UtilityMathTest
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


        ///<summary>
        ///"组合函数"的测试
        ///</summary>
        [TestMethod()]
        public void CTest()
        {
            int n = 64;
            int r = 64;
            BigInteger expected = 1;
            BigInteger actual;
            actual = UtilityMath.C(n, r);
            Assert.AreEqual(expected, actual);

            r = 2;
            expected = 2016;
            actual = UtilityMath.C(n, r);
            Assert.AreEqual(expected, actual);

            r = 4;
            expected = 635376;
            actual = UtilityMath.C(n, r);
            Assert.AreEqual(expected, actual);

            r = 8;
            expected = 4426165368;
            actual = UtilityMath.C(n, r);
            Assert.AreEqual(expected, actual);

            n = 512;
            r = 32;
            string expectedstring = "702814253655938225018039814641109516123603866169840";
            string actualstring = UtilityMath.C(n, r).ToString();
            Assert.AreEqual(expectedstring, actualstring);
        }

        ///<summary>
        ///"排列函数"的测试
        ///</summary>
        [TestMethod()]
        public void PTest()
        {
            int n = 64;
            int r = 1;
            BigInteger expected = 64;
            BigInteger actual;
            actual = UtilityMath.P(n, r);
            Assert.AreEqual(expected, actual);

            n = 64;
            r = 4;
            expected = 15249024;
            actual = UtilityMath.P(n, r);
            Assert.AreEqual(expected, actual);

            n = 64;
            r = 8;
            expected = 178462987637760;
            actual = UtilityMath.P(n, r);
            Assert.AreEqual(expected, actual);
        }

    }
}
