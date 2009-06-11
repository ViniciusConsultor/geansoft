using Gean.Src.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace Gean.Library.Test
{
    [TestClass()]
    public class UtilityKeysTest
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
        ///ParseByShortcutChar 的测试
        ///</summary>
        [TestMethod()]
        public void ParseByShortcutCharTest()
        {
            string shortcutChar;
            Keys expected;
            Keys actual;

            shortcutChar = "ctrl+shift+a";
            expected = Keys.Control | Keys.Shift | Keys.A;
            actual = UtilityKeys.ParseByShortcutChar(shortcutChar);
            Assert.AreEqual(expected, actual);

            shortcutChar = "shift+ctrl+R";
            expected = Keys.Control | Keys.Shift | Keys.R;
            actual = UtilityKeys.ParseByShortcutChar(shortcutChar);
            Assert.AreEqual(expected, actual);

            shortcutChar = "alt+ctrl+f10";
            expected = Keys.Control | Keys.Alt | Keys.F10;
            actual = UtilityKeys.ParseByShortcutChar(shortcutChar);
            Assert.AreEqual(expected, actual);

            shortcutChar = "alt+cTrl+shIFt+f12";
            expected = Keys.Control | Keys.Alt | Keys.Shift | Keys.F12;
            actual = UtilityKeys.ParseByShortcutChar(shortcutChar);
            Assert.AreEqual(expected, actual);

            shortcutChar = "alt+cTrl+shIFt+eSc";
            expected = Keys.Control | Keys.Alt | Keys.Shift | Keys.Escape;
            actual = UtilityKeys.ParseByShortcutChar(shortcutChar);
            Assert.AreEqual(expected, actual);

            shortcutChar = "alt+cTrl+shIFt+dEl";
            expected = Keys.Control | Keys.Alt | Keys.Shift | Keys.Delete;
            actual = UtilityKeys.ParseByShortcutChar(shortcutChar);
            Assert.AreEqual(expected, actual);
        }
    }
}
