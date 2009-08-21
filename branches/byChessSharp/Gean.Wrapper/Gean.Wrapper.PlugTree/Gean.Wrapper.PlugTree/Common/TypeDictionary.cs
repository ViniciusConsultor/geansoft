using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 键是Type的全名，值就是该System.Type。该类是抽象类。
    /// </summary>
    public abstract class TypeDictionary : ReadOnlyDictionary<Type> { }
}
