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
            }

            internal static readonly OptionManager Instance = null;
        }

        #endregion

        public String ApplicationStartPath { get { return AppDomain.CurrentDomain.SetupInformation.ApplicationBase; } }

        public OptionCollection Options { get; private set; }

        internal XmlDocument OptionDocument { get; private set; }

        public void Initializes(string optionFile)
        {
            this.Options = new OptionCollection(false);
            string optionFilePath = Path.Combine(ApplicationStartPath, optionFile);
            if (!File.Exists(optionFilePath))
            {
                OptionFile.Create(optionFilePath);
            }
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
                            this.Options.Add(Option.Builder(ass, type, (OptionAttribute)obj));
                        }
                    }
                        
                }
            }
        }

        public void ReLoad()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
