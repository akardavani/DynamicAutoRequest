namespace Domain.Model
{
    public class RabinOrderRequestSnapshot
    {
        public string Url { get; set; } = default!;

        public string Authorization { get; set; } = default!; // Bearer xxx
        public string FingerPrint { get; set; } = default!;   // fp header

        public string Origin { get; set; } = default!;
        public string Referer { get; set; } = default!;
        public string UserAgent { get; set; } = default!;

        public string? Cookie { get; set; }

        public string JsonBody { get; set; } = default!;
    }

}
