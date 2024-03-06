namespace BlazorApp;

public class SubmitLimiter
{
    private Dictionary<string, DateTime> ips = new();

    private Task? _loop;

    public void AddLimit(string ip)
    {
        ips.Add(ip, DateTime.UtcNow.AddSeconds(3));

        if (_loop is null)
        {
            _loop = Task.Run(ClearLimitLoop);
        }
    }

    public bool IsIpLimited(string ip)
    {
        return ips.ContainsKey(ip);
    }


    public async Task ClearLimitLoop()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            var expiredLimits = ips.Where(x => (x.Value < DateTime.UtcNow));
            foreach (var expired in expiredLimits)
            {
                ips.Remove(expired.Key);
                Console.WriteLine($"Cleared IP limit: {expired.Key}");
            }
        }
    }
}