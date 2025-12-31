using System.Threading.Channels;

namespace Infrastructure
{
    public static class LogQueue
    {
        public static readonly Channel<RequestLog> Channel =
            System.Threading.Channels.Channel.CreateUnbounded<RequestLog>();
    }
}
