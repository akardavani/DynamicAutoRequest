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

    //public class SahraRequest
    //{
    //    public int validity { get; set; }
    //    public object validityDate { get; set; }
    //    public long price { get; set; }
    //    public long volume { get; set; }
    //    public int side { get; set; }
    //    public string isin { get; set; }
    //    public int accountType { get; set; }
    //}
}
