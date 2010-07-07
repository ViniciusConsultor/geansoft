using System.Collections.Generic;
using Gean;
namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// ѡ��ڼ���
    /// </summary>
    public class OptionCollection : IndexCollection<UUOption>
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="uniqueKey">��ֵ�Ƿ�Ψһ</param>
        public OptionCollection(bool uniqueKey) : base(uniqueKey) { }

        /// <summary>
        /// ���ѡ���
        /// </summary>
        /// <param name="option">ѡ���</param>
        /// <returns>ѡ���</returns>
        public virtual UUOption Add(UUOption option)
        {
            this.Add(option.Name, option);
            return option;
        }

        /// <summary>
        /// ���/�滻ѡ��ڣ�����������滻��
        /// </summary>
        /// <param name="option">ѡ���</param>
        public virtual UUOption Set(UUOption option)
        {
            Converting.StringToEnum<OptionOperatorEnum>("");
            this.Set(option.Name, option);
            return option;
        }

        /// <summary>
        /// ��ȸ��Ƽ���
        /// </summary>
        /// <returns>���ƺ�ļ���</returns>
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