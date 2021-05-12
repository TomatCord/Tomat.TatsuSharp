using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tomat.TatsuSharp.Data;
using Tomat.TatsuSharp.Utilities;

namespace Tomat.TatsuSharp
{
    public class TatsuClient
    {
        public string APIKey { get; }

        public RequestBucket Bucket { get; }

        public HttpClient Client { get; private set; }

        public TatsuClient(string apiKey)
        {
            APIKey = apiKey;
            Bucket = new RequestBucket(60, 60,
                TimeSpan.FromMinutes(1),
                DateTime.Now.Add(TimeSpan.FromMinutes(1)));
            Client = new HttpClient();
        }

        public async Task<TatsuGuildMemberRanking> GetAllTimeGuildMemberRanking(string guildID, string userID) =>
            await Get<TatsuGuildMemberRanking>(EndpointHelpers.GetAllTimeGuildMemberRanking(guildID, userID));

        public async Task<TatsuGuildRankings> GetAllTimeGuildRankings(string guildID, ulong offset) =>
            await Get<TatsuGuildRankings>(EndpointHelpers.GetAllTimeGuildRankings(guildID, offset));

        public async Task<TatsuUser> GetUserProfile(string userID) =>
            await Get<TatsuUser>(EndpointHelpers.GetUserProfile(userID));

        private async Task<TType> Get<TType>(string endpoint) where TType : class
        {
            await Bucket.Acquire();
            Client = await TatsuRequestHelper.SetHeaders(Client, APIKey);

            using HttpResponseMessage response = await Client.GetAsync(endpoint);
            await Bucket.ParseHeaders(response.Headers);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("r u ok");

            return await Task.FromResult(JsonSerializer.Deserialize<TType>(await response.Content.ReadAsStringAsync()));
        }
    }
}