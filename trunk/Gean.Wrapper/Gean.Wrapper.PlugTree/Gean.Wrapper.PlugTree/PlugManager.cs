using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;

using Gean.Wrapper.PlugTree.Service;

namespace Gean.Wrapper.PlugTree
{
    public static class PlugManager
    {
        #region Properties
        
        public static string ApplicationName { get; private set; }
        public static string ApplicationStartupPath { get; private set; }

        internal static string PlugStartupPath { get; private set; }
        internal static string PlugCustomPath { get; private set; }
        internal static bool IsAllowUseCustomPath { get; private set; }
        internal static StringCollection PlugFileCollection { get; private set; }

        public static PlugPath PlugPath { get; internal set; }
        public static PlugCollection PlugCollection { get; private set; }
        public static BuilderCollection BuilderCollection { get; private set; }
        public static ConditionEvaluatorCollection ConditionEvaluatorCollection { get; private set; }
 
        #endregion

        /// <summary>
        /// 初始化整体应用相关的所有的Plug
        /// </summary>
        /// <param name="sp">The sp.</param>
        public static void CoreInitialization(StartParameter sp)
        {
            if (sp.ApplicationName == null ||
                sp.ApplicationStartupPath == null ||
                sp.PlugStartupPath == null)
            {
                throw new Exception("Aarhus: Startup Parameter isn't set!");
            }

            ApplicationName = sp.ApplicationName;
            ApplicationStartupPath = Path.GetFullPath(sp.ApplicationStartupPath);
            PlugStartupPath = Path.GetFullPath(sp.PlugStartupPath);
            if (!string.IsNullOrEmpty(sp.PlugCustomPath))
            {
                PlugCustomPath = Path.GetFullPath(sp.PlugCustomPath);
            }
            IsAllowUseCustomPath = sp.IsAllowUseCustomPath;

            PlugPath = new PlugPath(ApplicationName);
            PlugPath.IsRoot = true;

            PlugCollection = new PlugCollection();
            BuilderCollection = new BuilderCollection();
            PlugFileCollection = PlugManager.SearchPlugFiles(PlugStartupPath);

            if (IsAllowUseCustomPath && Directory.Exists(PlugCustomPath))// 如果允许用户自定义Plug所在的路径，找到所有的自定义路径下的Plug文件
            {
                StringCollection sc = PlugManager.SearchPlugFiles(PlugCustomPath);
                foreach (var item in sc)
                {
                    PlugFileCollection.Add(item);
                }
            }

            foreach (string file in PlugFileCollection)
            {
                Plug plug = null;
                if (Plug.TryParse(file, out plug))
                {
                    PlugCollection.Add(plug);
                }
            }
        }

        internal static StringCollection SearchPlugFiles(string dirpath)
        {
            return UtilityFile.SearchDirectory(dirpath, "*.ahsplugin", true, true);
        }


        public static object BuildItem(string path, object caller)
        {
            return null;
            //int pos = path.LastIndexOf('/');
            //string parent = path.Substring(0, pos);
            //string child = path.Substring(pos + 1);
            //AddInTreeNode node = GetTreeNode(parent);
            //return node.BuildChildItem(child, caller, new ArrayList(BuildItems<object>(path, caller, false)));
        }

        public static List<T> BuildItems<T>(string path, object caller)
        {
            return BuildItems<T>(path, caller, true);
        }

        public static List<T> BuildItems<T>(string path, object caller, bool throwOnNotFound)
        {
            //AddInTreeNode node = GetTreeNode(path, throwOnNotFound);
            if (true)
                return new List<T>();
            else
                return new List<T>(); //node.BuildChildItems<T>(caller);
        }


        /// <summary>
        /// 启动参数
        /// </summary>
        public class StartParameter
        {

            public string ApplicationName = null;
            public string ApplicationStartupPath = null;
            public string PlugStartupPath = null;
            public string PlugCustomPath = null;

            /// <summary>
            /// 是否允许使用定制的插件路径
            /// </summary>
            public bool IsAllowUseCustomPath = true;

        }

#if DEBUG
        internal static void Init()
        {
            PlugPath = new PlugPath("ApplicationName");
            PlugPath.IsRoot = true;

            PlugFileCollection = new StringCollection();
            BuilderCollection = new BuilderCollection();
            PlugCollection = new PlugCollection();
            ConditionEvaluatorCollection = new ConditionEvaluatorCollection();
        }
#endif

    }

}





        /*
        static string configurationFileName;
        static string plugInstallTemp;
        static string userPlugPath;

        /// <summary>
        /// Installs the Plugs from PlugInstallTemp to the UserPlugPath.
        /// In case of installation errors, a error message is displayed to the user
        /// and the affected Plug is added to the disabled list.
        /// This method is normally called by <see cref="CoreStartup.ConfigureUserPlugs"/>
        /// </summary>
        public static void InstallPlugs(List<string> disabled)
        {
            if (!Directory.Exists(PlugStartupPath))
                return;
            LogService.Info("PlugManager.InstallPlugs started");
            if (!Directory.Exists(userPlugPath))
                Directory.CreateDirectory(userPlugPath);
            string removeFile = Path.Combine(plugInstallTemp, "remove.txt");
            bool allOK = true;

            List<string> notRemovedList = new List<string>();
            if (File.Exists(removeFile))
            {
                using (StreamReader r = new StreamReader(removeFile))
                {
                    string plugName;
                    while ((plugName = r.ReadLine()) != null)
                    {
                        plugName = plugName.Trim();
                        if (plugName.Length == 0)
                            continue;
                        string targetDir = Path.Combine(userPlugPath, plugName);
                        if (!UninstallPlug(disabled, plugName, targetDir))
                        {
                            notRemovedList.Add(plugName);
                            allOK = false;
                        }
                    }
                }
                if (notRemovedList.Count == 0)
                {
                    LogService.Info("Deleting remove.txt");
                    File.Delete(removeFile);
                }
                else
                {
                    LogService.Info("Rewriting remove.txt");
                    using (StreamWriter w = new StreamWriter(removeFile))
                    {
                        notRemovedList.ForEach(w.WriteLine);
                    }
                }
            }
            foreach (string sourceDir in Directory.GetDirectories(plugInstallTemp))
            {
                string plugName = Path.GetFileName(sourceDir);
                string targetDir = Path.Combine(userPlugPath, plugName);
                if (notRemovedList.Contains(plugName))
                {
                    LogService.Info("Skipping installation of " + plugName + " because deinstallation failed.");
                    continue;
                }
                if (UninstallPlug(disabled, plugName, targetDir))
                {
                    LogService.Info("Installing " + plugName + "...");
                    Directory.Move(sourceDir, targetDir);
                }
                else
                {
                    allOK = false;
                }
            }
            if (allOK)
            {
                try
                {
                    Directory.Delete(plugInstallTemp, false);
                }
                catch (Exception ex)
                {
                    LogService.Warn("Error removing install temp", ex);
                }
            }
            LogService.Info("PlugManager.InstallPlugs finished");
        }

        static bool UninstallPlug(List<string> disabled, string plugName, string targetDir)
        {
            if (Directory.Exists(targetDir))
            {
                LogService.Info("Removing " + plugName + "...");
                try
                {
                    Directory.Delete(targetDir, true);
                }
                catch (Exception ex)
                {
                    disabled.Add(plugName);
                    MessageService.ShowError("Error removing " + plugName + ":\n" +
                                             ex.Message + "\nThe Plug will be " +
                                             "removed on the next start of " + 
                                             " and is disabled for now.");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Uninstalls the user addin on next start.
        /// <see cref="RemoveUserPlugOnNextStart"/> schedules the Plug for
        /// deinstallation, you can unschedule it using
        /// <see cref="AbortRemoveUserPlugOnNextStart"/>
        /// </summary>
        /// <param name="identity">The identity of the addin to remove.</param>
        public static void RemoveUserPlugOnNextStart(string identity)
        {
            List<string> removeEntries = new List<string>();
            string removeFile = Path.Combine(plugInstallTemp, "remove.txt");
            if (File.Exists(removeFile))
            {
                using (StreamReader r = new StreamReader(removeFile))
                {
                    string plugName;
                    while ((plugName = r.ReadLine()) != null)
                    {
                        plugName = plugName.Trim();
                        if (plugName.Length > 0)
                        {
                            removeEntries.Add(plugName);
                        }
                    }
                }
                if (removeEntries.Contains(identity))
                    return;
            }
            removeEntries.Add(identity);
            if (!Directory.Exists(plugInstallTemp))
                Directory.CreateDirectory(plugInstallTemp);
            using (StreamWriter w = new StreamWriter(removeFile))
            {
                removeEntries.ForEach(w.WriteLine);
            }
        }

        /// <summary>
        /// Prevents a user Plug from being uninstalled.
        /// <see cref="RemoveUserPlugOnNextStart"/> schedules the Plug for
        /// deinstallation, you can unschedule it using
        /// <see cref="AbortRemoveUserPlugOnNextStart"/>
        /// </summary>
        /// <param name="identity">The identity of which to abort the removal.</param>
        public static void AbortRemoveUserPlugOnNextStart(string identity)
        {
            string removeFile = Path.Combine(plugInstallTemp, "remove.txt");
            if (!File.Exists(removeFile))
            {
                return;
            }
            List<string> removeEntries = new List<string>();
            using (StreamReader r = new StreamReader(removeFile))
            {
                string plugName;
                while ((plugName = r.ReadLine()) != null)
                {
                    plugName = plugName.Trim();
                    if (plugName.Length > 0)
                        removeEntries.Add(plugName);
                }
            }
            if (removeEntries.Remove(identity))
            {
                using (StreamWriter w = new StreamWriter(removeFile))
                {
                    removeEntries.ForEach(w.WriteLine);
                }
            }
        }

        /// <summary>
        /// Adds the specified external Plugs to the list of registered external
        /// Plugs.
        /// </summary>
        /// <param name="plugs">
        /// The list of Plugs to add. (use <see cref="Plug"/> instances
        /// created by <see cref="Plug.Load(TextReader)"/>).
        /// </param>
        public static void AddExternalPlugs(IList<Plug> plugs)
        {
            List<string> plugFiles = new List<string>();
            List<string> disabled = new List<string>();
            LoadPlugConfiguration(plugFiles, disabled);

            foreach (Plug plug in plugs)
            {
                if (!plugFiles.Contains(plug.FileName))
                    plugFiles.Add(plug.FileName);
                plug.Enabled = false;
                plug.Action = PlugAction.Install;
                PlugTree.InsertPlug(plug);
            }

            SavePlugConfiguration(plugFiles, disabled);
        }

        /// <summary>
        /// Removes the specified external Plugs from the list of registered external
        /// Plugs.
        /// </summary>
        /// The list of Plugs to remove.
        /// (use external Plugs from the <see cref="PlugTree.Plugs"/> collection).
        public static void RemoveExternalPlugs(IList<Plug> plugs)
        {
            List<string> plugFiles = new List<string>();
            List<string> disabled = new List<string>();
            LoadPlugConfiguration(plugFiles, disabled);

            foreach (Plug plug in plugs)
            {
                foreach (string identity in plug.PlugManifest.Identities.Keys)
                {
                    disabled.Remove(identity);
                }
                plugFiles.Remove(plug.FileName);
                plug.Action = PlugAction.Uninstall;
                if (!plug.Enabled)
                {
                    PlugTree.RemovePlug(plug);
                }
            }

            SavePlugConfiguration(plugFiles, disabled);
        }

        /// <summary>
        /// Marks the specified Plugs as enabled (will take effect after
        /// next application restart).
        /// </summary>
        public static void Enable(IList<Plug> plugs)
        {
            List<string> plugFiles = new List<string>();
            List<string> disabled = new List<string>();
            LoadPlugConfiguration(plugFiles, disabled);

            foreach (Plug plug in plugs)
            {
                foreach (string identity in plug.PlugManifest.Identities.Keys)
                {
                    disabled.Remove(identity);
                }
                if (plug.Action == PlugAction.Uninstall)
                {
                    if (UtilityFile.IsBaseDirectory(userPlugPath, plug.FileName))
                    {
                        foreach (string identity in plug.PlugManifest.Identities.Keys)
                        {
                            AbortRemoveUserPlugOnNextStart(identity);
                        }
                    }
                    else
                    {
                        if (!plugFiles.Contains(plug.FileName))
                            plugFiles.Add(plug.FileName);
                    }
                }
                plug.Action = PlugAction.Enable;
            }

            SavePlugConfiguration(plugFiles, disabled);
        }

        /// <summary>
        /// Marks the specified Plugs as disabled (will take effect after
        /// next application restart).
        /// </summary>
        public static void Disable(IList<Plug> plugs)
        {
            List<string> plugFiles = new List<string>();
            List<string> disabled = new List<string>();
            LoadPlugConfiguration(plugFiles, disabled);

            foreach (Plug plug in plugs)
            {
                string identity = "";// plug.PlugManifest.PrimaryIdentity;
                if (identity == null)
                    throw new ArgumentException("The Plug cannot be disabled because it has no identity.");

                if (!disabled.Contains(identity))
                    disabled.Add(identity);
                plug.Action = PlugAction.Disable;
            }

            SavePlugConfiguration(plugFiles, disabled);
        }

        /// <summary>
        /// Loads a configuration file.
        /// The 'file' from XML elements in the form "&lt;Plug file='full path to .addin file'&gt;" will
        /// be added to <paramref name="plugFiles"/>, the 'addin' element from
        /// "&lt;Disable addin='addin identity'&gt;" will be added to <paramref name="disabledPlugs"/>,
        /// all other XML elements are ignored.
        /// </summary>
        /// <param name="plugFiles">File names of external Plugs are added to this collection.</param>
        /// <param name="disabledPlugs">Identities of disabled addins are added to this collection.</param>
        public static void LoadPlugConfiguration(List<string> plugFiles, List<string> disabledPlugs)
        {
            if (!File.Exists(configurationFileName))
                return;
            using (XmlTextReader reader = new XmlTextReader(configurationFileName))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Plug")
                        {
                            string fileName = reader.GetAttribute("file");
                            if (fileName != null && fileName.Length > 0)
                            {
                                plugFiles.Add(fileName);
                            }
                        }
                        else if (reader.Name == "Disable")
                        {
                            string plug = reader.GetAttribute("addin");
                            if (plug != null && plug.Length > 0)
                            {
                                disabledPlugs.Add(plug);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the Plug configuration in the format expected by
        /// <see cref="LoadPlugConfiguration"/>.
        /// </summary>
        /// <param name="plugFiles">List of file names of external Plugs.</param>
        /// <param name="disabledPlugs">List of Identities of disabled addins.</param>
        public static void SavePlugConfiguration(List<string> plugFiles, List<string> disabledPlugs)
        {
            using (XmlTextWriter writer = new XmlTextWriter(configurationFileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("PlugConfiguration");
                foreach (string file in plugFiles)
                {
                    writer.WriteStartElement("Plug");
                    writer.WriteAttributeString("file", file);
                    writer.WriteEndElement();
                }
                foreach (string name in disabledPlugs)
                {
                    writer.WriteStartElement("Disable");
                    writer.WriteAttributeString("addin", name);
                    writer.WriteEndElement();
                }
                writer.WriteEndDocument();
            }
        }
        */
