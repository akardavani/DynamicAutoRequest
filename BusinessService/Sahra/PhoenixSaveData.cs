using Domain;
using Infrastructure;
using System.Text.RegularExpressions;

namespace BusinessService
{
    public class PhoenixSaveData : IBaseSaveData
    {
        public void SaveJson(string curlText)
        {
            var snapshot = ParseCurlToSnapshot(curlText);

            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            JsonConvertor.WriteJsonData(
                snapshot,
                JsonFileNames.PhoenixOrderRequestSnapshot,
                jsonFolderPath
            );
        }

        public PhoenixOrderRequestSnapshot ParseCurlToSnapshot(string curlText)
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

            var snapshot = new PhoenixOrderRequestSnapshot
            {
                // ===== URL =====
                Url = Extract(@"curl\s+'([^']+)'")
                      ?? throw new InvalidOperationException("URL not found"),

                // ===== Common =====
                Referer = ExtractHeader("Referer"),
                UserAgent = ExtractHeader("User-Agent"),

                // ===== Body =====
                JsonBody = Extract(@"--data-raw\s+'([\s\S]+?)'")
                           ?? throw new InvalidOperationException("JSON body not found"),

                // ===== Broker specific =====
                SessionId = ExtractHeader("x-sessionId")
            };

            // ===== Generic Headers =====
            void Add(string name)
            {
                var value = ExtractHeader(name);
                if (!string.IsNullOrWhiteSpace(value))
                    snapshot.Headers[name] = value;
            }

            Add("Accept");
            Add("Content-Type");
            Add("sec-ch-ua");
            Add("sec-ch-ua-mobile");
            Add("sec-ch-ua-platform");

            return snapshot;
        }
    }
}
