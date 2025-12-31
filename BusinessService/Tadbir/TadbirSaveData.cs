using Domain.Model;
using Infrastructure;
using System.Text.RegularExpressions;

namespace BusinessService
{
    public class TadbirSaveData : IBaseSaveData
    {
        public void SaveJson(string curlText)
        {
            var snapshot = ParseCurlToSnapshot(curlText);

            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            JsonConvertor.WriteJsonData(
                snapshot,
                JsonFileNames.TadbirOrderRequestSnapshot,
                jsonFolderPath
            );
        }

        public TadbirOrderRequestSnapshot ParseCurlToSnapshot(string curlText)
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

            return new TadbirOrderRequestSnapshot
            {
                // ===== URL =====
                Url = Extract(@"curl\s+'([^']+)'")
                      ?? throw new InvalidOperationException("URL not found"),

                // ===== Headers =====
                Accept = ExtractHeader("Accept"),
                AcceptLanguage = ExtractHeader("Accept-Language"),
                Origin = ExtractHeader("Origin"),
                Referer = ExtractHeader("Referer"),
                UserAgent = ExtractHeader("User-Agent"),
                XRequestedWith = ExtractHeader("X-Requested-With"),

                // Cookie از -b
                Cookie = Extract(@"-b\s+'([^']+)'"),

                // ===== Body =====
                JsonBody = Extract(@"--data-raw\s+'([\s\S]+?)'")
                           ?? throw new InvalidOperationException("JSON body not found")
            };
        }
    }
}
