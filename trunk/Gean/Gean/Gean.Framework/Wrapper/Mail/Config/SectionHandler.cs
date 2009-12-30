using System.Configuration;
using Gean.Config.DotNetConfig;
using Gean.Mail;

namespace Gean.Mail
{
	/// <summary>
	/// <see cref="MailSender"/> �����ý�
	/// </summary>
	/// <remarks>
	/// �����ļ���ʽ��˵����
	///		<code>
	///			&lt;configSections&gt;
	///				&lt;sectionGroup name="htb.devfx" type="Gean.Config.GroupHandler, HTB.DevFx.BaseFx"&gt;
	///					&lt;section name="mail" type="HTB.DevFx.Utils.Mail.Config.SectionHandler, HTB.DevFx.BaseFx" /&gt;
	///					......
	///				&lt;/sectionGroup&gt;
	///			&lt;/configSections&gt;
	/// 
	///			......
	/// 
	///			&lt;htb.devfx&gt;
	///				&lt;mail&gt;
	///					&lt;smtpSetting server="" port="" userName="" password="" /&gt;
	///				&lt;/mail&gt;
	///			&lt;/htb.devfx&gt;
	///			......
	///		</code>
	/// </remarks>
	public class SectionHandler : SectionBaseHandler<SectionHandler>
	{
		/// <summary>
		/// Smtp������Ϣ
		/// </summary>
		[ConfigurationProperty("smtpSetting")]
		public MailSetting MailSetting {
			get { return (MailSetting)this["smtpSetting"]; }
		}
	}
}
