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

        public String Name { get; private set; }
        public Object Entity { get; private set; }
        public XmlElement XmlElement { get; internal set; }

        #endregion

        #region public static methods

        public static Option[] Load(Assembly ass, Type type, OptionAttribute optionAttr)
        {
            List<Option> options = new List<Option>(); 
            #region
            if (!optionAttr.IsCollection)//如果不是集合型的选项
            {
                #region
                Option option = new Option();
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
                                option.XmlElement = optionElement;
                            }
                            else
                            {
                                continue;
                            }
                        }//foreach
                    }//foreach PropertyInfo
                }
                #endregion
                OptionManager.Instance.Options.Add(option);
                option.OnOptionLoaded(new OptionLoadedEventArgs(option));
                options.Add(option);
            }
            else
            {
                #region
                XmlNode optionNode = OptionManager.Instance.OptionDocument.DocumentElement.SelectSingleNode(optionAttr.ParentSectionName);
                if (optionNode != null)//当选项的xml节点可以从选项文件找到
                {
                    if (optionNode.HasChildNodes)
                    {
                        foreach (XmlNode optionChildrenNode in optionNode)
                        {
                            Option option = new Option();
                            #region
                            if (optionChildrenNode.NodeType == XmlNodeType.Element)
                            {
                                option.Name = optionAttr.OptionSectionName;

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
                                            XmlElement optionElement = (XmlElement)optionChildrenNode;
                                            Object obj = UtilityConvert.ConvertTo(optionElement.GetAttribute(valueAttr.Name), info.PropertyType);
                                            info.SetValue(option.Entity, obj, null);
                                            option.XmlElement = optionElement;
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
                            #endregion
                            OptionManager.Instance.Options.Add(option);
                            option.OnOptionLoaded(new OptionLoadedEventArgs(option));
                            options.Add(option);
                        }
                    }
                }
                #endregion
            }
            #endregion
            return options.ToArray();
        }

        #endregion

        #region public methods

        public Option SetOptionValue(string key, object value)
        {
            PropertyInfo[] propertyInfoList = this.Entity.GetType().GetProperties();
            foreach (PropertyInfo info in propertyInfoList)
            {
                object[] valueAttrs = info.GetCustomAttributes(false);
                foreach (var attr in valueAttrs)
                {
                    if (attr is OptionValueAttribute)
                    {
                        OptionValueAttribute valueAttr = (OptionValueAttribute)attr;
                        if (valueAttr.Name != key)
                        {
                            continue;
                        }
                        OnOptionChanging(new OptionChangeEventArgs(this, key, value));//选项值发生改变前的事件的注册
                        this.XmlElement.SetAttribute(valueAttr.Name, value.ToString());
                        Object obj = UtilityConvert.ConvertTo(this.XmlElement.GetAttribute(valueAttr.Name), info.PropertyType);
                        info.SetValue(this.Entity, obj, null);
                        OnOptionChanged(new OptionChangeEventArgs(this, key, value));//选项值发生改变后的事件的注册
                    }
                    else
                    {
                        continue;
                    }
                }//foreach
            }//foreach PropertyInfo
            return null;
        }

        #endregion

        #region event

        /// <summary>
        /// 当选项载入(Load)完成后发生的事件
        /// </summary>
        public event OptionLoadedEventHandler OptionLoadedEvent;
        private void OnOptionLoaded(OptionLoadedEventArgs e)
        {
            if (OptionLoadedEvent != null)
                OptionLoadedEvent(this, e);
        }
        public delegate void OptionLoadedEventHandler(Object sender, OptionLoadedEventArgs e);

        /// <summary>
        /// 选项值发生改变时的包含事件数据的类
        /// </summary>
        public class OptionLoadedEventArgs : EventArgs
        {
            public Option Option { get; private set; }
            public OptionLoadedEventArgs(Option option)
            {
                this.Option = option;
            }
        }


        /// <summary>
        /// 当选项改变后发生的事件
        /// </summary>
        public event OptionChangedEventHandler OptionChangedEvent;
        private void OnOptionChanged(OptionChangeEventArgs e)
        {
            if (OptionChangedEvent != null)
                OptionChangedEvent(this, e);
        }
        public delegate void OptionChangedEventHandler(Object sender, OptionChangeEventArgs e);

        /// <summary>
        /// 当选项改变前发生的事件(注意：此事件发生后，选项存在保存发生异常的可能性)
        /// </summary>
        public event OptionChangingEventHandler OptionChangingEvent;
        private void OnOptionChanging(OptionChangeEventArgs e)
        {
            if (OptionChangingEvent != null)
                OptionChangingEvent(this, e);
        }
        public delegate void OptionChangingEventHandler(Object sender, OptionChangeEventArgs e);

        /// <summary>
        /// 选项值发生改变时的包含事件数据的类
        /// </summary>
        public class OptionChangeEventArgs : EventArgs
        {
            public Option Option { get; private set; }
            public String OptionValueName { get; private set; }
            public Object OptionValue { get; private set; }
            public OptionChangeEventArgs(Option option, String key, Object value)
            {
                this.Option = option;
                this.OptionValueName = key;
                this.OptionValue = value;
            }
        }

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
