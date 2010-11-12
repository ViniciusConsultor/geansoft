using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NLog;
using System.Windows.Forms;

namespace Gean.Option
{
    public class OptionFile
    {
        private static Logger _Logger = LogManager.GetCurrentClassLogger();

        public FileInfo File { get; private set; }

        public static OptionFile Load(string optionFileFullPath)
        {
            OptionFile oFile = new OptionFile();
            try
            {
                if (!System.IO.File.Exists(optionFileFullPath))
                {
                    _Logger.Warn(string.Format("用户的选项文件不存在，创建默认的选项文件。{0}", optionFileFullPath));
                    using (XmlTextWriter w = new XmlTextWriter(optionFileFullPath, Encoding.UTF8))
                    {
                        w.Formatting = Formatting.Indented;
                        w.WriteStartDocument();
                        w.WriteStartElement("Options");
                        w.WriteAttributeString("ProductName", Application.ProductName);
                        w.WriteAttributeString("Created", DateTime.Now.ToString());
                        w.WriteAttributeString("ApplicationVersion", Application.ProductVersion);
                        w.WriteAttributeString("OptionVersion", "1");
                        w.WriteAttributeString("UpdateCount", "0");
                        w.WriteAttributeString("Modified", DateTime.Now.ToString());
                        w.WriteEndElement();
                        w.Flush();
                    }
                }
                oFile.File = new FileInfo(optionFileFullPath);
            }
            catch
            {
                try
                {
                    _Logger.Warn(string.Format("可能定义该选项文件虽然存在但发生了异常,将采取先删除再新建的办法处理。{0}", optionFileFullPath));
                    FileAttributes fileAtts = FileAttributes.Normal;
                    //先获取此文件的属性
                    fileAtts = System.IO.File.GetAttributes(optionFileFullPath);
                    //讲文件属性设置为普通（即没有只读和隐藏等）
                    System.IO.File.SetAttributes(optionFileFullPath, FileAttributes.Normal);
                    System.IO.File.Delete(optionFileFullPath);
                    Load(optionFileFullPath);
                }
                catch
                {
                    _Logger.Error(string.Format("文件：{0}访问失败。\r\n原因：一般可能为被其他应用程序锁定。", optionFileFullPath));
                }
            }
            return oFile;
        }
    }
}

