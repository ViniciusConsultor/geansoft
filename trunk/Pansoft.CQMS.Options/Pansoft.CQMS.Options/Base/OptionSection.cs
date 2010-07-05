using System;
using Gean;

namespace Pansoft.CQMS.Options
{
    /// <summary>
    ///  选项节属性的实现
    /// </summary>
    public abstract class OptionSection : IOptionSection
    {
        /// <summary>
        /// 保护构造方法
        /// </summary>
        /// <param name="properties">属性集合</param>
        /// <param name="readonly">是否只读</param>
        protected OptionSection(OptionValueCollection optionValues, bool readonlyFlag)
        {
            _optionValues = optionValues;
            _readonly = readonlyFlag;
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        protected bool _readonly;

        /// <summary>
        /// 属性集合
        /// </summary>
        protected OptionValueCollection _optionValues;

        /// <summary>
        /// 创建选项属性实例
        /// </summary>
        /// <param name="properties">属性集合</param>
        /// <param name="readonly">是否只读</param>
        /// <returns>OptionProperty</returns>
        protected abstract OptionSection CreateOptionSection(OptionValueCollection properties, bool @readonly);

        /// <summary>
        /// 创建选项属性实例
        /// </summary>
        /// <param name="readonly">是否只读</param>
        /// <returns>OptionProperty</returns>
        protected virtual OptionSection CreateOptionSection(bool @readonly)
        {
            return this.CreateOptionSection(new OptionValueCollection(), @readonly);
        }

        /// <summary>
        /// 创建选项值
        /// </summary>
        /// <param name="name">选项值名</param>
        /// <param name="value">选项值</param>
        /// <param name="readonly">是否只读</param>
        /// <returns>OptionValue</returns>
        protected abstract OptionValue CreateOptionValue(string name, string value, bool @readonly);

        /// <summary>
        /// 当前选项节属性是否只读
        /// </summary>
        public virtual bool ReadOnly
        {
            get { return this._readonly; }
        }

        /// <summary>
        /// 选项节的属性个数
        /// </summary>
        public virtual int Count
        {
            get { return this._optionValues.Count; }
        }

        /// <summary>
        /// 获取/设置属性值(根据属性名)
        /// </summary>
        /// <param name="propertyName">属性名</param>
        public virtual OptionValue this[string propertyName]
        {
            get { return this._optionValues[propertyName]; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new OptionException("选项属性只读");
                }
                this._optionValues.Set(propertyName, value);
            }
        }

        /// <summary>
        /// 获取属性值(根据属性索引)
        /// </summary>
        /// <param name="propertyIndex">属性索引</param>
        public virtual OptionValue this[int propertyIndex]
        {
            get { return this._optionValues[propertyIndex]; }
        }

        /// <summary>
        /// 尝试获取某属性值
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性值</returns>
        public virtual string TryGetPropertyValue(string propertyName)
        {
            OptionValue value = this._optionValues[propertyName];
            if (value != null)
            {
                return value.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 尝试获取某属性值并转换成指定类型
        /// </summary>
        /// <typeparam name="T">转换成指定的类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <returns>指定类型的实例</returns>
        public virtual T TryGetPropertyValue<T>(string propertyName)
        {
            return this.TryGetPropertyValue<T>(propertyName, default(T));
        }

        /// <summary>
        /// 尝试获取某属性值并转换成指定类型
        /// </summary>
        /// <typeparam name="T">转换成指定的类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>指定类型的实例</returns>
        public virtual T TryGetPropertyValue<T>(string propertyName, T defaultValue)
        {
            OptionValue value = this._optionValues[propertyName];
            if (value != null)
            {
                return (value as IConverting).TryToObject<T>(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        internal virtual OptionSection Merge(OptionSection property)
        {
            foreach (string key in property._optionValues.Keys)
            {
                switch (key)
                {
                    case Option.OptionFilePropertyName:
                    case Option.OptionNodePropertyName:
                        break;
                    default:
                        this._optionValues.Set(property[key].Clone());
                        break;
                }
            }
            return this;
        }

        #region Clone

        /// <summary>
        /// 克隆选项属性
        /// </summary>
        /// <param name="readonly">是否只读</param>
        /// <param name="deep">是否深度复制</param>
        /// <returns>OptionProperty</returns>
        public virtual OptionSection Clone(bool @readonly, bool deep)
        {
            OptionSection property = this.CreateOptionSection(@readonly);
            if (deep)
            {
                foreach (OptionValue settingValue in this._optionValues.Values)
                {
                    property._optionValues.Add(settingValue.Clone(@readonly));
                }
            }
            else
            {
                property._optionValues = this._optionValues;
            }
            return property;
        }

        #endregion Clone

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return this.Clone(this.ReadOnly, true);
        }

        #endregion

        #region IOptionProperty Members

        bool IOptionSection.ReadOnly
        {
            get { return this.ReadOnly; }
        }

        int IOptionSection.Count
        {
            get { return this.Count; }
        }

        IOptionValue IOptionSection.this[string propertyName]
        {
            get { return this[propertyName]; }
        }

        IOptionValue IOptionSection.this[int propertyIndex]
        {
            get { return this[propertyIndex]; }
        }

        IOptionSection IOptionSection.Clone(bool @readonly, bool deep)
        {
            return this.Clone(@readonly, deep);
        }

        string IOptionSection.TryGetPropertyValue(string propertyName)
        {
            return this.TryGetPropertyValue(propertyName);
        }

        T IOptionSection.TryGetPropertyValue<T>(string propertyName)
        {
            return this.TryGetPropertyValue<T>(propertyName);
        }

        T IOptionSection.TryGetPropertyValue<T>(string propertyName, T defaultValue)
        {
            return this.TryGetPropertyValue<T>(propertyName, defaultValue);
        }

        #endregion
    }
}