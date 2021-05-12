using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tomat.TatsuSharp.Data;
using Tomat.TatsuSharp.Utilities;

namespace Tomat.TatsuSharp
{
    /// <summary>
    ///     Core client class. Provides numerous methods for obtaining data. <br />
    ///     Adapted from https://github.com/tatsuworks/tatsu-api-go/blob/main/rest_client.go and tid-bits of https://github.com/tatsuworks/tatsu-api-go/blob/main/client.go
    /// </summary>
    public class TatsuClient
    {
        /// <summary>
        ///     Your Tatsu API key.
        /// </summary>
        public string APIKey { get; }

        /// <summary>
        ///     Automatically-created <see cref="RequestBucket"/>.
        /// </summary>
        public RequestBucket Bucket { get; }

        /// <summary>
        ///     Automatically-created <see cref="HttpClient"/>. Used for web requests.
        /// </summary>
        public HttpClient Client { get; private set; }

        /// <summary>
        ///     Create a <see cref="TatsuClient"/> instance. The <paramref name="apiKey"/> is required.
        /// </summary>
        /// <param name="apiKey">The Tatsu API key you wish to use. Be sure to keep this private!</param>
        public TatsuClient(string apiKey)
        {
            APIKey = apiKey;
            Bucket = new RequestBucket(60, 60,
                TimeSpan.FromMinutes(1),
                DateTime.Now.Add(TimeSpan.FromMinutes(1)));
            Client = new HttpClient();
        }

        /// <summary>
        ///     Gets a <see cref="TatsuGuildMemberRanking"/> instance with the given <paramref name="guildID"/> and <paramref name="userID"/>.
        /// </summary>
        /// <param name="guildID">The guild you want to get rankings from.</param>
        /// <param name="userID">The ID of the user who you want to get the rankings of.</param>
        /// <exception cref="RateLimitException">Thrown if the Tatsu rate limit is exceeded.</exception>
        public async Task<TatsuGuildMemberRanking> GetAllTimeGuildMemberRanking(string guildID, string userID) =>
            await Get<TatsuGuildMemberRanking>(EndpointHelpers.GetAllTimeGuildMemberRanking(guildID, userID));

        /// <summary>
        ///     Gets a <see cref="TatsuGuildRankings"/> instance using the provided <paramref name="guildID"/>, and applies the provided <paramref name="offset"/>.
        /// </summary>
        /// <param name="guildID">The ID of the guild.</param>
        /// <param name="offset">The offset.</param>
        /// <exception cref="RateLimitException">Thrown if the Tatsu rate limit is exceeded.</exception>
        public async Task<TatsuGuildRankings> GetAllTimeGuildRankings(string guildID, ulong offset) =>
            await Get<TatsuGuildRankings>(EndpointHelpers.GetAllTimeGuildRankings(guildID, offset));

        /// <summary>
        ///     Gets a universal user profile (<see cref="TatsuUser"/>) used across the entirety of Tatsu.
        /// </summary>
        /// <param name="userID">The ID of the user you want to access.</param>
        /// <exception cref="RateLimitException">Thrown if the Tatsu rate limit is exceeded.</exception>
        public async Task<TatsuUser> GetUserProfile(string userID) =>
            await Get<TatsuUser>(EndpointHelpers.GetUserProfile(userID));

        private async Task<TType> Get<TType>(string endpoint) where TType : class
        {
            if (!await Bucket.Acquire())
                throw new RateLimitException();

            Client = await TatsuRequestHelper.SetHeaders(Client, APIKey);

            using HttpResponseMessage response = await Client.GetAsync(endpoint);
            await Bucket.ParseHeaders(response.Headers);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.StatusCode.ToString());

            return await Task.FromResult(JsonSerializer.Deserialize<TType>(await response.Content.ReadAsStringAsync()));
        }
    }
}