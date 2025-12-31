using Infrastructure;
using Domain.Model;
using System.Text.RegularExpressions;

namespace BusinessService
{
    public  class RabinSaveData : IBaseSaveData
    {
        public  void SaveJson(string text)
        {
            var snapshot = ParseCurlToSnapshot(text);

            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            JsonConvertor.WriteJsonData(snapshot, JsonFileNames.RabinOrderRequestSnapshot, jsonFolderPath);
        }

        public  RabinOrderRequestSnapshot ParseCurlToSnapshot(string curlText)
        {
            string? Extract(string pattern)
            {
                var m = Regex.Match(
                    curlText,
                    pattern,
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );

                return m.Success ? m.Groups[1].Value.Trim() : null;
            }

            string? ExtractHeader(string headerName)
            {
                return Extract($@"-H\s+'{Regex.Escape(headerName)}:\s*([^']+)'");
            }

            return new RabinOrderRequestSnapshot
            {
                // ===== URL =====
                Url = Extract(@"curl\s+'([^']+)'")
                      ?? throw new InvalidOperationException("URL not found in curl"),

                // ===== Headers =====
                Authorization = ExtractHeader("authorization")
                                ?? throw new InvalidOperationException("Authorization header not found"),

                FingerPrint = ExtractHeader("fp")
                              ?? throw new InvalidOperationException("fp header not found"),

                Origin = ExtractHeader("origin") ?? string.Empty,
                Referer = ExtractHeader("referer") ?? string.Empty,
                UserAgent = ExtractHeader("user-agent") ?? string.Empty,
                Cookie = ExtractHeader("cookie"),

                // ===== Body =====
                JsonBody = Extract(@"--data-raw\s+'([\s\S]+?)'")
                           ?? throw new InvalidOperationException("JSON body not found")
            };
        }
    }
}
