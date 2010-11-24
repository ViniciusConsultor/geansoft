using System;

namespace Gean.Net.AloneSocket
{
    public class AloneTcpClient
    {
        static int _messageCount = 0;
        static object _messageCounterLock = new object();

        const string MESSAGEBASE = "Message ";

        static void Main()
        {
            MessageService.Instance.MessageSent += new EventHandler<MessageEventArgs>(OnMessageServiceMessageSent);

            string input = string.Empty;
            while (input == string.Empty)
            {
                input = Console.ReadLine();
                MessageService.Instance.Send(GetMessage());
            }
        }

        private static void OnMessageServiceMessageSent(object sender, MessageEventArgs e)
        {
            switch (e.Status)
            {
                case MessageStatus.Success:
                    Log(string.Format("Send of message \"{0}\" completed - remaining messages: {1}",
                    e.Message, e.QueuedMessageCount), ConsoleColor.Red);
                    break;
                case MessageStatus.Failure:
                    Log(string.Format("Send failed" + Environment.NewLine +
                    "Reason: {0}", e.Error), ConsoleColor.Red);
                    break;
            }
        }

        private static string GetMessage()
        {
            lock (_messageCounterLock)
            {
                return string.Format("{0} {1}", MESSAGEBASE, ++_messageCount);
            }
        }

        private static void Log(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(string.Format("{0} - {1}",
                DateTime.Now.ToLongTimeString(), message));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
