using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 从所有Plug文件中解析出实现了IRun接口的类型，以键值对的模式存储在一个集合里。
    /// 键是类的全名，值为System.Type。
    /// 通过GetIRunObject方法可以直接获得已实例的IRun接口类型。
    /// </summary>
    public sealed class RunnerCollection : ReadOnlyDictionary<IRun>
    {
    }
}
