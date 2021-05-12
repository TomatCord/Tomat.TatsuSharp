using System.Net.Http;
using System.Threading.Tasks;

namespace Tomat.TatsuSharp.Utilities
{
    public static class TatsuRequestHelper
    {


        public static Task<HttpClient> SetHeaders(HttpClient client, string apiKey, string userAgent = "Tomat.TatsuSharp (Stevie, https://www.github.com/TomatCord/Tomat.TatsuSharp)")
        {
            client.DefaultRequestHeaders.Add("Authorization", apiKey);
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            return Task.FromResult(client);
        }
    }
}