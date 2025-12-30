namespace Services
{
    public static class ServiceProviderExtensions
    {
        public static async Task<TimeSpan> SyncTime(CancellationToken cancellation = default)
        {
            try
            {
                var client = new HttpClient();
                var t0 = DateTime.Now;
                var response = await client.GetAsync("https://qcore.mobinsb.ir/ntp", cancellation);
                var t3 = DateTime.Now;
                response.EnsureSuccessStatusCode();

                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                var ticks = long.Parse(response.Headers.GetValues("X-SERVER-TIME").FirstOrDefault());
                var t1 = new DateTime(epoch.Ticks + (DateTime.Now.Ticks - DateTime.UtcNow.Ticks) + (ticks * 10_000), DateTimeKind.Utc);
                var _delay = t1 - t3 + (t3 - t0);

                return _delay;
                //_timer = new Timer(callback: OnTick, null, 0, 999);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
