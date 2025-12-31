using Domain.Model;
using Services;

namespace BusinessService.SendRequest
{
    public  class TadbirRequest : BaseRequest
    {
        public  async Task<(string text, LogJson jsonLog)> Send(TimeSpan delay, OrderData orderData)
        {
            try
            {
                var requestTime = DateTime.Now + delay;

                var stringContent = orderData.stringContent;

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{orderData.OriginUrl}/Web/V1/Order/Post".Replace("https://online", "https://api"));
                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("Accept-Language", "en-US,en;q=0.9,fa;q=0.8");
                request.Headers.Add("Connection", "keep-alive");
                request.Headers.Add("Cookie", $"{orderData.Cookie}");
                request.Headers.Add("Origin", $"{orderData.OriginUrl}");
                request.Headers.Add("Referer", $"{orderData.OriginUrl}/");
                request.Headers.Add("Sec-Fetch-Dest", "empty");
                request.Headers.Add("Sec-Fetch-Mode", "cors");
                request.Headers.Add("Sec-Fetch-Site", "same-site");
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                request.Headers.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"125\", \"Chromium\";v=\"125\", \"Not.A/Brand\";v=\"24\"");
                request.Headers.Add("sec-ch-ua-mobile", "?0");
                request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                var content = new StringContent(stringContent, null, "application/json");
                request.Content = content;

                var startTime = DateTime.Now + delay;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();

                var log = Logging.Log(delay, stringContent, startTime, responseText);
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