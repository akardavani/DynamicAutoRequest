namespace Domain.Model
{
    public class RequestTimeData
    {
        /// <summary>
        /// انتخاب oms
        /// </summary>
        public int OmsProvider { get; set; }
        public int BatchSize { get; set; }
        public int TotalRequests { get; set; }
        public int Delay { get; set; }
        public string StartRequestTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Log { get; set; }        
    }
}
