using System.Text.Json.Serialization;

namespace Tomat.TatsuSharp.Data
{
    /// <summary>
    ///     Class containing an array of <see cref="TatsuGuildRanking"/>s and an associated guild ID. <br />
    ///     Adapted from: https://github.com/tatsuworks/tatsu-api-go/blob/main/struct.go#L10
    /// </summary>
    public class TatsuGuildRankings
    {
        [JsonPropertyName("guild_id")] public string GuildID { get; set; }

        [JsonPropertyName("rankings")] public TatsuGuildRanking[] Rankings { get; set; }
    }
}