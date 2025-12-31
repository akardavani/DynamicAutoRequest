using Domain.Model;
using Services;

namespace BusinessService.SendRequest
{
    public  class ArmanxRequest : BaseRequest
    {
        public  async Task<(string text, LogJson jsonLog)> Send(TimeSpan delay, OrderData orderData)
        {
            try
            {
                var requestTime = DateTime.Now + delay;

                var stringContent = orderData.stringContent.Replace("\\", "");

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{orderData.OriginUrl}/api/order".Replace(".trade", ".trade/api-tse-root"));

                request.Headers.Add("accept", "application/json");
                request.Headers.Add("accept-language", "fa");
                request.Headers.Add("authorization", $"{orderData.Authorization}");
                request.Headers.Add("origin", $"{orderData.OriginUrl}");
                request.Headers.Add("referer", $"{orderData.OriginUrl}/");
                request.Headers.Add("priority", "u=1, i");
                request.Headers.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
                request.Headers.Add("sec-ch-ua-mobile", "?0");
                request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                request.Headers.Add("sec-fetch-dest", "empty");
                request.Headers.Add("sec-fetch-mode", "cors");
                request.Headers.Add("sec-fetch-site", "same-origin");
                request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");

                var content = new StringContent(stringContent, null, "application/json");

                request.Content = content;

                var startTime = DateTime.Now + delay;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();


                //var endTime = DateTime.Now + delay;
                //var elapsedTime = endTime - startTime;

                //var logJson = new LogJson
                //{
                //    //Isin = orderData.SahraRequest.isin,
                //    StartDate = startTime,
                //    EndDate = endTime,
                //    StartDateString = startTime.ToString("hh:mm:ss.fff tt"),
                //    EndDateString = endTime.ToString("hh:mm:ss.fff tt"),
                //    ElapsedTime = elapsedTime.Milliseconds,
                //    Response = responseText
                //};

                //var text = @$"{stringContent} {Environment.NewLine} {Environment.NewLine} {responseText}";
                //return (text, logJson);
                var log = Logging.Log(delay, stringContent, startTime, responseText);
                return log;
            }
            catch (Exception ex)
            {
                var time = DateTime.Now + delay;
                _ = Logging.WriteToFileAsync($"{Environment.NewLine}{time} - {ex.Message}", "log");
                return ("", null);
            }
        }
    }
}