using System.Net.Http;
using System.Threading.Tasks;

namespace Tomat.TatsuSharp.Utilities
{
    /// <summary>
    ///     Utility class. Currently provides a method for easily setting request headers. <br />
    ///     Adapted from "setHeaders" in https://github.com/tatsuworks/tatsu-api-go/blob/main/rest_client.go
    /// </summary>
    public static class TatsuRequestHelper
    {
        /// <summary>
        ///     Sets the authorization and user-agent headers of the provided client.
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> to modify.</param>
        /// <param name="apiKey">The API key to use.</param>
        /// <param name="userAgent">The user agent, defaults to Tomat.TatsuSharp, can seemingly be whatever you want.</param>
        public static async Task<HttpClient> SetHeaders(HttpClient client, string apiKey, string userAgent = "Tomat.TatsuSharp (Stevie, https://www.github.com/TomatCord/Tomat.TatsuSharp)")
        {
            client.DefaultRequestHeaders.Add("Authorization", apiKey);
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            return await Task.FromResult(client);
        }
    }
}