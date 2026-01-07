namespace Domain
{
    public class TadbirOrderRequestSnapshot : BaseOrderRequestSnapshot
    {
        // Headers
        public string Accept { get; set; }
        public string AcceptLanguage { get; set; }
        public string XRequestedWith { get; set; }
    }
}
