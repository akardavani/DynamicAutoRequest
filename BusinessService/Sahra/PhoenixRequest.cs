using Domain;
using Infrastructure;
using System.Text;

namespace BusinessService.SendRequest
{
    public class PhoenixRequest : IOmsRequest
    {
        private readonly PhoenixOrderRequestSnapshot snapshot;

        public PhoenixRequest()
        {
            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            snapshot = JsonConvertor.ReadJsonData<PhoenixOrderRequestSnapshot>(
                JsonFileNames.PhoenixOrderRequestSnapshot,
                jsonFolderPath
            );
        }

        public async Task<HttpResponseMessage> SendAsync(TimeSpan delay)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, snapshot.Url);

            // ===== Generic Headers =====
            foreach (var header in snapshot.Headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            // ===== Common =====
            if (!string.IsNullOrWhiteSpace(snapshot.Referer))
                request.Headers.TryAddWithoutValidation("Referer", snapshot.Referer);

            if (!string.IsNullOrWhiteSpace(snapshot.UserAgent))
                request.Headers.TryAddWithoutValidation("User-Agent", snapshot.UserAgent);

            // ===== Broker specific =====
            if (!string.IsNullOrWhiteSpace(snapshot.SessionId))
                request.Headers.TryAddWithoutValidation("x-sessionId", snapshot.SessionId);

            // ===== Body =====
            request.Content = new StringContent(
                snapshot.JsonBody,
                Encoding.UTF8,
                "application/json"
            );

            // ===== Send =====
            var response = await client.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            _ = LogQueue.Channel.Writer.WriteAsync(new RequestLog
            {
                Provider = OmsProvider.Phoenix.ToString(),
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