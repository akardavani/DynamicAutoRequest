using Domain.Enum;
using Domain.Model;
using Infrastructure;
using System.Net.Http.Headers;
using System.Text;

namespace BusinessService.SendRequest
{
    public class SmartRequest : IOmsRequest
    {
        public SmartOrderRequestSnapshot snapshot = null;

        public SmartRequest()
        {
            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            snapshot = JsonConvertor.ReadJsonData<SmartOrderRequestSnapshot>(JsonFileNames.SmartOrderRequestSnapshot, jsonFolderPath);
        }

        public async Task SendAsync(TimeSpan delay)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, snapshot.Url);

            // ===== Generic Headers =====
            foreach (var header in snapshot.Headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            // ===== Authorization =====
            if (!string.IsNullOrWhiteSpace(snapshot.Authorization))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        snapshot.Authorization.Replace("Bearer ", "")
                    );
            }

            // ===== Broker Specific =====
            if (!string.IsNullOrWhiteSpace(snapshot.FingerPrint))
                request.Headers.TryAddWithoutValidation("fp", snapshot.FingerPrint);

            if (!string.IsNullOrWhiteSpace(snapshot.Origin))
                request.Headers.TryAddWithoutValidation("origin", snapshot.Origin);

            if (!string.IsNullOrWhiteSpace(snapshot.Referer))
                request.Headers.TryAddWithoutValidation("referer", snapshot.Referer);

            if (!string.IsNullOrWhiteSpace(snapshot.UserAgent))
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

            // ===== Async Logging =====
            _ = LogQueue.Channel.Writer.WriteAsync(new RequestLog
            {
                Provider = OmsProvider.Smart.ToString(),
                Time = DateTime.Now,
                Url = snapshot.Url,
                RequestBody = snapshot.JsonBody,
                ResponseBody = responseText,
                StatusCode = (int)response.StatusCode
            });
        }
    }
}