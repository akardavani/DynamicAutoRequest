namespace BusinessService.SendRequest
{
    public interface IOmsRequest
    {
        Task SendAsync(TimeSpan delay);
    }
}
