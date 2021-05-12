namespace Tomat.TatsuSharp.Utilities
{
    /// <summary>
    ///     Utility class for getting URLs for the Tatsu endpoint. <br />
    ///     Uses API v1. <br />
    ///     Adapted from: https://github.com/tatsuworks/tatsu-api-go/blob/main/endpoint.go
    /// </summary>
    public static class EndpointHelpers
    {
        /// <summary>
        ///     The base endpoint URL to use. Currently API v1.
        /// </summary>
        public const string BaseURL = "https://api.tatsu.gg/v1";

        /// <summary>
        ///     Get an endpoint URL using the given parameters.
        /// </summary>
        public static string GetAllTimeGuildMemberRanking(string guildID, string userID) =>
            $"{BaseURL}/guilds/{guildID}/rankings/members/{userID}/all";

        /// <summary>
        ///     Get an endpoint URL using the given parameters.
        /// </summary>
        public static string GetAllTimeGuildRankings(string guildID, ulong offset) =>
            $"{BaseURL}/guilds/{guildID}/rankings/all?offset={offset}";

        /// <summary>
        ///     Get an endpoint URL using the given parameters.
        /// </summary>
        public static string GetUserProfile(string userID) =>
            $"{BaseURL}/users/{userID}/profile";
    }
}