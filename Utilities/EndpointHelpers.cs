namespace Tomat.TatsuSharp.Utilities
{
    /// <summary>
    ///     Utility class for getting URLs for the Tatsu endpoint. <br />
    ///     Uses API v1. <br />
    ///     Adapted from: https://github.com/tatsuworks/tatsu-api-go/blob/main/endpoint.go
    /// </summary>
    public static class EndpointHelpers
    {
        public const string BaseURL = "https://api.tatsu.gg/v1";

        public static string GetAllTimeGuildMemberRanking(string guildID, string userID) =>
            $"{BaseURL}/guilds/{guildID}/rankings/members/{userID}/all";

        public static string GetAllTimeGuildRankings(string guildID, ulong offset) =>
            $"{BaseURL}/guilds/{guildID}/rankings/all?offset={offset}";

        public static string GetUserProfile(string userID) =>
            $"{BaseURL}/users/{userID}/profile";
    }
}