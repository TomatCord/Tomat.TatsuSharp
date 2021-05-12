namespace Tomat.TatsuSharp.Utilities
{
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