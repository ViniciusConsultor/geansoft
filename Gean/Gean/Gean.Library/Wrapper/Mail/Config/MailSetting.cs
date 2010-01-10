using System.Configuration;
using Gean.Config.DotNetConfig;

namespace Gean.Mail
{
    /// <summary>
    /// �ʼ�������������Ϣ���ο�<seealso cref="SectionHandler"/>
    /// </summary>
    public class MailSetting : BaseConfigurationElement
    {
        /// <summary>
        /// ��������ַ��IP��������
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string)this["server"]; }
        }

        /// <summary>
        /// ����������˿�
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = 25)]
        public int Port
        {
            get { return (int)this["port"]; }
        }

        /// <summary>
        /// ��֤�û���
        /// </summary>
        [ConfigurationProperty("userName")]
        public string UserName
        {
            get { return (string)this["userName"]; }
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)this["password"]; }
        }

        internal static MailSetting Current
        {
            get
            {
                if (SectionHandler.Current != null)
                {
                    return SectionHandler.Current.MailSetting;
                }
                return null;
            }
        }
    }
}
