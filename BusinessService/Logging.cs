using Domain.Model;
using System.Text;

namespace Services
{
    public class Logging
    {
        public static async Task WriteToFileAsync(string content, string name)
        {
            string _path = Directory.GetCurrentDirectory() + @"\json\Logging\";
            var filePath = $@"{_path}{name}.txt";

            await WriteFileStream(content, filePath);            
        }

        //public static async Task WriteToFileAsync(string content, RequestTimeData requestTimeData, int startSum, int endSum)
        //{
        //    string _path = Directory.GetCurrentDirectory() + @"\json\Logging\";
        //    var filePath = $"{_path}log_{requestTimeData.TotalRequests}_{requestTimeData.BatchSize}_{requestTimeData.Delay} ({startSum} ms)_({endSum} ms).txt";

        //    await WriteFileStream(content, filePath);
        //    //if (!File.Exists(filePath))
        //    //{
        //    //    using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
        //    //    {
        //    //        byte[] encodedContent = Encoding.UTF8.GetBytes(content);
        //    //        await stream.WriteAsync(encodedContent, 0, encodedContent.Length);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    using (FileStream stream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
        //    //    {
        //    //        byte[] encodedContent = Encoding.UTF8.GetBytes(content);
        //    //        await stream.WriteAsync(encodedContent, 0, encodedContent.Length);
        //    //    }
        //    //}
        //}


        //public static async Task SaveLog(List<LogJson> log, RequestTimeData requestTimeData)
        //{
        //    log = log.OrderBy(e => e.StartDate).ToList();

        //    for (int i = 1; i < log.Count; i++)
        //    {
        //        log[i].StartDefferent = (log[i].StartDate - log[i - 1].StartDate).Milliseconds;
        //        log[i].EndDefferent = (log[i].EndDate - log[i - 1].EndDate).Milliseconds;
        //    }

        //    var startSum = log.Sum(e => e.StartDefferent);
        //    var endSum = log.Sum(e => e.EndDefferent);

        //    //var gr = log.GroupBy(e => e.BatchNumber).Select(e =>new { Key = e.Key});

        //    var json = JsonConvert.SerializeObject(log.OrderBy(e => e.StartDate).ToList());

        //    string _path = Directory.GetCurrentDirectory() + @"\Logging\";

        //    bool folderExists = Directory.Exists(_path);
        //    if (!folderExists)
        //        Directory.CreateDirectory(_path);

        //    var filePath = $"{_path}log_{requestTimeData.TotalRequests}_{requestTimeData.BatchSize}_{requestTimeData.Delay} ({startSum} ms)_({endSum} ms).txt";

        //    await WriteFileStream(json, filePath);

        //    //await Logging.WriteToFileAsync(json, requestTimeData, startSum, endSum);
        //}

        private static async Task WriteFileStream(string content, string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    byte[] encodedContent = Encoding.UTF8.GetBytes(content);
                    await stream.WriteAsync(encodedContent, 0, encodedContent.Length);
                }
            }
            else
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    byte[] encodedContent = Encoding.UTF8.GetBytes(content);
                    await stream.WriteAsync(encodedContent, 0, encodedContent.Length);
                }
            }
        }

        public static (string text, LogJson jsonLog) Log(TimeSpan delay, string stringContent, DateTime startTime, string responseText)
        {
            var endTime = DateTime.Now + delay;
            var elapsedTime = endTime - startTime;

            var logJson = new LogJson
            {
                //Isin = orderData.SahraRequest.isin,
                StartDate = startTime,
                EndDate = endTime,
                StartDateString = startTime.ToString("hh:mm:ss.fff tt"),
                EndDateString = endTime.ToString("hh:mm:ss.fff tt"),
                ElapsedTime = elapsedTime.Milliseconds,
                Response = responseText
            };

            var text = @$"{stringContent} {Environment.NewLine} {Environment.NewLine} {responseText}";

            return (text, logJson);
        }

    }
}
