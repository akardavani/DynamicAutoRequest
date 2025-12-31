using Domain.Enum;

namespace BusinessService.SendRequest
{
    public static class SendHttpRequest
    {
        public static async Task SendAsync(TimeSpan delay, OmsProvider provider)
        {
            var request = OmsRequestFactory.Create(provider);
            await request.SendAsync(delay);
        }
    }
}
