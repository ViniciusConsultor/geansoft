namespace Gean.Config
{
    /// <summary>
    /// ����ֵ����
    /// </summary>
    public class SettingValueCollection : CollectionBase<SettingValue>
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        public SettingValueCollection() : base(true) { }

        /// <summary>
        /// �������ֵ
        /// </summary>
        /// <param name="value">����ֵ</param>
        /// <returns>����ֵ</returns>
        public virtual SettingValue Add(SettingValue value)
        {
            this.Add(value.Name, value);
            return value;
        }

        /// <summary>
        /// ���/�滻����ֵ������������滻��
        /// </summary>
        /// <param name="value">����ֵ</param>
        public virtual void Set(SettingValue value)
        {
            this.Set(value.Name, value);
        }
    }
}