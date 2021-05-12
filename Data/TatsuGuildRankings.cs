using System.Text.Json.Serialization;

namespace Tomat.TatsuSharp.Data
{
    public class TatsuGuildRankings
    {
        [JsonPropertyName("guild_id")] public string GuildID { get; set; }

        [JsonPropertyName("rankings")] public TatsuGuildRanking[] Rankings { get; set; }
    }
}