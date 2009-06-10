using Gean.Wrapper.PlugTree.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Gean.Wrapper.PlugTree;

namespace Gean.Wrapper.PlugTreeTest
{
    
    
    /// <summary>
    ///这是 ConditionTest 的测试类，旨在
    ///包含所有 ConditionTest 单元测试
    ///</summary>
    [TestClass()]
    public class ConditionTest
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

        #region 数据

        #region _ComplexCondition
        
        private string _ComplexCondition = @"
<Plug>
        <ComplexCondition>
	        <Not>
		        <Condition name = ""Ownerstate"" ownerstate = ""Missing""/>
	        </Not>
	        <MenuItem id = ""Add"" label = ""${res:ProjectComponent.ContextMenu.AddMenu}"" type=""Menu"">
		        <MenuItem id    = ""New Item""
		                  label = ""${res:ProjectComponent.ContextMenu.NewItem}""
		                  icon  = ""Icons.16x16.NewDocumentIcon""
		                  class = ""ICSharpCode.SharpDevelop.Project.Commands.AddNewItemsToProject""/>
	        </MenuItem>
	        <MenuItem id = ""AddSeparator"" type = ""Separator""  />
	        <ComplexCondition>
		        <Or>
			        <Condition name = ""Ownerstate"" ownerstate = ""InProject""/>
			        <Condition name = ""Ownerstate"" ownerstate = ""None""/>
		        </Or>
		        <Condition name = ""Ownerstate"" ownerstate = ""InProject"">
			        <MenuItem id    = ""ExcludeFile""
			                  label = ""${res:ProjectComponent.ContextMenu.ExcludeFileFromProject}""
			                  class = ""ICSharpCode.SharpDevelop.Project.Commands.ExcludeFileFromProject""/>
		        </Condition>
		        <Condition name = ""Ownerstate"" ownerstate = ""None"">
			        <MenuItem id    = ""IncludeFile""
			                  label = ""${res:ProjectComponent.ContextMenu.IncludeFileInProject}""
			                  class = ""ICSharpCode.SharpDevelop.Project.Commands.IncludeFileInProject""/>
		        </Condition>
		        <MenuItem id = ""ExcludeSeparator"" type = ""Separator"" />
	        </ComplexCondition>
	        <Include id=""CutCopyPasteDeleteRename"" path=""/SharpDevelop/Pads/ProjectBrowser/ContextMenu/CutCopyPasteDeleteRename""/>
        </ComplexCondition>
</Plug>
";
        #endregion

        #region _Condition
        
        private string _Condition = @"
<Plug>
	<Condition name = ""Ownerstate"" ownerstate = ""Missing"">
		<MenuItem id = ""CreateMissing""
		          label = ""${res:ProjectComponent.ContextMenu.NewFolder}""
		          type  = ""Item""
		          icon  = ""Icons.16x16.NewFolderIcon""
		          class = ""ICSharpCode.SharpDevelop.Project.Commands.CreateMissingCommand""/>
	</Condition>
</Plug>
";
        #endregion

        #endregion

        /// <summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void Condition_Parse_Test()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this._Condition);
            XmlElement element = (XmlElement)doc.DocumentElement.ChildNodes[0];

            ICondition actual;
            actual = Condition.Parse(element);
            Assert.AreEqual("Ownerstate", actual.Name);
            Assert.AreEqual(ConditionFailedAction.Exclude, actual.Action);
        }

        /// <summary>
        ///Parse 的测试
        ///</summary>
        [TestMethod()]
        public void Condition_ParseComplex_Test()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this._ComplexCondition);
            XmlElement element = (XmlElement)doc.DocumentElement.ChildNodes[0];

            ICondition actual;
            actual = Condition.ParseComplex(element);
            Assert.AreNotEqual(null, actual);
        }
    }
}
