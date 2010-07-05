using System.Collections.Generic;
using Gean;
namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// 选项节集合
    /// </summary>
    public class OptionCollection : IndexCollection<Option>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="uniqueKey">键值是否唯一</param>
        public OptionCollection(bool uniqueKey) : base(uniqueKey) { }

        /// <summary>
        /// 添加选项节
        /// </summary>
        /// <param name="setting">选项节</param>
        /// <returns>选项节</returns>
        public virtual Option Add(Option setting)
        {
            this.Add(setting.Name, setting);
            return setting;
        }

        /// <summary>
        /// 添加/替换选项节（如果存在则替换）
        /// </summary>
        /// <param name="setting">选项节</param>
        public virtual Option Set(Option setting)
        {
            Converting.StringToEnum<OptionOperatorEnum>("");
            this.Set(setting.Name, setting);
            return setting;
        }

        /// <summary>
        /// 深度复制集合
        /// </summary>
        /// <returns>复制后的集合</returns>
        public virtual OptionCollection Clone()
        {
            OptionCollection collection = new OptionCollection(this.UniqueKey);
            foreach (Option setting in this.Values)
            {
                collection.Add(setting.Clone()).Parent = setting.Parent;
            }
            return collection;
        }
    }
}