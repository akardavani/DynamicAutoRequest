namespace Infrastructure
{
    public class RequestLog
    {
        public string Provider { get; set; }
        public DateTime Time { get; set; }
        public string Url { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public int StatusCode { get; set; }
    }
}
