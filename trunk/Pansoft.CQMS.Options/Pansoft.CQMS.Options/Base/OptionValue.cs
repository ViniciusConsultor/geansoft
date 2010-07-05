using System;
using Gean;


namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// 选项值的实现，提供一些缺省的实现
    /// </summary>
    public abstract class OptionValue : Converting, IOptionValue
    {
        /// <summary>
        /// 保护构造方法
        /// </summary>
        /// <param name="name">选项值名</param>
        /// <param name="value">选项值</param>
        /// <param name="readonly">是否只读</param>
        protected OptionValue(string name, string value, bool @readonly)
        {
            this._name = name;
            this._value = @value;
            this._readonly = @readonly;
        }

        /// <summary>
        /// 选项值名
        /// </summary>
        protected string _name;

        /// <summary>
        /// 选项值
        /// </summary>
        protected string _value;

        /// <summary>
        /// 是否只读
        /// </summary>
        protected bool _readonly;

        /// <summary>
        /// 被转换的值
        /// </summary>
        protected override string ConvertingValue
        {
            get { return this.Value; }
        }

        /// <summary>
        /// 创建选项值实例
        /// </summary>
        /// <param name="name">选项值名</param>
        /// <param name="value">选项值</param>
        /// <param name="readonly">是否只读</param>
        /// <returns>SettingValue</returns>
        protected abstract OptionValue CreateOptionValue(string name, string value, bool @readonly);

        /// <summary>
        /// 选项值名
        /// </summary>
        public virtual string Name
        {
            get { return this._name; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new OptionException("选项值只读");
                }
                else
                {
                    this._name = value;
                }
            }
        }

        /// <summary>
        /// 选项值
        /// </summary>
        public virtual string Value
        {
            get { return this._value; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new OptionException("选项值只读");
                }
                else
                {
                    this._value = value;
                }
            }
        }

        /// <summary>
        /// 当前选项值是否只读
        /// </summary>
        public virtual bool ReadOnly
        {
            get { return this._readonly; }
        }

        internal virtual void SetName(string name)
        {
            this._name = name;
        }

        #region Clone

        /// <summary>
        /// 克隆选项值
        /// </summary>
        /// <returns>SettingValue</returns>
        public virtual OptionValue Clone()
        {
            return this.Clone(this.ReadOnly);
        }

        /// <summary>
        /// 克隆选项值
        /// </summary>
        /// <param name="readonly">是否只读</param>
        /// <returns>SettingValue</returns>
        public virtual OptionValue Clone(bool @readonly)
        {
            return this.CreateOptionValue(this.Name, this.Value, @readonly);
        }

        #endregion Clone

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        IOptionValue IOptionValue.Clone(bool @readonly)
        {
            return this.Clone();
        }

        #endregion

        #region ISettingValue Members

        bool IOptionValue.ReadOnly
        {
            get { return this.ReadOnly; }
        }

        string IOptionValue.Name
        {
            get { return this.Name; }
        }

        string IOptionValue.Value
        {
            get { return this.Value; }
        }

        #endregion
    }
}
