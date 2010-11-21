using System;
namespace Gean.Net.Messages
{
    public interface IMessage
    {
        string ClientId { get; }
        int ClientType { get; }
        string Command { get; }
        string Data { get; set; }
        string TalkId { get; }
        string TimeTicks { get; }
        MessageSource Source { get; }
        MessageStatus Status { get; }
    }
}
