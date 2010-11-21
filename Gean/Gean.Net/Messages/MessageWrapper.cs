﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Messages
{
    /// <summary>
    /// 当新（发送、回复）消息到达时,数据的封装器
    /// </summary>
    public class MessageWrapper : EventArgs, IMessage
    {
        private static string _ClientId = string.Empty;
        static MessageWrapper()
        {
            _ClientId = UtilityHardware.GetCpuID();
        }

        /// <summary>
        /// Gets or sets 命令字.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; private set; }
        /// <summary>
        /// Gets the 请求者类型.
        /// </summary>
        /// <value>The type of the client.</value>
        public int ClientType { get { return SocketOption.ME.ClientType; } }
        /// <summary>
        /// Gets the 请求者编号.
        /// </summary>
        /// <value>The client id.</value>
        public string ClientId { get { return _ClientId; } }
        /// <summary>
        /// Gets the 时间戳.
        /// </summary>
        /// <value>The time ticks.</value>
        public string TimeTicks 
        { 
            get { return _TimeTicks; } 
        }
        private string _TimeTicks = UtilityDateTime.CurrTicks().ToString();
        /// <summary>
        /// Gets or sets 交易ID（由交易的发起者负责维护）
        /// </summary>
        /// <value>The talk id.</value>
        public string TalkId
        { get { return _TalkId; } }
        private string _TalkId = DataIDGenerator.ME.Generate();
        /// <summary>
        /// Gets or sets 请求数据.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; set; }

        public MessageSource Source { get; private set; }
        public MessageStatus Status { get; private set; }

        public MessageWrapper(MessageSource msgSource, string command, string data)
        {
            this.Source = msgSource;
            this.Command = command;
            this.Data = data;
            this.Status = MessageStatus.OK;
        }
        private MessageWrapper() { }

        public static MessageWrapper Parse(string message, MessageSource msgSource)
        {
            //LST_OPER_TYPE|OK|20101118191412|BEMP12001|.......
            string[] result = SocketOption.ME.Spliter.Split(message);
            //第1个域，命令字
            if (result.Length <= 0 || string.IsNullOrEmpty(result[0]))
                return null;
            MessageWrapper wrapper = new MessageWrapper();
            wrapper.Source = msgSource;
            wrapper.Command = result[0];

            //第2个域，可能是“OK”，“ERR”
            if (result.Length <= 1)
                return wrapper;
            if (result[1].Equals("ERR"))
                wrapper.Status = MessageStatus.ERROR;
            else if (result[1].Equals("OK"))
                wrapper.Status = MessageStatus.OK;
            else
            {
                try
                {
                    wrapper.Status = (MessageStatus)Enum.Parse(typeof(MessageStatus), result[1]);
                }
                catch (Exception e)
                {
                    wrapper.Status = MessageStatus.ERROR;
                }
            }

            //第3个域，可能是“OK”，“ERR”
            if (result.Length <= 2)
                return wrapper;
            wrapper._TimeTicks = result[2].Trim();

            //第4个域，本次交易(对话)的ID
            if (result.Length <= 3)
                return wrapper;
            wrapper._TalkId = result[3].Trim();

            //第5个域，实际数据
            if (result.Length <= 4)
                return wrapper;
            //实际数据的具体解析交由Protocol组件中的解析器去解析
            wrapper.Data = result[4].Trim().TrimEnd('@');
            return wrapper;
        }

        public override string ToString()
        {
            return SocketOption.ME.Combiner.Combin(this);
        }
    }
}
