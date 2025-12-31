namespace Domain.Model
{
    public class EasyTraderOrderRequestSnapshot
    {
        public string Url { get; set; }
        public string Authorization { get; set; }
        public string Origin { get; set; }
        public string Referer { get; set; }
        public string UserAgent { get; set; }
        public string? Cookie { get; set; }
        public string JsonBody { get; set; }
    }

}
