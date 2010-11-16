using System;
using System.Collections.Generic;
using System.Text;
using Gean;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Xml;
using NLog;

namespace Gean.Option
{
    public class OptionManager : IOptionManager
    {
        private Logger _Logger = LogManager.GetCurrentClassLogger();

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

            static readonly OptionManager Instance = null;
        }

        #endregion

        public String ApplicationStartPath { get { return AppDomain.CurrentDomain.SetupInformation.ApplicationBase; } }
        public OptionFile OptionFile { get; internal set; }
        public OptionCollection Options { get; internal set; }
        public Boolean IsChange { get; private set; }
        public List<Option.OptionChangeEventArgs> ChangeEventArgsList { get; private set; }
        internal XmlDocument OptionDocument { get; private set; }

        public bool Initializes(params object[] args)
        {
            if (args == null || args.Length <=0 || !(args[0] is string))
            {
                _Logger.Error("OptionService初始化参数不全，Option文件路径未传入。");
                return false;
            }
            string file = args[0].ToString();

            string optionFilePath = File.Exists(file) ? file : Path.Combine(ApplicationStartPath, file);

            this.OptionFile = OptionFile.Load(optionFilePath);
            this.OptionDocument = new XmlDocument();
            this.OptionDocument.Load(optionFilePath);

            StringCollection files = UtilityFile.SearchDirectory(ApplicationStartPath, "*.dll", true, true);
            foreach (string dll in files)
            {
                Assembly ass = Assembly.LoadFile(dll);
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
            return true;
        }

        void OptionChangingEvent(object sender, Option.OptionChangeEventArgs e)
        {
            this.IsChange = true;
            this.ChangeEventArgsList.Add(e);
        }

        public bool ReStart(params object[] objects)
        {
            try
            {
                this.Options.Clear();
                this.OptionDocument = null;
                this.Initializes(this.OptionFile.File.FullName);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
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

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }
    }
}
