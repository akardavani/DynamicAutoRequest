using Domain.Model;
using Services;
using System.Text;

namespace BusinessService.SendRequest
{
    public abstract class BaseRequest
    {
        protected HttpClient Client { get; } = new HttpClient();

        protected HttpRequestMessage CreateRequest(string url, string content, Dictionary<string, string> headers)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return request;
        }

        protected async Task<(string text, LogJson log)> SendRequest(HttpRequestMessage request, TimeSpan delay, string payload)
        {
            var startTime = DateTime.Now + delay;
            try
            {
                var response = await Client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseText = await response.Content.ReadAsStringAsync();

                var log = Logging.Log(delay, payload, startTime, responseText);
                return log;
            }
            catch (Exception ex)
            {
                var time = DateTime.Now + delay;
                await Logging.WriteToFileAsync($"{Environment.NewLine}{time} - {ex.Message}", "log");
                return ("", null);
            }
        }
    }

}
