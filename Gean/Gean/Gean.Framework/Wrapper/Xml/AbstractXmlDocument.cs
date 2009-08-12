using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace Gean.Xml
{
    /// <summary>
    /// 对XmlDocument的类的封装
    /// </summary>
    public abstract class AbstractXmlDocument : AbstractBaseXmlNode
    {

        #region 构造函数

        /// <summary>
        /// 基础的XmlDocument扩展(组合)类
        /// </summary>
        /// <param name="filePath">XML文件的物理绝对路径</param>
        public AbstractXmlDocument(string filePath)
        {
            this.FilePath = filePath;
            if (!File.Exists(this.FilePath))
            {
#if DEBUG   
                throw new FileNotFoundException("Xml File isn't Exists!");
#else
                this.BaseXmlNode = XmlHelper.CreatNewDoucmnet(this.FilePath, "root");//如果文件不存在，建立这个文件
#endif
            }
            else
            {
                this.BaseXmlNode = new XmlDocument();
                (this.BaseXmlNode as XmlDocument).Load(this.FilePath);
            }
            this.InitializeComponent();
        }

        protected virtual void InitializeComponent() { }

        #endregion

        #region 属性

        /// <summary>
        /// 获取文档的根 System.Xml.XmlElement。
        /// </summary>
        protected virtual XmlElement DocumentElement
        {
            get { return ((XmlDocument)this.BaseXmlNode).DocumentElement; }
        }

        /// <summary>
        /// 获取文档的根 System.Xml.XmlElement。
        /// </summary>
        protected virtual XmlNodeList ChildNodes
        {
            get { return this.BaseXmlNode.ChildNodes; }
        }

        /// <summary>
        /// 获取文档的绝对路径
        /// </summary>
        public string FilePath { get; private set; }

        #endregion

        #region 方法

        /// <summary>
        /// 保存当前XmlDocument
        /// </summary>
        public virtual void Save()
        {
            if (string.IsNullOrEmpty(this.FilePath))
            {
                Debug.Fail("this.FilePath is Null!");
                return;
            }
            FileAttributes fileAtts = FileAttributes.Normal;
            if (File.Exists(FilePath))
            {
                fileAtts = File.GetAttributes(FilePath);//先获取此文件的属性
                File.SetAttributes(FilePath, FileAttributes.Normal);//将文件属性设置为普通（即没有只读和隐藏等）
            }
            ((XmlDocument)this.BaseXmlNode).Save(FilePath);//在文件属性为普通的情况下保存。（不然有可能会“访问被拒绝”）
            File.SetAttributes(FilePath, fileAtts);//恢复文件属性
        }

        #endregion

    }
}
