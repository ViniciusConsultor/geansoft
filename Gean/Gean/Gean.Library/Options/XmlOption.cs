using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gean.Options;

namespace Gean.Options
{
    /// <summary>
    /// 内容保存在XML中的Option的抽象类
    /// </summary>
    public abstract class XmlOption : Option<XmlElement>
    {
    }
}
