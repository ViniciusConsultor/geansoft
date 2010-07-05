using System.Collections.Generic;
using Gean;
namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// 选项值集合
    /// </summary>
    public class OptionValueCollection : IndexCollection<OptionValue>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OptionValueCollection() : base(true) { }

        /// <summary>
        /// 添加选项值
        /// </summary>
        /// <param name="value">选项值</param>
        /// <returns>选项值</returns>
        public virtual OptionValue Add(OptionValue value)
        {
            this.Add(value.Name, value);
            return value;
        }

        /// <summary>
        /// 添加/替换选项值（如果存在则替换）
        /// </summary>
        /// <param name="value">选项值</param>
        public virtual void Set(OptionValue value)
        {
            this.Set(value.Name, value);
        }
    }
}