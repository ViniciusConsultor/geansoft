using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gean.Wrapper.PlugTree.Service;
using System.Collections.Specialized;

namespace Gean.Wrapper.PlugTree
{

#if DEBUG

    /// <summary>
    /// 为UnitTest准备的类，该类的所有静态方法调用本程序集中的所有internal方法以供测试。
    /// 本类型置入#if DEBUG标签中，Release时本类型无效。
    /// </summary>
    public static class UnitTestClass
    {

        public static PlugPath GetPlugPath()
        {
            PlugPath PlugPath = new PlugPath("ApplicationName");
            PlugPath.IsRoot = true;
            return PlugPath;
        }

        public static Plug GetPlug()
        {
            return new Plug();
        }

        public static void PlugManager_PlugFileCollection_Init()
        {
            PlugManager.Init();
        }
    }

#endif
}
