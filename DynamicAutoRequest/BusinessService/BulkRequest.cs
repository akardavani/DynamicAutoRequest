using BusinessService.Json;
using BusinessService.SendRequest;
using Domain.Model;
using Services;
using System.Collections.Concurrent;

namespace DynamicAutoRequest.BusinessService
{
    public class BulkRequest
    {
        public static async Task Send(TimeSpan delay, RequestTimeData requestTimeData)
        {
            var orderDatas = JsonConvertor.ReadJsonDataCollection<OrderData>("OrderData").ToList();
            var log = new ConcurrentBag<LogJson>();

            for (int i = 0; i < requestTimeData.TotalRequests; i += requestTimeData.BatchSize)
            {
                var tasks = orderDatas.Select(async orderData =>
                {
                    var resp = await SendHttpRequest.Send(delay, requestTimeData.OmsProvider, orderData);
                    if (resp.log is not null)
                    {
                        resp.log.BatchNumber = i;
                        log.Add(resp.log);
                    }
                });

                await Task.WhenAll(tasks);

                if (!CheckTime(delay, requestTimeData))
                    await Task.Delay(requestTimeData.Delay);
            }

            await Logging.SaveLog(log.ToList(), requestTimeData);
        }


        //public static async Task Send(TimeSpan delay, RequestTimeData requestTimeData)
        //{
        //    int numberOfRequests = requestTimeData.TotalRequests;
        //    int batchSize = requestTimeData.BatchSize;
        //    var orderDatas = JsonConvertor.ReadJsonDataCollection<OrderData>("OrderData").ToList();
        //    var log = new List<LogJson>();
        //    for (int i = 0; i < numberOfRequests; i += batchSize)
        //    {
        //        List<int> requestNumbers = Enumerable.Range(i + 1, batchSize).ToList();

        //        Parallel.ForEach(requestNumbers, async (number) =>
        //        {
        //            _ = Parallel.ForEach(orderDatas, async (orderData) =>
        //            {
        //                var resp = await SendHttpRequest.Send(delay, requestTimeData.OmsProvider, orderData);

        //                if (resp.jsonLog is not null)
        //                {
        //                    resp.jsonLog.BatchNumber = i;
        //                    log.Add(resp.jsonLog);
        //                }
        //            });
        //        });

        //        if (CheckTime(delay, requestTimeData))
        //        {
        //            continue;
        //        }

        //        await Task.Delay(requestTimeData.Delay); // مکث به میلی‌ثانیه
        //    }

        //    await Task.Delay(30 * 1000);

        //    await Logging.SaveLog(log, requestTimeData);
        //}

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

        //private static bool CheckTime(TimeSpan delay, RequestTimeData requestTimeData)
        //{
        //    var requestTime = DateTime.Now.TimeOfDay - delay;
        //    var start = TimeSpan.Parse(requestTimeData.StartTime);
        //    var end = TimeSpan.Parse(requestTimeData.EndTime);

        //    return start != end && requestTime >= start && requestTime <= end;
        //}

    }
}
