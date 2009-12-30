using System;

namespace Gean.Config
{
    /// <summary>
    /// ����ֵ��ʵ�֣��ṩһЩȱʡ��ʵ��
    /// </summary>
    public abstract class SettingValue : Converting, ISettingValue
    {
        /// <summary>
        /// �������췽��
        /// </summary>
        /// <param name="name">����ֵ��</param>
        /// <param name="value">����ֵ</param>
        /// <param name="readonly">�Ƿ�ֻ��</param>
        protected SettingValue(string name, string value, bool @readonly)
        {
            this.name = name;
            this.value = @value;
            this.@readonly = @readonly;
        }

        /// <summary>
        /// ����ֵ��
        /// </summary>
        protected string name;
        /// <summary>
        /// ����ֵ
        /// </summary>
        protected string value;
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        protected bool @readonly;

        /// <summary>
        /// ��ת����ֵ
        /// </summary>
        protected override string ConvertingValue
        {
            get { return this.Value; }
        }

        /// <summary>
        /// ��������ֵʵ��
        /// </summary>
        /// <param name="name">����ֵ��</param>
        /// <param name="value">����ֵ</param>
        /// <param name="readonly">�Ƿ�ֻ��</param>
        /// <returns>SettingValue</returns>
        protected abstract SettingValue CreateSettingValue(string name, string value, bool @readonly);

        /// <summary>
        /// ����ֵ��
        /// </summary>
        public virtual string Name
        {
            get { return this.name; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new ConfigException("����ֵֻ��");
                }
                else
                {
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        public virtual string Value
        {
            get { return this.value; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new ConfigException("����ֵֻ��");
                }
                else
                {
                    this.value = value;
                }
            }
        }

        /// <summary>
        /// ��ǰ����ֵ�Ƿ�ֻ��
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
        /// ��¡����ֵ
        /// </summary>
        /// <returns>SettingValue</returns>
        public virtual SettingValue Clone()
        {
            return this.Clone(this.ReadOnly);
        }

        /// <summary>
        /// ��¡����ֵ
        /// </summary>
        /// <param name="readonly">�Ƿ�ֻ��</param>
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
