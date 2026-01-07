namespace Domain
{
    public class RabinOrderRequestSnapshot : BaseOrderRequestSnapshot
    {
        public string FingerPrint { get; set; } = default!;   // fp header
    }
}
