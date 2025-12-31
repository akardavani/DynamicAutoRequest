using BusinessService.SendRequest;
using Domain;

namespace BusinessService
{
    public static class BaseSaveData
    {
        public static void SaveData(int omsProvider, string text)
        {
            var request = OmsSaveDataFactory.Create((OmsProvider)omsProvider);
            request.SaveJson(text);
        }
    }
}
