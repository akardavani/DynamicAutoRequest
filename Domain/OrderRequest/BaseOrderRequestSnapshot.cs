namespace Domain.Model
{
    public abstract class BaseOrderRequestSnapshot
    {
        public string Url { get; set; } = default!;
        public string JsonBody { get; set; } = default!;
        public string? Authorization { get; set; }
        public string? Origin { get; set; }
        public string? Referer { get; set; }
        public string? UserAgent { get; set; }
        public string? Cookie { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new();
    }

}
