namespace Gean.Config
{
    /// <summary>
    /// 配置节集合
    /// </summary>
    public class ConfigSettingCollection : CollectionBase<ConfigSetting>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="uniqueKey">键值是否唯一</param>
        public ConfigSettingCollection(bool uniqueKey) : base(uniqueKey) { }

        /// <summary>
        /// 添加配置节
        /// </summary>
        /// <param name="setting">配置节</param>
        /// <returns>配置节</returns>
        public virtual ConfigSetting Add(ConfigSetting setting)
        {
            this.Add(setting.Name, setting);
            return setting;
        }

        /// <summary>
        /// 添加/替换配置节（如果存在则替换）
        /// </summary>
        /// <param name="setting">配置节</param>
        public virtual ConfigSetting Set(ConfigSetting setting)
        {
            Converting.StringToEnum<ConfigSettingOperator>("");
            this.Set(setting.Name, setting);
            return setting;
        }

        /// <summary>
        /// 深度复制集合
        /// </summary>
        /// <returns>复制后的集合</returns>
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