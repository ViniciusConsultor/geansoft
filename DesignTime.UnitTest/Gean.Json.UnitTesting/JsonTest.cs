using Gean.Json3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
namespace Gean.MyJson.UnitTesting
{
    [TestClass()]
    public class JsonTest
    {
        #region 附加测试属性
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
        ///DeserializeObject 的测试
        ///</summary>
        public void DeserializeObjectTestHelper<T>()
        {
            Json target = new Json(); // TODO: 初始化为适当的值
            string jsonInput = string.Empty; // TODO: 初始化为适当的值
            T expected = default(T); // TODO: 初始化为适当的值
            T actual;
            actual = target.DeserializeObject<T>(jsonInput);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///SerializeObject 的测试
        ///</summary>
        [TestMethod()]
        public void SerializeObjectTest()
        {
            Json target = new Json(); 

            string expected = "{\"Addresses\":[{\"City\":\"Paia\",\"Code\":123456789,\"Description\":\"香港立法会财务委员会。\",\"HiArray\":[\"壹\",\"贰\",\"叁\",\"肆\",\"伍\"],\"IsAddress\":true,\"MChar\":\"m\",\"Number\":[12,23,34,45,56,67,78,89],\"State\":\"HI\",\"Street\":\"32 Kaiea Place\",\"Zip\":\"96779\"},{\"City\":\"Paia\",\"Code\":123456789,\"Description\":\"香港立法会财务委员会。\",\"HiArray\":[\"壹\",\"贰\",\"叁\",\"肆\",\"伍\"],\"IsAddress\":true,\"MChar\":\"m\",\"Number\":[12,23,34,45,56,67,78,89],\"State\":\"HI\",\"Street\":\"32 Kaiea Place\",\"Zip\":\"96779\"}],\"Entered\":\"\\/Date(1263139200000+0800)\\/\",\"Name\":\"John Jones\"}"; 
            string actual;
            actual = target.SerializeObject(GetCustomer());
            Assert.AreEqual(expected, actual);
        }

        Customer GetCustomer()
        {
            Customer person = new Customer();
            person.Name = "John Jones";
            person.Entered = new DateTime(2010, 01, 11);

            person.Addresses.Add(new Address());
            person.Addresses.Add(new Address());

            return person;
        }
    }

    [Serializable]
    public class Customer
    {
        public List<Address> Addresses = new List<Address>();
        public DateTime Entered = DateTime.Now;
        public string Name = "John";
    }


    [Serializable]
    public class Address
    {
        public string City = "Paia";
        public string State = "HI";
        public string Street = "32 Kaiea Place";
        public string Zip = "96779";
        public string Description = "香港立法会财务委员会。";
        public int Code = 123456789;
        public bool IsAddress = true;
        public string[] HiArray = new string[] { "壹", "贰", "叁", "肆", "伍" };
        public Int64[] Number = new Int64[] { 12, 23, 34, 45, 56, 67, 78, 89 };
        public char MChar = 'm';
    }

}
