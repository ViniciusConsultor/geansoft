using System;
using System.Collections.Generic;
using System.Text;
using Gean;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Xml;

namespace Pansoft.CQMS.Options
{
    public class OptionManager// : IOptionManager
    {
        #region 单件实例

        private OptionManager()
        {
            //在这里添加构造函数的代码
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static OptionManager Instance
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton()
            {
                Instance = new OptionManager();
                Instance.Options = new OptionCollection(false);
                Instance.IsChange = false;
                Instance.ChangeEventArgsList = new List<Option.OptionChangeEventArgs>();
            }

            internal static readonly OptionManager Instance = null;
        }

        #endregion

        public String ApplicationStartPath { get { return AppDomain.CurrentDomain.SetupInformation.ApplicationBase; } }
        public OptionFile OptionFile { get; internal set; }
        public OptionCollection Options { get; internal set; }
        public Boolean IsChange { get; private set; }
        public List<Option.OptionChangeEventArgs> ChangeEventArgsList { get; private set; }
        internal XmlDocument OptionDocument { get; private set; }

        public void Initializes(string optionFile)
        {
            string optionFilePath = string.Empty;
            if (File.Exists(optionFile))
            {
                optionFilePath = optionFile;
            }
            else
            {
                optionFilePath = Path.Combine(ApplicationStartPath, optionFile);
            }

            this.OptionFile = OptionFile.Load(optionFilePath);

            this.OptionDocument = new XmlDocument();
            this.OptionDocument.Load(optionFilePath);

            StringCollection files = UtilityFile.SearchDirectory(ApplicationStartPath, "*.dll", true, true);
            foreach (string file in files)
            {
                Assembly ass = Assembly.LoadFile(file);
                Type[] types = ass.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsDefined(typeof(OptionAttribute), false))//如果Class被定制特性标记
                    {
                        object[] objs = type.GetCustomAttributes(false);
                        foreach (var obj in objs)
                        {
                            if (!(obj is OptionAttribute))
                            {
                                continue;
                            }
                            Option[] options = Option.Load(ass, type, (OptionAttribute)obj);
                            foreach (Option o in options)
                            {
                                o.OptionChangingEvent += new Option.OptionChangingEventHandler(OptionChangingEvent);
                            }
                        }
                    }
                }
            }

        }

        void OptionChangingEvent(object sender, Option.OptionChangeEventArgs e)
        {
            this.IsChange = true;
            this.ChangeEventArgsList.Add(e);
        }

        public void ReLoad()
        {
            this.Options.Clear();
            this.OptionDocument = null;
            this.Initializes(this.OptionFile.File.FullName);
        }

        public bool Save()
        {
            try
            {
                this.OptionDocument.Save(this.OptionFile.File.FullName);
                this.IsChange = false;
                this.ChangeEventArgsList.Clear();
            }
            catch
            {
                throw;
            }
            return true;
        }

        public FileInfo Backup(string file)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(file);
            }
            catch (DirectoryNotFoundException)
            {
                UtilityFile.CreateDirectory(Path.GetDirectoryName(file));
                this.Backup(file);
            }
            catch (IOException)
            {
                FileAttributes fileAtts = FileAttributes.Normal;
                //先获取此文件的属性
                fileAtts = System.IO.File.GetAttributes(file);
                //讲文件属性设置为普通（即没有只读和隐藏等）
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                System.IO.File.Delete(file);
                this.Backup(file);
            }
            this.OptionDocument.Save(fs);
            return new FileInfo(file);
        }

    }
}
