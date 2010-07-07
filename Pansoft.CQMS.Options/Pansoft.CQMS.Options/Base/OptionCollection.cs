using System.Collections.Generic;
using Gean;
namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// 选项节集合
    /// </summary>
    public class OptionCollection : IndexCollection<UUOption>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="uniqueKey">键值是否唯一</param>
        public OptionCollection(bool uniqueKey) : base(uniqueKey) { }

        /// <summary>
        /// 添加选项节
        /// </summary>
        /// <param name="option">选项节</param>
        /// <returns>选项节</returns>
        public virtual UUOption Add(UUOption option)
        {
            this.Add(option.Name, option);
            return option;
        }

        /// <summary>
        /// 添加/替换选项节（如果存在则替换）
        /// </summary>
        /// <param name="option">选项节</param>
        public virtual UUOption Set(UUOption option)
        {
            Converting.StringToEnum<OptionOperatorEnum>("");
            this.Set(option.Name, option);
            return option;
        }

        /// <summary>
        /// 深度复制集合
        /// </summary>
        /// <returns>复制后的集合</returns>
        public virtual OptionCollection Clone()
        {
            OptionCollection collection = new OptionCollection(this.UniqueKey);
            foreach (UUOption option in this.Values)
            {
                collection.Add(option.Clone()).Parent = option.Parent;
            }
            return collection;
        }
    }
}