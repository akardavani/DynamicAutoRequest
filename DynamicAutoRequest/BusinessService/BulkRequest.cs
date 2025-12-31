using BusinessService.SendRequest;
using Domain;

namespace DynamicAutoRequest.BusinessService
{
    public class BulkRequest
    {
        public static async Task Send(TimeSpan delay, RequestTimeData requestTimeData)
        {
            int sent = 0;

            while (sent < requestTimeData.TotalRequests)
            {
                int currentBatchSize = Math.Min(
                    requestTimeData.BatchSize,
                    requestTimeData.TotalRequests - sent
                );

                var tasks = Enumerable
                    .Range(0, currentBatchSize)
                    .Select(_ =>
                        SendHttpRequest.SendAsync(
                            delay,
                            (OmsProvider)requestTimeData.OmsProvider
                        )
                    )
                    .ToArray();

                await Task.WhenAll(tasks);

                sent += currentBatchSize;

                if (!CheckTime(delay, requestTimeData))
                    await Task.Delay(requestTimeData.Delay);
            }

            //await Logging.SaveLog(log.ToList(), requestTimeData);
        }

        private static bool CheckTime(TimeSpan delay, RequestTimeData requestTimeData)
        {
            var resp = false;
            var request_time = DateTime.Now.TimeOfDay - delay;

            var startSplit = requestTimeData.StartTime.Split(":");
            var endSplit = requestTimeData.EndTime.Split(":");
            var start = new TimeSpan(int.Parse(startSplit[0]), int.Parse(startSplit[1]), int.Parse(startSplit[2]));
            var end = new TimeSpan(int.Parse(endSplit[0]), int.Parse(endSplit[1]), int.Parse(endSplit[2]));

            if (start == end)
            {
                return resp;
            }

            if (request_time >= start && request_time <= end)
            {
                resp = true;
            }

            return resp;
        }
    }
}
