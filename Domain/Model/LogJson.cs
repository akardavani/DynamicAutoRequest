namespace Domain.Model
{
    public class LogJson
    {
        public int BatchNumber { get; set; }
        public string Isin { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public int StartDefferent { get; set; }
        public int EndDefferent { get; set; }
        public int ElapsedTime { get; set; }
        public string Response { get; set; }
    }
}
