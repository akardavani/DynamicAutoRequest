using Infrastructure;
using Domain;
using Domain.Model;
using System.Net.Http.Headers;
using System.Text;

namespace BusinessService.SendRequest
{
    public class RabinRequest : IOmsRequest
    {
        public RabinOrderRequestSnapshot snapshot = null;

        public RabinRequest()
        {
            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            snapshot = JsonConvertor.ReadJsonData<RabinOrderRequestSnapshot>(JsonFileNames.RabinOrderRequestSnapshot, jsonFolderPath);
        }

        public async Task<HttpResponseMessage> SendAsync(TimeSpan delay)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, snapshot.Url);

            // ===== Headers =====
            request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
            request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.9,fa;q=0.8");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", snapshot.Authorization.Replace("Bearer ", ""));

            request.Headers.TryAddWithoutValidation("fp", snapshot.FingerPrint);
            request.Headers.TryAddWithoutValidation("origin", snapshot.Origin);
            request.Headers.TryAddWithoutValidation("referer", snapshot.Referer);
            request.Headers.TryAddWithoutValidation("user-agent", snapshot.UserAgent);

            if (!string.IsNullOrWhiteSpace(snapshot.Cookie))
                request.Headers.TryAddWithoutValidation("cookie", snapshot.Cookie);

            // ===== Body =====
            request.Content = new StringContent(
                snapshot.JsonBody,
                Encoding.UTF8,
                "application/json"
            );

            // ===== Send =====
            var response = await client.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            // ===== Async Logging (Non-blocking) =====
            _ = LogQueue.Channel.Writer.WriteAsync(new RequestLog
            {
                Provider = OmsProvider.Rabin.ToString(),
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