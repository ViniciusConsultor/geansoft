using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    public static class PlugTree
    {
        public static RunnerCollection Runners
        {
            get { return _Runners; }
        }
        private static RunnerCollection _Runners = null;

        public static PlugPath PlugPath
        {
            get { return _PlugPath; }
        }
        private static PlugPath _PlugPath = null;

        public static string[] PlugFiles
        {
            get { return _PlugFiles; }
        }
        private static string[] _PlugFiles = null;

    }
}
