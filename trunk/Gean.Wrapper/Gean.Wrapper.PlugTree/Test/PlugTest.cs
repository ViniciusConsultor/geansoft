using Gean.Wrapper.PlugTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.IO;
using System.Text;
using System;
using System.Collections;
namespace Gean.Wrapper.PlugTreeTest
{
    
    
    /// <summary>
    ///这是 PlugTest 的测试类，旨在
    ///包含所有 PlugTest 单元测试
    ///</summary>
    [TestClass()]
    public class PlugTest
    {

        #region system

        /// <summary>
        ///获取或设置测试上下文，上下文提供有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext { get; set; }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        /// <summary>
        /// 使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string file = "Aaa.Bbb.Ccc.Ddd.Eee.dll";
            File.Copy(@"..\..\..\Test\" + file, file, true);
            UnitTestClass.PlugManager_PlugFileCollection_Init();
        }
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

        #region 数据

        private string _TestSimpleString = @"
<Plug name =""08-12-8 21:55:15-----PlugTest"">
    <Manifest HintPath = """">
        <Identity name = ""Identity_11"" version = ""@WorkbenchCore""/>
        <Identity name = ""Identity_22"" version = ""1.0.2.1234""/>
        <Dependency name = ""Dependency_00"" version = ""1.001.013.1234""/>
        <Conflict name = ""Conflict_00"" version = ""88.99-66.77-11.44-44.44"" RequirePreload=""true""/>
        <DisablePlug message = ""MMM+MMM+MMM!"" />
    </Manifest>
	<Runtime>
        <Import assembly= ""Aaa.Bbb.Ccc.Ddd.Eee.dll"">
			<ConditionEvaluator name = ""IsBreakpointSet"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestClass_111""/>
			<ConditionEvaluator name = ""IsBreakpointActive"" class= ""Aaa.Bbb.Ccc.Ddd.Eee.TestClass_111"" />
			<Builder name = ""TestBuilder_111"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestBuilder_111""/>
			<Builder name = ""TestBuilder_222"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestBuilder_222""/>
        </Import>
		<Import assembly=""Aaa.Bbb.Ccc.Ddd.Eee.dll""/>
		<Import assembly=""Aaa.Bbb.Ccc.Ddd.Eee.dll""/>
	</Runtime>

	<Path name = ""/aa/aaaa/file"">
		<TestBuilder_111 id = ""TestCommand_111"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_111""/>
	</Path>
	<Path name = ""/aa/aaaa/file"">
		<TestBuilder_111 id = ""TestCommand_222"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_222""/>
		<TestBuilder_111 id = ""TestCommand_333"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_333""/>
	</Path>
	<Path name = ""/aa/aaaa/file"">
		<TestBuilder_111 id = ""TestCommand_444"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_444""/>
	</Path>
	<Path name = ""/bb/edit"">
		<TestBuilder_222 id = ""TestCommand_555"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_555""/>
		<TestBuilder_222 id = ""TestCommand_666"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_666""/>
	</Path>
    <Path name = ""/bb/edit"">
		<TestBuilder_222 id = ""TestCommand_777"" class = ""Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_777""/>
	</Path>

</Plug>
";

        #endregion


        /// <summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void Plug_Parse_Test()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this._TestSimpleString);
            XmlElement element = (XmlElement)doc.DocumentElement.ChildNodes[0];

            string filename = "PlugTest.xml";
            File.WriteAllText(filename, this._TestSimpleString, Encoding.UTF8);

            Plug plug = null;
            //Plug.Parse(Path.GetFullPath(filename), out plug);

            Assert.AreEqual(2, plug.PlugManifest.Identities.Count);
            Assert.AreEqual(1, plug.PlugManifest.Dependencies.Count);
            Assert.AreEqual(1, plug.PlugManifest.Conflicts.Count);
            Assert.AreEqual(new Version("11.44"), plug.PlugManifest.Conflicts[0].MinimumVersion);
            Assert.AreEqual(new Version("88.99"), plug.PlugManifest.Conflicts[0].MaximumVersion);
            Assert.AreEqual("MMM+MMM+MMM!", plug.CustomErrorMessage);
            Assert.AreEqual(3, plug.PlugRuntimes.Count);
            Assert.AreEqual(2, plug.PlugRuntimes[0].DefinedBuilders.Count);
            Assert.AreEqual(2, plug.PlugRuntimes[0].DefinedConditionEvaluators.Count);

            IBuilder builder1 = plug.PlugRuntimes[0].DefinedBuilders[0];
            Assert.AreEqual(false, builder1.HandleConditions);
            IBuilder builder2 = plug.PlugRuntimes[0].DefinedBuilders[1];
            Assert.AreEqual(true, builder2.HandleConditions);

            Assert.AreEqual("ApplicationName".ToLowerInvariant(), PlugManager.PlugPath.Name);
            Assert.AreEqual(2, PlugManager.PlugPath.Items.Count);
            Assert.AreEqual(0, PlugManager.PlugPath.Items[0].Blocks.Count);
            Assert.AreEqual(0, PlugManager.PlugPath.Items[0].Items[0].Blocks.Count);
            Assert.AreEqual("/applicationname/aa/aaaa/file", PlugManager.PlugPath.Items[0].Items[0].Items[0].ToString());
            Assert.AreEqual(4, PlugManager.PlugPath.Items[0].Items[0].Items[0].Blocks.Count);
            Assert.AreEqual("/applicationname/bb/edit", PlugManager.PlugPath.Items[1].Items[0].ToString());
            Assert.AreEqual(3, PlugManager.PlugPath.Items[1].Items[0].Blocks.Count);

            Assert.AreEqual("TestCommand_777", PlugManager.PlugPath.Items[1].Items[0].Blocks[2].Id);
            Assert.AreEqual("TestBuilder_222", PlugManager.PlugPath.Items[1].Items[0].Blocks[2].Name);
            Assert.AreEqual("Aaa.Bbb.Ccc.Ddd.Eee.TestCommand_777", 
                (string)PlugManager.PlugPath.Items[1].Items[0].Blocks[2].Properties["class"]);

            object obj = PlugManager.PlugPath.Items[1].Items[0].Blocks[2].BuildItem(null, new ArrayList());
            Assert.AreEqual("TestBuilder_222_String", (string)obj);
        }
    }
}
