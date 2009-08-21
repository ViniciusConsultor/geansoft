using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个对象生产者接口,主要用来根据Plug.Definers生产相应的类型，例如：某Plug.Definers中定义是一个Menuitem，
    /// 那就调用相应的MenuitemProducer来创建这个对象。
    /// </summary>
    public interface IProducer
    {
        string Name { get; }
        object CreateObject(Plug plug, object caller);
    }
}
