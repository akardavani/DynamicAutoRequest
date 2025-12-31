namespace Domain.Model
{
    public class SmartOrderRequestSnapshot
    {
        public string Url { get; set; } = default!;
        public string JsonBody { get; set; } = default!;

        public string? Authorization { get; set; }
        public string? FingerPrint { get; set; }

        public string? Origin { get; set; }
        public string? Referer { get; set; }
        public string? UserAgent { get; set; }
        public string? Cookie { get; set; }

        // ⭐ هدرهای عمومی (accept, accept-language, ...)
        public Dictionary<string, string> Headers { get; set; } = new();
    }
}
