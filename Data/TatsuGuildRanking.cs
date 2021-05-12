using System.Text.Json.Serialization;

namespace Tomat.TatsuSharp.Data
{
    /// <summary>
    ///     Class containing super-simple guild rank and score data for a user. <br />
    ///     Adapted from: https://github.com/tatsuworks/tatsu-api-go/blob/main/struct.go#L15
    /// </summary>
    public class TatsuGuildRanking
    {
        [JsonPropertyName("rank")] public long Rank { get; set; }

        [JsonPropertyName("score")] public ulong Score { get; set; }

        [JsonPropertyName("user_id")] public string UserID { get; set; }
    }
}