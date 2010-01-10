using Gean.SimpleLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gean.Library.UnitTesting
{
    /// <summary>
    ///这是 LogWriterTest 的测试类，旨在
    ///包含所有 LogWriterTest 单元测试
    ///</summary>
    [TestClass()]
    public class LogWriterTest
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

        private SimpleLoggerWriter _logWriter = null;

        public LogWriterTest()
        {
            _logWriter = SimpleLoggerWriter.InitializeComponent(@"d:\xiaofanlog.txt");
        }

        /// <summary>
        ///Write 的测试
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            _logWriter.Write(SimpleLoggerLevel.Info, "LogWriter Begin...");
            _logWriter.Write(SimpleLoggerLevel.Warn, UtilityGuid.Get());
            _logWriter.Write(SimpleLoggerLevel.Debug, "_logWriter.Write(LogLevel.Debug, ***)");
            _logWriter.Write(SimpleLoggerLevel.Error, "_logWriter.Write(LogLevel.Error, ***)");
            _logWriter.Write(SimpleLoggerLevel.Error, "_logWriter.Write(LogLevel.Error, ***)");
            _logWriter.Write(SimpleLoggerLevel.Info, UtilityGuid.Get());
            _logWriter.Write(SimpleLoggerLevel.Debug, UtilityGuid.Get());
            _logWriter.Write(SimpleLoggerLevel.Error, UtilityGuid.Get());
            _logWriter.Write(SimpleLoggerLevel.Info, UtilityGuid.Get());
            _logWriter.Write(SimpleLoggerLevel.Warn, UtilityGuid.Get());
            _logWriter.Write(SimpleLoggerLevel.Warn, UtilityGuid.Get());
            _logWriter.Write(SimpleLoggerLevel.Info, "LogWriter End...");

            _logWriter.BakupLogFile();
            _logWriter.Close(false);
        }
    }
}
