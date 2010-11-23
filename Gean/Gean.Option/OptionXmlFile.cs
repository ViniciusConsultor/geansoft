using System;
using System.Collections.Generic;
using System.Text;
using Gean.Xml;

namespace Gean.Options
{
    /// <summary>
    /// 描述一个选项XML文件，封装了XmlDoucment在本类型中。
    /// </summary>
    public class OptionXmlFile : AbstractXmlDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionXmlFile"/> class.
        /// </summary>
        /// <param name="filePath">XML文件的物理绝对路径</param>
        public OptionXmlFile(string filePath)
            : base(filePath)
        {

        }
    }
}
