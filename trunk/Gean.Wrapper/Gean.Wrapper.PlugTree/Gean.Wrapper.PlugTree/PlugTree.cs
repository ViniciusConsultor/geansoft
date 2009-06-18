using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    public static class PlugTree
    {
        static RunnerCollection _Runners = new RunnerCollection();

        static ConditionCollection _Conditions = new ConditionCollection();

        static PlugPath _PlugPath = null;

        static string[] _PlugFiles = null;


        public static RunnerCollection Runners
        {
            get { return _Runners; }
        }

        public static ConditionCollection Conditions
        {
            get { return _Conditions; }
        }

        public static PlugPath PlugPath
        {
            get { return _PlugPath; }
        }

        public static string[] PlugFiles
        {
            get { return _PlugFiles; }
        }

    }
}
