using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Pansoft.CQMS.Options
{
    public class OptionFile
    {
        public FileInfo File { get; private set; }

        public static OptionFile Load(string optionFileFullPath)
        {
            OptionFile ofile = new OptionFile();
            try
            {
                if (!System.IO.File.Exists(optionFileFullPath))//如果用户的选项文件不存在，创建默认的选项文件
                {
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
                ofile.File = new FileInfo(optionFileFullPath);
            }
            catch
            {
                //如果程序运行到这里，一般定义该选项文件虽然存在但发生了异常，将采取先删除再新建的办法处理
                try
                {
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
                    throw new IOException(string.Format("文件：{0}访问失败。\r\n原因：一般可能为被其他应用程序锁定。", optionFileFullPath));
                }
            }
            return ofile;
        }
    }
}

