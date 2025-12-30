using BusinessService.Json;
using Domain.Model;
using System.Net.Http.Headers;
using System.Text;

namespace BusinessService.SendRequest
{
    public class EasyTraderRequest : BaseRequest
    {
        public async Task<(string text, LogJson jsonLog)> Send(TimeSpan delay)
        {
            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            var snapshot = JsonConvertor.ReadJsonData<EasyTraderOrderRequestSnapshot>(JsonFileNames.EasyTraderOrderRequestSnapshot, jsonFolderPath);

            await ExecuteRequest(snapshot);

            return ("", null);
        }

        private async Task ExecuteRequest(EasyTraderOrderRequestSnapshot snapshot)
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

            //Console.WriteLine("Status: " + response.StatusCode);
            //Console.WriteLine(responseText);


            _ = LogQueue.Channel.Writer.WriteAsync(new RequestLog
            {
                Time = DateTime.Now,
                Url = snapshot.Url,
                RequestBody = snapshot.JsonBody,
                ResponseBody = responseText,
                StatusCode = (int)response.StatusCode
            });


        }
    }
}