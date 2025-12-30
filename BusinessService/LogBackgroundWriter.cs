using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BusinessService
{
    public static class LogBackgroundWriter
    {
        public static void Start()
        {
            Task.Run(async () =>
            {
                await foreach (var log in LogQueue.Channel.Reader.ReadAllAsync())
                {
                    try
                    {
                        var path = Path.Combine(
                            Environment.CurrentDirectory,
                            "Logs",
                            $"{DateTime.Now:yyyyMMdd}.log");

                        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                        var line = JsonSerializer.Serialize(log);
                        await File.AppendAllTextAsync(path, line + Environment.NewLine);
                    }
                    catch
                    {
                        // intentionally swallow
                        // لاگ نباید برنامه رو بکشه
                    }
                }
            });
        }
    }

    public static class LogQueue
    {
        public static readonly Channel<RequestLog> Channel =
            System.Threading.Channels.Channel.CreateUnbounded<RequestLog>();
    }
    public class RequestLog
    {
        public DateTime Time { get; set; }
        public string Url { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public int StatusCode { get; set; }
    }


}
