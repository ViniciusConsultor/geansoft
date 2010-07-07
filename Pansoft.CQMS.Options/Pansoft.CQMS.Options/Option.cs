using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using Gean;

namespace Pansoft.CQMS.Options
{

    public sealed class Option : ICloneable
    {
        #region constructor

        private Option()
        {
        }

        #endregion

        #region property AND field for property

        public Object Entity { get; private set; }
        public String Name { get; private set; }

        #endregion

        #region public static methods

        public static Option Builder(Assembly ass, Type type, OptionAttribute optionAttr)
        {
            Option option = new Option();
            if (!optionAttr.IsCollection)//如果不是集合型的选项
            {
                option.Name = optionAttr.OptionSectionName;
                XmlNode optionNode = OptionManager.Instance.OptionDocument.DocumentElement.SelectSingleNode(optionAttr.OptionSectionName);
                if (optionNode != null)//当选项的xml节点可以从选项文件找到
                {
                    //从类型创建对象
                    option.Entity = UtilityType.CreateObject(ass, type.FullName, type, true, null);
                    //从类型获得该类型的所有属性
                    PropertyInfo[] propertyInfoList = type.GetProperties();

                    //为每个属性赋值
                    foreach (PropertyInfo info in propertyInfoList)
                    {
                        object[] valueAttrs = info.GetCustomAttributes(false);
                        foreach (var attr in valueAttrs)
                        {
                            if (attr is OptionValueAttribute)
                            {
                                OptionValueAttribute valueAttr = (OptionValueAttribute)attr;
                                XmlElement optionElement = (XmlElement)optionNode;
                                Object obj = UtilityConvert.ConvertTo(optionElement.GetAttribute(valueAttr.Name), info.PropertyType);
                                info.SetValue(option.Entity, obj, null);
                            }
                            else
                            {
                                continue;
                            }
                        }//foreach
                    }//foreach PropertyInfo
                }
            }
            else
            {
                option.Name = optionAttr.OptionSectionName;
                XmlNode optionNode = OptionManager.Instance.OptionDocument.DocumentElement.SelectSingleNode(optionAttr.ParentSectionName);
                if (optionNode != null)//当选项的xml节点可以从选项文件找到
                {
                    if (optionNode.HasChildNodes)
                    {
                        foreach (XmlNode node in optionNode)
                        {
                            if (node.NodeType == XmlNodeType.Element)
                            {
                                //从类型创建对象
                                option.Entity = UtilityType.CreateObject(ass, type.FullName, type, true, null);
                                //从类型获得该类型的所有属性
                                PropertyInfo[] propertyInfoList = type.GetProperties();

                                //为每个属性赋值
                                foreach (PropertyInfo info in propertyInfoList)
                                {
                                    object[] valueAttrs = info.GetCustomAttributes(false);
                                    foreach (var attr in valueAttrs)
                                    {
                                        if (attr is OptionValueAttribute)
                                        {
                                            OptionValueAttribute valueAttr = (OptionValueAttribute)attr;
                                            XmlElement optionElement = (XmlElement)node;
                                            Object obj = UtilityConvert.ConvertTo(optionElement.GetAttribute(valueAttr.Name), info.PropertyType);
                                            info.SetValue(option.Entity, obj, null);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }//foreach
                                }//foreach PropertyInfo
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            return option;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        #endregion

        #region fields

        #endregion

        #region ICloneable

        public Option Clone()
        {
            return null;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion
    }
}
