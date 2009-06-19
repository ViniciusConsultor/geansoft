using System.IO;
using System.Xml;
using System.Collections.Generic;
using System;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个启动PlugTree的服务组件
    /// </summary>
    /// <remarks>
    /// 1. Create a new CoreStartup instance
    /// 2. (Optional) Set the values of the properties.
    /// 3. Call <see cref="StartCoreServices()"/>.
    /// 4. Add "preinstalled" AddIns using <see cref="AddAddInsFromDirectory"/>
    ///    and <see cref="AddAddInFile"/>.
    /// 5. (Optional) Call <see cref="ConfigureExternalAddIns"/> to support
    ///    disabling AddIns and installing external AddIns
    /// 6. (Optional) Call <see cref="ConfigureUserAddIns"/> to support installing
    ///    user AddIns.
    /// 7. Call <see cref="RunInitialization"/>.
    /// </remarks>
    public sealed class StartupService
    {
        readonly string _PLUG_FILE_EXPAND_NAME = "*.gplug";

        List<string> _PlugFiles = new List<string>();
        List<string> _DisabledPlugs = new List<string>();

        string _ApplicationName;

        /// <summary>
        /// 一般在"c:\documents and settings\username\application data"
        /// </summary>
        public string ConfigDirectory { get; internal set; }

        /// <summary>
        /// 使用"ApplicationRootPath\data"，存储应用的一些资源，如图片等.
        /// </summary>
        public string DataDirectory { get; internal set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="applicationName">这是应用程序的名字</param>
        public StartupService(string applicationName)
        {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException("applicationName");
            this._ApplicationName = applicationName;
            //MessageService.DefaultMessageBoxTitle = applicationName;
            //MessageService.ProductName = applicationName;
        }

        /// <summary>
        /// 从目录中找到所有的Plug配置文件
        /// </summary>
        public void AddPlugsFromDirectory(string plugDir)
        {
            if (string.IsNullOrEmpty(plugDir))
                throw new ArgumentNullException("plugDir");
            _PlugFiles.AddRange(PlugTree.SearchDirectory(plugDir, this._PLUG_FILE_EXPAND_NAME, true, true));
        }

        /// <summary>
        /// 向Plug文件集合中增加一个新的Plug文件
        /// </summary>
        public void AddPlugFile(string plugFile)
        {
            if (string.IsNullOrEmpty(plugFile))
                throw new ArgumentNullException("plugFile");
            _PlugFiles.Add(plugFile);
        }

        /// <summary>
        /// Initializes the AddIn system.
        /// This loads the AddIns that were added to the list,
        /// then it executes the <see cref="ICommand">commands</see>
        /// in <c>/Workspace/Autostart</c>.
        /// </summary>
        public void Initialization()
        {
            PlugTree.Load(_PlugFiles, _DisabledPlugs);

            // run workspace autostart commands
            // LoggingService.Info("Running autostart commands...");
            //foreach (ICommand command in AddInTree.BuildItems<ICommand>("/Workspace/Autostart", null, false))
            //{
            //    try
            //    {
            //        command.Run();
            //    }
            //    catch (Exception ex)
            //    {
            //        // allow startup to continue if some commands fail
            //        MessageService.ShowError(ex);
            //    }
            //}
        }

        ///// <summary>
        ///// Starts the core services.
        ///// This initializes the PropertyService and ResourceService.
        ///// </summary>
        //public void StartCoreServices()
        //{
        //    if (_ConfigDirectory == null)
        //        _ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        //                                       _ApplicationName);
        //    PropertyService.InitializeService(_ConfigDirectory,
        //                                      _DataDirectory ?? Path.Combine(FileUtility.ApplicationRootPath, "data"),
        //                                      propertiesName);
        //    PropertyService.Load();
        //    ResourceService.InitializeService(FileUtility.Combine(PropertyService.DataDirectory, "resources"));
        //    StringParser.Properties["AppName"] = _ApplicationName;
        //}

    }
}
