using BusinessService.SendRequest;
using Domain;
using Services;

namespace DynamicAutoRequest.BusinessService
{
    public class StartWork
    {
        public static async Task FindStartTime(RequestTimeData requestTimeData, CancellationToken cancellation = default)
        {
            try
            {
                var generateRequestTime = await GenerateBrokersRequestTime(requestTimeData.StartRequestTime, cancellation);
                var requestTime = generateRequestTime.request_time;
                var delay = await ServiceProviderExtensions.SyncTime(cancellation);

                while (true)
                {
                    if (requestTimeData.Log)
                    {
                        _ = Logging.WriteToFileAsync($"{Environment.NewLine}requestTime : {requestTime} - DateTime.Now : {DateTime.Now} ==>   dif : {requestTime - DateTime.Now}", "RequestTimeLog");
                    }
                    if (requestTime > DateTime.Now && requestTime < DateTime.Now.AddMilliseconds(2))
                    {
                        await BulkRequest.Send(delay, requestTimeData: requestTimeData);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static async Task<HttpResponseMessage> Test(RequestTimeData requestTimeData, CancellationToken cancellation = default)
        {
            try
            {
                var generateRequestTime = await GenerateBrokersRequestTime(requestTimeData.StartRequestTime, cancellation);
                var requestTime = generateRequestTime.request_time;
                var delay = await ServiceProviderExtensions.SyncTime(cancellation);

                var response = await SendHttpRequest.SendAsync(delay, (OmsProvider)requestTimeData.OmsProvider);

                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage();
            }
        }

        private static async Task<(DateTime request_time, TimeSpan delay)> GenerateBrokersRequestTime(string timeOrder, CancellationToken cancellation = default)
        {
            var dotSplit = timeOrder.Split('.');

            int hour = Convert.ToInt32(dotSplit[0].Split(':')[0]);
            int min = Convert.ToInt32(dotSplit[0].Split(':')[1]);
            int sec = Convert.ToInt32(dotSplit[0].Split(':')[2]);
            int mili = Convert.ToInt32(dotSplit[1]);

            var delay = await ServiceProviderExtensions.SyncTime(cancellation);

            var request_time = DateTime.Today.Date + new TimeSpan(0, hour, min, sec, mili) - delay;

            return (request_time, delay);
        }
    }
}
