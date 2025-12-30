namespace Domain.Model
{
    public class OrderData
    {
        public string OriginUrl { get; set; }
        public string Cookie { get; set; }
        public string Authorization { get; set; }
        public string SessionId { get; set; }
        public bool Log { get; set; }
        public string stringContent { get; set; }
        //public SahraRequest SahraRequest { get; set; }
    }
}
