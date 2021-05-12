using System.Text.Json.Serialization;

namespace Tomat.TatsuSharp.Data
{
    public class TatsuGuildMemberRanking
    {
        [JsonPropertyName("guild_id")] public string GuildID { get; set; }

        [JsonPropertyName("rank")] public long Rank { get; set; }

        [JsonPropertyName("score")] public ulong Score { get; set; }

        [JsonPropertyName("user_id")] public string UserID { get; set; }
    }
}