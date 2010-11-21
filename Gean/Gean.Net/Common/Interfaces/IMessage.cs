using System;
namespace Gean.Net.Common
{
    public interface IMessage
    {
        string ClientId { get; }
        int ClientType { get; }
        string Command { get; }
        string Data { get; set; }
        MessageSource Source { get; }
        string TalkId { get; }
        string TimeTicks { get; }
    }
}
