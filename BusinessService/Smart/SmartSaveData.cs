using Infrastructure;
using Domain.Model;
using System.Text.RegularExpressions;

namespace BusinessService
{
    public static class SmartSaveData
    {
        public static void SaveJson(string text)
        {
            var snapshot = ParseCurlToSnapshot(text);

            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            JsonConvertor.WriteJsonData(snapshot, JsonFileNames.SmartOrderRequestSnapshot, jsonFolderPath);
        }

        public static SmartOrderRequestSnapshot ParseCurlToSnapshot(string curlText)
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
                => Extract($@"-H\s+'{Regex.Escape(headerName)}:\s*([^']+)'");

            string? ExtractCookie()
                => Extract(@"-b\s+'([^']+)'");

            string? ExtractAuthFromCookie(string? cookie)
            {
                if (string.IsNullOrWhiteSpace(cookie))
                    return null;

                var m = Regex.Match(
                    cookie,
                    @"Authorization=Bearer%20([^;]+)",
                    RegexOptions.IgnoreCase
                );

                return m.Success ? "Bearer " + Uri.UnescapeDataString(m.Groups[1].Value) : null;
            }

            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            void TryAdd(string name)
            {
                var v = ExtractHeader(name);
                if (!string.IsNullOrWhiteSpace(v))
                    headers[name] = v;
            }

            // ===== Common headers =====
            TryAdd("Accept");
            TryAdd("Accept-Language");
            TryAdd("Content-Type");
            TryAdd("Connection");
            TryAdd("Origin");
            TryAdd("Referer");
            TryAdd("User-Agent");
            TryAdd("Sec-Fetch-Dest");
            TryAdd("Sec-Fetch-Mode");
            TryAdd("Sec-Fetch-Site");
            TryAdd("sec-ch-ua");
            TryAdd("sec-ch-ua-mobile");
            TryAdd("sec-ch-ua-platform");

            var cookie = ExtractCookie();
            var authorization =
                ExtractHeader("authorization")
                ?? ExtractAuthFromCookie(cookie);

            return new SmartOrderRequestSnapshot
            {
                Url = Extract(@"curl\s+'([^']+)'")
                      ?? throw new InvalidOperationException("URL not found in curl"),

                Authorization = authorization,   // ممکنه null باشه
                FingerPrint = ExtractHeader("fp"), // اینجا نداریم → null

                Origin = ExtractHeader("Origin"),
                Referer = ExtractHeader("Referer"),
                UserAgent = ExtractHeader("User-Agent"),
                Cookie = cookie,

                Headers = headers,

                JsonBody = Extract(@"--data-raw\s+'([\s\S]+?)'")
                           ?? throw new InvalidOperationException("JSON body not found")
            };
        }


        //public static SmartOrderRequestSnapshot ParseCurlToSnapshot(string curlText)
        //{
        //    string? Extract(string pattern)
        //    {
        //        var m = Regex.Match(
        //            curlText,
        //            pattern,
        //            RegexOptions.IgnoreCase | RegexOptions.Singleline
        //        );

        //        return m.Success ? m.Groups[1].Value.Trim() : null;
        //    }

        //    string? ExtractHeader(string headerName)
        //    {
        //        return Extract($@"-H\s+'{Regex.Escape(headerName)}:\s*([^']+)'");
        //    }

        //    return new SmartOrderRequestSnapshot
        //    {
        //        // ===== URL =====
        //        Url = Extract(@"curl\s+'([^']+)'")
        //              ?? throw new InvalidOperationException("URL not found in curl"),

        //        // ===== Headers =====
        //        Authorization = ExtractHeader("authorization")
        //                        ?? throw new InvalidOperationException("Authorization header not found"),

        //        FingerPrint = ExtractHeader("fp")
        //                      ?? throw new InvalidOperationException("fp header not found"),

        //        Origin = ExtractHeader("origin") ?? string.Empty,
        //        Referer = ExtractHeader("referer") ?? string.Empty,
        //        UserAgent = ExtractHeader("user-agent") ?? string.Empty,
        //        Cookie = ExtractHeader("cookie"),

        //        // ===== Body =====
        //        JsonBody = Extract(@"--data-raw\s+'([\s\S]+?)'")
        //                   ?? throw new InvalidOperationException("JSON body not found")
        //    };
        //}

    }
}
