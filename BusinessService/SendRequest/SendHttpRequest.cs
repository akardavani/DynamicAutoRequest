using Domain.Enum;
using Domain.Model;

namespace BusinessService.SendRequest
{
    public static class SendHttpRequest
    {
        //public static async Task<(string text, LogJson log)> Send(TimeSpan delay, int omsProvider, OrderData orderData)
        //{
        //    return omsProvider switch
        //    {
        //        (int)OmsProvider.Sahra => await new SahraRequest().Send(delay, orderData),
        //        (int)OmsProvider.Tadbir => await new TadbirRequest().Send(delay, orderData),
        //        (int)OmsProvider.Rabin => await new RabinRequest().Send(delay, orderData),
        //        (int)OmsProvider.Armanx => await new ArmanxRequest().Send(delay, orderData),
        //        (int)OmsProvider.EasyTrader => await new EasyTraderRequest().Send(delay),
        //        _ => ("", null)
        //    };
        //}

        public static async Task Send(TimeSpan delay, int omsProvider)
        {
            var orderData = new OrderData();
            switch (omsProvider)
            {
                case (int)OmsProvider.Sahra:
                    await new SahraRequest().Send(delay, orderData);
                    break;

                case (int)OmsProvider.Tadbir:
                    await new TadbirRequest().Send(delay, orderData);
                    break;                

                case (int)OmsProvider.Armanx:
                    await new ArmanxRequest().Send(delay, orderData);
                    break;

                case (int)OmsProvider.Smart:
                    await new SmartRequest().Send(delay);
                    break;

                case (int)OmsProvider.Rabin:
                    await new RabinRequest().Send(delay);
                    break;

                case (int)OmsProvider.EasyTrader:
                    await new EasyTraderRequest().Send(delay);
                    break;

                default:
                    // اگر هیچ کدوم نبود، کاری انجام نده یا لاگ بزن
                    break;
            }
        }
    }

}
