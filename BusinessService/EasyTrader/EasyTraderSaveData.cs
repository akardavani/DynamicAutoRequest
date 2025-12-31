using Domain.Model;
using Infrastructure;
using System.Text.RegularExpressions;

namespace BusinessService
{
    public  class EasyTraderSaveData : IBaseSaveData
    {
        public  void SaveJson(string text)
        {
            var snapshot = ParseCurlToSnapshot(text);

            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            JsonConvertor.WriteJsonData(snapshot, JsonFileNames.EasyTraderOrderRequestSnapshot, jsonFolderPath);
        }

        public  EasyTraderOrderRequestSnapshot ParseCurlToSnapshot(string curlText)
        {
            string Extract(string pattern)
            {
                var m = Regex.Match(curlText, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                return m.Success ? m.Groups[1].Value.Trim() : null;
            }

            string ExtractHeader(string headerName)
            {
                return Extract($@"-H\s+'{headerName}:\s*([^']+)'");
            }

            return new EasyTraderOrderRequestSnapshot
            {
                // URL
                Url = Extract(@"curl\s+'([^']+)'"),

                // Headers مهم
                Authorization = ExtractHeader("authorization"),
                Origin = ExtractHeader("origin"),
                Referer = ExtractHeader("referer"),
                UserAgent = ExtractHeader("user-agent"),
                Cookie = ExtractHeader("cookie"),

                // Body
                JsonBody = Extract(@"--data-raw\s+'(.+)'")
            };
        }
    }
}
