using System.Text.Json.Serialization;

namespace Tomat.TatsuSharp.Data
{
    /// <summary>
    ///     Class containing super-simple info regarding ranks and scores with an associated user and guild ID. <br />
    ///     Adapted from: https://github.com/tatsuworks/tatsu-api-go/blob/main/struct.go#L3
    /// </summary>
    public class TatsuGuildMemberRanking
    {
        [JsonPropertyName("guild_id")] public string GuildID { get; set; }

        [JsonPropertyName("rank")] public long Rank { get; set; }

        [JsonPropertyName("score")] public ulong Score { get; set; }

        [JsonPropertyName("user_id")] public string UserID { get; set; }
    }
}