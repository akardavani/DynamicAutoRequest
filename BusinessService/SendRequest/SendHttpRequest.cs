using Domain;

namespace BusinessService.SendRequest
{
    public static class SendHttpRequest
    {
        public static async Task<HttpResponseMessage> SendAsync(TimeSpan delay, OmsProvider provider)
        {
            var request = OmsRequestFactory.Create(provider);
            var response = await request.SendAsync(delay);
            return response;
        }
    }
}
