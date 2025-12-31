using Domain.Enum;

namespace BusinessService.SendRequest
{
    public static class OmsRequestFactory
    {
        private static readonly Dictionary<OmsProvider, Func<object>> _map =
            new()
            {
            // { OmsProvider.Sahra, () => new SahraRequest() },
            // { OmsProvider.Tadbir, () => new TadbirRequest() },
            // { OmsProvider.Armanx, () => new ArmanxRequest() },
            { OmsProvider.Smart, () => new SmartRequest() },
            { OmsProvider.Rabin, () => new RabinRequest() },
            { OmsProvider.EasyTrader, () => new EasyTraderRequest() },
            };

        public static IOmsRequest Create(OmsProvider provider)
            => _map.TryGetValue(provider, out var factory)
                ? (IOmsRequest)factory()
                : throw new NotSupportedException($"OMS Provider '{provider}' is not supported");
    }

    public static class SendHttpRequest
    {
        public static async Task SendAsync(TimeSpan delay, OmsProvider provider)
        {
            var request = OmsRequestFactory.Create(provider);
            await request.SendAsync(delay);
        }
    }

    public interface IOmsRequest
    {
        Task SendAsync(TimeSpan delay);
    }
}
