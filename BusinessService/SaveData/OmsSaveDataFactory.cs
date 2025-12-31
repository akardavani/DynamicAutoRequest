using Domain;

namespace BusinessService.SendRequest
{
    public static class OmsSaveDataFactory
    {
        private static readonly Dictionary<OmsProvider, Func<object>> _map =
            new()
            {
            // { OmsProvider.Sahra, () => new SahraRequest() },            
            // { OmsProvider.Armanx, () => new ArmanxRequest() },
            { OmsProvider.Tadbir, () => new TadbirSaveData() },
            { OmsProvider.Smart, () => new SmartSaveData() },
            { OmsProvider.Rabin, () => new RabinSaveData() },
            { OmsProvider.EasyTrader, () => new EasyTraderSaveData() },
            };

        public static IBaseSaveData Create(OmsProvider provider)
            => _map.TryGetValue(provider, out var factory)
                ? (IBaseSaveData)factory()
                : throw new NotSupportedException($"OMS Provider '{provider}' is not supported");
    }
}
