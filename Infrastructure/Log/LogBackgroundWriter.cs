using System.Text.Json;

namespace Infrastructure
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

                        string path = Path.Combine(Environment.CurrentDirectory,
                            "Logs", 
                            $"{log.Provider}_{DateTime.Now:yyyyMMdd}.log");

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
}
