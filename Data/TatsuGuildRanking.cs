using System.Text.Json.Serialization;

namespace Tomat.TatsuSharp.Data
{
    public class TatsuGuildRanking
    {
        [JsonPropertyName("rank")] public long Rank { get; set; }

        [JsonPropertyName("score")] public ulong Score { get; set; }

        [JsonPropertyName("user_id")] public string UserID { get; set; }
    }
}