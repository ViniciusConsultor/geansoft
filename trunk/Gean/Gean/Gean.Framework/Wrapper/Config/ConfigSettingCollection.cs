namespace Gean.Config
{
    /// <summary>
    /// ���ýڼ���
    /// </summary>
    public class ConfigSettingCollection : CollectionBase<ConfigSetting>
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="uniqueKey">��ֵ�Ƿ�Ψһ</param>
        public ConfigSettingCollection(bool uniqueKey) : base(uniqueKey) { }

        /// <summary>
        /// ������ý�
        /// </summary>
        /// <param name="setting">���ý�</param>
        /// <returns>���ý�</returns>
        public virtual ConfigSetting Add(ConfigSetting setting)
        {
            this.Add(setting.Name, setting);
            return setting;
        }

        /// <summary>
        /// ���/�滻���ýڣ�����������滻��
        /// </summary>
        /// <param name="setting">���ý�</param>
        public virtual ConfigSetting Set(ConfigSetting setting)
        {
            Converting.StringToEnum<ConfigSettingOperator>("");
            this.Set(setting.Name, setting);
            return setting;
        }

        /// <summary>
        /// ��ȸ��Ƽ���
        /// </summary>
        /// <returns>���ƺ�ļ���</returns>
        public virtual ConfigSettingCollection Clone()
        {
            ConfigSettingCollection collection = new ConfigSettingCollection(this.UniqueKey);
            foreach (ConfigSetting setting in this.Values)
            {
                collection.Add(setting.Clone()).Parent = setting.Parent;
            }
            return collection;
        }
    }
}