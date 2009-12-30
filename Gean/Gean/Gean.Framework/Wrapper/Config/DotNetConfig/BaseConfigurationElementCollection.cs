using System.Configuration;

namespace Gean.Config.DotNetConfig
{
    /// <summary>
    /// ����������Ԫ�ؼ��ϣ����󣩣��̳��� <seealso cref="ConfigurationElementCollection"/>
    /// </summary>
    public abstract class BaseConfigurationElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// ���Ԫ�صļ�
        /// </summary>
        /// <param name="element">����Ԫ��</param>
        /// <returns>��</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.GetHashCode();
        }
    }
}