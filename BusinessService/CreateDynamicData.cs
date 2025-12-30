//using BusinessService.Json;
//using Domain.Model;
//using System.Text;
//using System.Text.Json;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace BusinessService
//{
//    public static class CreateDynamicData
//    {
//        public static async Task SaveJson(string text, bool addItem, bool log)
//        {
//            string cookie = "";
//            string authorization = "";
//            string sessionId = "";
//            string stringContent = "";
//            string originUrl = "";

//            var lines = text.Split(Environment.NewLine);

//            foreach (var line in lines)
//            {
//                if (line.Contains("origin", StringComparison.OrdinalIgnoreCase))
//                    originUrl = ExtractOriginHeader(line);

//                if (line.Contains("authorization", StringComparison.OrdinalIgnoreCase))
//                    authorization = ExtractHeader(line);

//                if (line.Contains("cookie", StringComparison.OrdinalIgnoreCase))
//                    cookie = ExtractHeader(line);

//                if (line.Contains("x-sessionId", StringComparison.OrdinalIgnoreCase))
//                    sessionId = ExtractHeader(line);

//                if (line.Contains("StringContent"))
//                    stringContent = ExtractJson(line);

//                if (line.Contains("--data-raw"))
//                    stringContent = ExtractJson(line);

//            }

//            var newOrderData = new OrderData
//            {
//                OriginUrl = originUrl,
//                Cookie = cookie,
//                Authorization = authorization,
//                SessionId = sessionId,
//                stringContent = stringContent,
//                Log = log
//            };

//            var orderDatas = addItem
//                ? JsonConvertor.ReadJsonDataCollection<OrderData>("OrderData").ToList()
//                : new List<OrderData>();

//            //await SendOrderTest();

//            orderDatas.Insert(0, newOrderData);

//            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
//            //JsonConvertor.WriteJsonData(orderDatas, "OrderData", jsonFolderPath);
//        }


//        private static string ExtractOriginHeader(string line)
//        {
//            var match = Regex.Match(line, @"https?://[^\s'\""]+");
//            return match.Success ? match.Value : "";
//        }

//        private static string ExtractHeader(string line)
//        {
//            var parts = line.Split(':', 2);
//            if (parts.Length < 2) return "";

//            return parts[1]
//                .Trim()                // فاصله‌ها
//                .Trim('"')             // کوتیشن "
//                .Trim('\'')            // کوتیشن '
//                .Trim('\\');           // بک‌اسلش \
//        }

//        private static string ExtractJson(string line)
//        {
//            var first = line.IndexOf('{');
//            var last = line.LastIndexOf('}');
//            if (first < 0 || last < 0) return "";
//            return line.Substring(first, last - first + 1);
//        }
//    }
//}
