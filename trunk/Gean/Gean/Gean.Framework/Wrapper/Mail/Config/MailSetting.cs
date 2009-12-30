using System.Configuration;
using Gean.Config.DotNetConfig;

namespace Gean.Mail
{
    /// <summary>
    /// 邮件服务器配置信息，参看<seealso cref="SectionHandler"/>
    /// </summary>
    public class MailSetting : BaseConfigurationElement
    {
        /// <summary>
        /// 服务器地址（IP或域名）
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string)this["server"]; }
        }

        /// <summary>
        /// 服务的侦听端口
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = 25)]
        public int Port
        {
            get { return (int)this["port"]; }
        }

        /// <summary>
        /// 认证用户名
        /// </summary>
        [ConfigurationProperty("userName")]
        public string UserName
        {
            get { return (string)this["userName"]; }
        }

        /// <summary>
        /// 认证密码
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
