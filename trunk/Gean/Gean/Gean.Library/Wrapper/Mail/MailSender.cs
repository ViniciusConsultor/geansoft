using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Timers;

namespace Gean.Mail
{
	/// <summary>
	/// �ʼ����͹��ߣ�֧����֤����
	/// </summary>
	public static class MailSender
	{
		#region constructor

        static MailSender()
        {
            if (MailSetting.Current != null)
            {
                smtpServer = MailSetting.Current.Server;
                serverPort = MailSetting.Current.Port;
                userName = MailSetting.Current.UserName;
                password = MailSetting.Current.Password;
            }
        }

		#endregion
		
		#region private static fields

		private static string smtpServer;
		private static int serverPort;
		private static string userName;
		private static string password;
        private static Timer timer;
        private static Queue<MailMessage> msgQueue;
        private static object lockObject = new object();

        private static void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();

            int msgCount = msgQueue.Count;
            if (msgCount > 0)
            {
                SmtpClient smtp = new SmtpClient(smtpServer, serverPort);
                smtp.Credentials = new NetworkCredential(userName, password);
                while (msgCount-- > 0)
                {
                    MailMessage message = null;
                    try
                    {
                        message = msgQueue.Dequeue();
                        if (message != null)
                        {
                            smtp.Send(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        string title = null;
                        string to = null;
                        if (message != null)
                        {
                            title = message.Subject;
                            to = message.To.ToString();
                        }
                        //LoggorHelper.WriteLog((object)null, "Mail Send Failed\r\nTitle: {0}\r\nTo: {1}\r\nException:\r\n{2}", title, to, ex);
                    }
                }
            }

            timer.Start();
        }

		#endregion

		#region public static methods

		/// <summary>
		/// �����ʼ�
		/// </summary>
		/// <param name="message">MailMessageʵ��</param>
		public static void Send(MailMessage message) {
			Send(message, true);
		}

		/// <summary>
		/// �����ʼ�
		/// </summary>
		/// <param name="message">MailMessageʵ��</param>
		/// <param name="cached">�Ƿ񻺴��ʼ�����߳�����Ӧ�ٶȣ�</param>
		public static void Send(MailMessage message, bool cached) {
			if (cached) {
				lock (lockObject) {
					if (msgQueue == null) {
						msgQueue = new Queue<MailMessage>();
					}
					if (timer == null) {
						timer = new Timer(1000);
						timer.Enabled = false;
						timer.AutoReset = false;
						timer.Elapsed += new ElapsedEventHandler(TimerOnElapsed);
					}
					if (!timer.Enabled) {
						timer.Enabled = true;
					}
				}
				lock (msgQueue) {
					msgQueue.Enqueue(message);
				}
			} else {
				SmtpClient smtp = new SmtpClient(smtpServer, serverPort);
				smtp.Credentials = new NetworkCredential(userName, password);
				smtp.Send(message);
			}
		}

		/// <summary>
		/// �����ʼ�
		/// </summary>
		/// <param name="from">�����ߵ�ַ</param>
		/// <param name="to">�����ߵ�ַ����������ַ����Ӣ�ķֺš�;���ָ</param>
		/// <param name="subject">�ʼ�����</param>
		/// <param name="messageText">�ʼ�����</param>
		public static void Send(string from, string to, string subject, string messageText) {
			Send(from, to, subject, messageText, true);
		}

		/// <summary>
		/// �����ʼ�
		/// </summary>
		/// <param name="from">�����ߵ�ַ</param>
		/// <param name="to">�����ߵ�ַ����������ַ����Ӣ�ķֺš�;���ָ</param>
		/// <param name="subject">�ʼ�����</param>
		/// <param name="messageText">�ʼ�����</param>
		/// <param name="cached">�Ƿ񻺴��ʼ�����߳�����Ӧ�ٶȣ�</param>
		public static void Send(string from, string to, string subject, string messageText, bool cached) {
			MailMessage message = new MailMessage(from, to, subject, messageText);
			Send(message, cached);
		}

		#endregion

		#region public static properties

		/// <summary>
		/// SMTP��������ַ
		/// </summary>
		public static string SmtpServer {
			get { return smtpServer; }
			set { smtpServer = value; }
		}

		/// <summary>
		/// �����������˿�
		/// </summary>
		public static int ServerPort {
			get { return serverPort; }
			set { serverPort = value; }
		}

		/// <summary>
		/// ��֤�û���
		/// </summary>
		public static string UserName {
			get { return userName; }
			set { userName = value; }
		}

		/// <summary>
		/// ��֤�û�����
		/// </summary>
		public static string Password {
			get { return password; }
			set { password = value; }
		}

		#endregion
	}
}
