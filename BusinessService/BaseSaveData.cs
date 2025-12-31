using Domain.Enum;

namespace BusinessService
{
    public static class BaseSaveData
    {
        public static void SaveData(int omsProvider, string text)
        {
            switch (omsProvider)
            {
                //case (int)OmsProvider.Sahra:
                //    new SahraRequest().Send(delay, orderData);
                //    break;

                //case (int)OmsProvider.Tadbir:
                //    new TadbirRequest().Send(delay, orderData);
                //    break;

                //case (int)OmsProvider.Armanx:
                //    new ArmanxRequest().Send(delay, orderData);
                //    break;

                case (int)OmsProvider.Smart:
                    SmartSaveData.SaveJson(text);
                    break;

                case (int)OmsProvider.Rabin:
                    RabinSaveData.SaveJson(text);
                    break;

                case (int)OmsProvider.EasyTrader:
                    EasyTraderSaveData.SaveJson(text);
                    break;

                default:
                    // اگر هیچ کدوم نبود، کاری انجام نده یا لاگ بزن
                    break;
            }
        }

    }
}
