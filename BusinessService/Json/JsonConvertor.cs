using System.Text.Encodings.Web;
using System.Text.Json;

namespace BusinessService.Json
{
    public static class JsonConvertor
    {
        //public static T ReadJsonData<T>(string fileName)
        //{
        //    var currentDirectory = Environment.CurrentDirectory;

        //    var filePath = Path.Combine(currentDirectory, $"{fileName}.json");
        //    if (File.Exists(filePath))
        //    {
        //        var content = File.ReadAllText(filePath);

        //        var ss = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions()
        //        {
        //            ReadCommentHandling = JsonCommentHandling.Skip
        //        });

        //        return ss;
        //    }

        //    return default(T);
        //}


        public static T ReadJsonData<T>(string fileName, string? directoryPath = null)
        {
            // اگر مسیر داده نشده بود، از CurrentDirectory استفاده کن
            var basePath = string.IsNullOrWhiteSpace(directoryPath)
                ? Environment.CurrentDirectory
                : directoryPath;

            var filePath = Path.Combine(basePath, $"{fileName}.json");

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);

                var ss = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions()
                {
                    ReadCommentHandling = JsonCommentHandling.Skip
                });

                return ss;
            }

            return default(T);
        }



        public static ICollection<T> ReadJsonDataCollection<T>(string fileName)
        {
            var currentDirectory = Environment.CurrentDirectory;

            var filePath = Path.Combine(currentDirectory, $"{fileName}.json");
            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<ICollection<T>>(content, new JsonSerializerOptions()
                {
                    ReadCommentHandling = JsonCommentHandling.Skip
                });
            }

            return null;
        }

        public static ICollection<T> ReadJsonDataCollection<T>(string fileName, string path)
        {
            var filePath = Path.Combine(path, $"{fileName}.json");
            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<ICollection<T>>(content, new JsonSerializerOptions()
                {
                    ReadCommentHandling = JsonCommentHandling.Skip
                });
            }

            return null;
        }

        public static void WriteJsonDataList<T>(List<T> data, string fileName, string path)
        {
            var filePath = Path.Combine(path, $"{fileName}.json");

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            File.WriteAllText(filePath, json);
        }
        public static void WriteJsonData<T>(T data, string fileName, string path)
        {
            var filePath = Path.Combine(path, $"{fileName}.json");

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            File.WriteAllText(filePath, json);
        }

        public static ICollection<T> ReadJsonByString<T>(string content)
        {
            return JsonSerializer.Deserialize<ICollection<T>>(content, new JsonSerializerOptions()
            {
                ReadCommentHandling = JsonCommentHandling.Skip
            });
        }

        public static T ReadJsonDataByContent<T>(string content)
        {
            var ss = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions()
            {
                ReadCommentHandling = JsonCommentHandling.Skip
            });

            return ss;
        }
    }
}
