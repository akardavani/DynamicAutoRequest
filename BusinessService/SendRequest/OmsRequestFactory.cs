using Domain;

namespace BusinessService.SendRequest
{
    public static class OmsRequestFactory
    {
        private static readonly Dictionary<OmsProvider, Func<object>> _map =
            new()
            {
            // { OmsProvider.Sahra, () => new SahraRequest() },            
            // { OmsProvider.Armanx, () => new ArmanxRequest() },
            { OmsProvider.Tadbir, () => new TadbirRequest() },
            { OmsProvider.Smart, () => new SmartRequest() },
            { OmsProvider.Rabin, () => new RabinRequest() },
            { OmsProvider.EasyTrader, () => new EasyTraderRequest() },
            { OmsProvider.Phoenix, () => new PhoenixRequest() },
            { OmsProvider.AsaTrader, () => new AsaTraderRequest() },
            };

        public static IOmsRequest Create(OmsProvider provider)
            => _map.TryGetValue(provider, out var factory)
                ? (IOmsRequest)factory()
                : throw new NotSupportedException($"OMS Provider '{provider}' is not supported");
    }
}
