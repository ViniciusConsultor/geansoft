using System.Configuration;

namespace Gean.Config.DotNetConfig
{
    /// <summary>
    /// ���ü��ϣ����ͣ�
    /// </summary>
    /// <typeparam name="T">����Ԫ������</typeparam>
    public class ConfigCollection<T> : BaseConfigurationElementCollection 
        where T : ConfigurationElement, new()
    {
        /// <summary>
        /// ��������ʽ��ȡԪ��
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>Ԫ��</returns>
        public virtual T this[int index]
        {
            get { return (T)this.BaseGet(index); }
        }

        /// <summary>
        /// ����ֵ��ʽ��ȡԪ��
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <returns>Ԫ��</returns>
        public virtual T this[object key]
        {
            get { return (T)this.BaseGet(key); }
        }

        /// <summary>
        /// ����ת��������
        /// </summary>
        /// <returns>Ԫ������</returns>
        public virtual T[] ToArray()
        {
            T[] values = new T[this.Count];
            this.CopyTo(values, 0);
            return values;
        }

        /// <summary>
        /// ������Ԫ��
        /// </summary>
        /// <returns>��Ԫ��</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }
    }
}