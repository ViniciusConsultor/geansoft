using System;

namespace Gean.Config
{
    /// <summary>
    /// 配置值的实现，提供一些缺省的实现
    /// </summary>
    public abstract class SettingValue : Converting, ISettingValue
    {
        /// <summary>
        /// 保护构造方法
        /// </summary>
        /// <param name="name">配置值名</param>
        /// <param name="value">配置值</param>
        /// <param name="readonly">是否只读</param>
        protected SettingValue(string name, string value, bool @readonly)
        {
            this.name = name;
            this.value = @value;
            this.@readonly = @readonly;
        }

        /// <summary>
        /// 配置值名
        /// </summary>
        protected string name;
        /// <summary>
        /// 配置值
        /// </summary>
        protected string value;
        /// <summary>
        /// 是否只读
        /// </summary>
        protected bool @readonly;

        /// <summary>
        /// 被转换的值
        /// </summary>
        protected override string ConvertingValue
        {
            get { return this.Value; }
        }

        /// <summary>
        /// 创建配置值实例
        /// </summary>
        /// <param name="name">配置值名</param>
        /// <param name="value">配置值</param>
        /// <param name="readonly">是否只读</param>
        /// <returns>SettingValue</returns>
        protected abstract SettingValue CreateSettingValue(string name, string value, bool @readonly);

        /// <summary>
        /// 配置值名
        /// </summary>
        public virtual string Name
        {
            get { return this.name; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new ConfigException("配置值只读");
                }
                else
                {
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// 配置值
        /// </summary>
        public virtual string Value
        {
            get { return this.value; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new ConfigException("配置值只读");
                }
                else
                {
                    this.value = value;
                }
            }
        }

        /// <summary>
        /// 当前配置值是否只读
        /// </summary>
        public virtual bool ReadOnly
        {
            get { return this.@readonly; }
        }

        internal virtual void SetName(string name)
        {
            this.name = name;
        }

        #region Clone

        /// <summary>
        /// 克隆配置值
        /// </summary>
        /// <returns>SettingValue</returns>
        public virtual SettingValue Clone()
        {
            return this.Clone(this.ReadOnly);
        }

        /// <summary>
        /// 克隆配置值
        /// </summary>
        /// <param name="readonly">是否只读</param>
        /// <returns>SettingValue</returns>
        public virtual SettingValue Clone(bool @readonly)
        {
            return this.CreateSettingValue(this.Name, this.Value, @readonly);
        }

        #endregion Clone

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        ISettingValue ISettingValue.Clone(bool @readonly)
        {
            return this.Clone();
        }

        #endregion

        #region ISettingValue Members

        bool ISettingValue.ReadOnly
        {
            get { return this.ReadOnly; }
        }

        string ISettingValue.Name
        {
            get { return this.Name; }
        }

        string ISettingValue.Value
        {
            get { return this.Value; }
        }

        #endregion
    }
}
