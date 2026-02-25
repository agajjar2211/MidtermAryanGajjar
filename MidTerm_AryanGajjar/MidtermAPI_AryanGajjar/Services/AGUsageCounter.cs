namespace MidtermAPI_AryanGajjar.Services
{
    public class AGUsageCounter
    {
        
            private readonly Dictionary<string, int> RequestCounts = new();

            public int Increment(string apiKey)
            {
                if (string.IsNullOrWhiteSpace(apiKey))
                    apiKey = "UNKNOWN";

                if (!RequestCounts.ContainsKey(apiKey))
                    RequestCounts[apiKey] = 0;

                RequestCounts[apiKey]++;

                return RequestCounts[apiKey];
            }
        
    }
}
