using Infrastructure;
using Domain;
using Domain.Model;
using System.Net.Http.Headers;
using System.Text;

namespace BusinessService.SendRequest
{
    public class EasyTraderRequest : IOmsRequest
    {
        public EasyTraderOrderRequestSnapshot snapshot = null;

        public EasyTraderRequest()
        {
            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            snapshot = JsonConvertor.ReadJsonData<EasyTraderOrderRequestSnapshot>(JsonFileNames.EasyTraderOrderRequestSnapshot, jsonFolderPath);
        }

        public async Task<HttpResponseMessage> SendAsync(TimeSpan delay)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, snapshot.Url);

            request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
            request.Headers.TryAddWithoutValidation("accept-language", "fa");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", snapshot.Authorization.Replace("Bearer ", ""));

            request.Headers.TryAddWithoutValidation("origin", snapshot.Origin);
            request.Headers.TryAddWithoutValidation("referer", snapshot.Referer);
            request.Headers.TryAddWithoutValidation("user-agent", snapshot.UserAgent);

            if (!string.IsNullOrWhiteSpace(snapshot.Cookie))
                request.Headers.TryAddWithoutValidation("cookie", snapshot.Cookie);

            request.Content = new StringContent(snapshot.JsonBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            _ = LogQueue.Channel.Writer.WriteAsync(new RequestLog
            {
                Provider = OmsProvider.EasyTrader.ToString(),
                Time = DateTime.Now,
                Url = snapshot.Url,
                RequestBody = snapshot.JsonBody,
                ResponseBody = responseText,
                StatusCode = (int)response.StatusCode
            });

            return response;
        }
    }
}