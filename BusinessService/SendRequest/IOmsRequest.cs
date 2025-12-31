namespace BusinessService.SendRequest
{
    public interface IOmsRequest
    {
        Task<HttpResponseMessage> SendAsync(TimeSpan delay);
    }
}
