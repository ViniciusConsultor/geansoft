using System;
using System.Collections.Generic;
using System.Text;
using Gean;
using System.Collections.Specialized;
using System.Reflection;

namespace Pansoft.CQMS.Options
{
    public class OptionManager// : IOptionManager
    {

        private static string ApplicationStartPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        public OptionCollection GetOption(string param)
        {
            throw new NotImplementedException();
        }

        public void Initializes(string optionFile)
        {
            StringCollection files = UtilityFile.SearchDirectory(ApplicationStartPath, "*.dll", true, true);
            foreach (string file in files)
            {
                Assembly ass = Assembly.LoadFile(file);
                Type[] types = ass.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsDefined(typeof(OptionFileAttribute), false))
                    {
                        Console.WriteLine(type);
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
