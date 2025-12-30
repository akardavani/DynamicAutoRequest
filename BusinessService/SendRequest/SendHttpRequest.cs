using Domain.Enum;
using Domain.Model;

namespace BusinessService.SendRequest
{
    public static class SendHttpRequest
    {
        public static async Task<(string text, LogJson log)> Send(TimeSpan delay, int omsProvider, OrderData orderData)
        {
            return omsProvider switch
            {
                (int)OmsProvider.Sahra => await new SahraRequest().Send(delay, orderData),
                (int)OmsProvider.Tadbir => await new TadbirRequest().Send(delay, orderData),
                (int)OmsProvider.Rabin => await new RabinRequest().Send(delay, orderData),
                (int)OmsProvider.Armanx => await new ArmanxRequest().Send(delay, orderData),
                (int)OmsProvider.EasyTrader => await new EasyTraderRequest().Send(delay),
                _ => ("", null)
            };
        }
    }

}
