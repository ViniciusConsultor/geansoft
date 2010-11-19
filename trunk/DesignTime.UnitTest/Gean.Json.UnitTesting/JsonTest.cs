
namespace Gean.MyJson.UnitTesting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using Gean.Json3;

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

        string jsonstring = "{\"Addresses\":[{\"City\":\"Paia\",\"Code\":123456789,\"Description\":\"香港\"立法会\"财务委员会。\",\"HiArray\":[\"壹\",\"贰\",\"叁\",\"肆\",\"伍\"],\"IsAddress\":true,\"MChar\":\"m\",\"Number\":[12,23,34,45,56,67,78,89],\"State\":\"HI\",\"Street\":\"32 Kaiea Place\",\"Zip\":\"96779\"},{\"City\":\"Paia\",\"Code\":123456789,\"Description\":\"香港立法会财务委员会。\",\"HiArray\":[\"壹\",\"贰\",\"叁\",\"肆\",\"伍\"],\"IsAddress\":true,\"MChar\":\"m\",\"Number\":[12,23,34,45,56,67,78,89],\"State\":\"HI\",\"Street\":\"32 Kaiea Place\",\"Zip\":\"96779\"}],\"Entered\":\"\\/Date(1263139200000+0800)\\/\",\"Name\":\"John Jones\"}"; 

        /// <summary>
        ///DeserializeObject 的测试
        ///</summary>
        [TestMethod()]
        public void DeserializeObjectTest()
        {
            Json json = new Json();
            Customer actual = json.DeserializeObject<Customer>(jsonstring);
            Assert.AreEqual("John Jones", actual.Name);
            Assert.AreEqual(new DateTime(2010, 01, 11), actual.Entered);
            Assert.AreEqual(2, actual.Addresses.Count);
            Assert.AreEqual("Paia", actual.Addresses[0].City);
            Assert.AreEqual("HI", actual.Addresses[0].State);
            Assert.AreEqual("32 Kaiea Place", actual.Addresses[0].Street);
            Assert.AreEqual("96779", actual.Addresses[0].Zip);
            Assert.AreEqual("香港立法会财务委员会。", actual.Addresses[0].Description);
            Assert.AreEqual(123456789, actual.Addresses[1].Code);
            Assert.AreEqual(true, actual.Addresses[1].IsAddress);
            Assert.AreEqual('m', actual.Addresses[1].MChar);

            string[] HiArray = new string[] { "壹", "贰", "叁", "肆", "伍" };
            Assert.AreEqual(HiArray.Length, actual.Addresses[1].HiArray.Length);
            for (int i = 0; i < HiArray.Length; i++)
            {
                Assert.AreEqual(HiArray[i], actual.Addresses[1].HiArray[i]);
            }

            Int64[] Number = new Int64[] { 12, 23, 34, 45, 56, 67, 78, 89 };
            Assert.AreEqual(Number.Length, actual.Addresses[0].Number.Length);
            for (int i = 0; i < HiArray.Length; i++)
            {
                Assert.AreEqual(Number[i], actual.Addresses[0].Number[i]);
            }
        }

        /// <summary>
        ///SerializeObject 的测试
        ///</summary>
        [TestMethod()]
        public void SerializeObjectTest()
        {
            Json target = new Json(); 

            string actual;
            actual = target.SerializeObject(GetCustomer());
            Assert.AreEqual(jsonstring, actual);
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

    //[Serializable]
    public class Customer
    {
        public List<Address> Addresses = new List<Address>();
        public DateTime Entered = DateTime.Now;
        public string Name = "John";
    }


    //[Serializable]
    public class Address
    {
        public string City = "Paia";
        public string State = "HI";
        public string Street = "32 Kaiea Place";
        public string Zip = "96779";
        public string Description = "香港\"立法会\"财务委员会。";
        public int Code = 123456789;
        public bool IsAddress = true;
        public string[] HiArray = new string[] { "壹", "贰", "叁", "肆", "伍" };
        public Int64[] Number = new Int64[] { 12, 23, 34, 45, 56, 67, 78, 89 };
        public char MChar = '~';
    }

}

namespace Gean.Json.UnitTesting
{
    using Gean.Json2;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Gean.MyJson.UnitTesting;
    using System;

    /// <summary>
    ///这是 JsonTest 的测试类，旨在
    ///包含所有 JsonTest 单元测试
    ///</summary>
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

        string jsonstring = "{\"Addresses\":[{\"City\":\"Paia\",\"State\":\"HI\",\"Street\":\"32 Kaiea Place\",\"Zip\":\"96779\",\"Description\":\"香港立法会财务委员会。\",\"Code\":123456789,\"IsAddress\":true,\"HiArray\":[\"壹\",\"贰\",\"叁\",\"肆\",\"伍\"],\"Number\":[12,23,34,45,56,67,78,89],\"MChar\":\"m\"},{\"City\":\"Paia\",\"State\":\"HI\",\"Street\":\"32 Kaiea Place\",\"Zip\":\"96779\",\"Description\":\"香港立法会财务委员会。\",\"Code\":123456789,\"IsAddress\":true,\"HiArray\":[\"壹\",\"贰\",\"叁\",\"肆\",\"伍\"],\"Number\":[12,23,34,45,56,67,78,89],\"MChar\":\"m\"}],\"Entered\":\"\\/Date(1263139200000+0800)\\/\",\"Name\":\"John Jones\"}";

        /// <summary>
        ///DeserializeObject 的测试
        ///</summary>
        [TestMethod()]
        public void DeserializeObjectTest()
        {
            Json json = new Json();
            Customer actual = json.DeserializeObject<Customer>(jsonstring);
            Assert.AreEqual("John Jones", actual.Name);
            Assert.AreEqual(new DateTime(2010, 01, 11), actual.Entered);
            Assert.AreEqual(2, actual.Addresses.Count);
            Assert.AreEqual("Paia", actual.Addresses[0].City);
            Assert.AreEqual("HI", actual.Addresses[0].State);
            Assert.AreEqual("32 Kaiea Place", actual.Addresses[0].Street);
            Assert.AreEqual("96779", actual.Addresses[0].Zip);
            Assert.AreEqual("香港立法会财务委员会。", actual.Addresses[0].Description);
            Assert.AreEqual(123456789, actual.Addresses[1].Code);
            Assert.AreEqual(true, actual.Addresses[1].IsAddress);
            Assert.AreEqual('m', actual.Addresses[1].MChar);

            string[] HiArray = new string[] { "壹", "贰", "叁", "肆", "伍" };
            Assert.AreEqual(HiArray.Length, actual.Addresses[1].HiArray.Length);
            for (int i = 0; i < HiArray.Length; i++)
            {
                Assert.AreEqual(HiArray[i], actual.Addresses[1].HiArray[i]);
            }

            Int64[] Number = new Int64[] { 12, 23, 34, 45, 56, 67, 78, 89 };
            Assert.AreEqual(Number.Length, actual.Addresses[0].Number.Length);
            for (int i = 0; i < HiArray.Length; i++)
            {
                Assert.AreEqual(Number[i], actual.Addresses[0].Number[i]);
            }
        }

        /// <summary>
        ///SerializeObject 的测试
        ///</summary>
        [TestMethod()]
        public void SerializeObjectTest()
        {
            Json target = new Json();

            string actual;
            actual = target.SerializeObject(GetCustomer());
            Assert.AreEqual(jsonstring, actual);
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
}
