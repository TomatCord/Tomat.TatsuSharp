﻿using System.Text.Json.Serialization;

namespace Tomat.TatsuSharp.Data
{
    public class TatsuUser
    {
        [JsonPropertyName("avatar_url")] public string AvatarURL { get; set; }

        [JsonPropertyName("credits")] public ulong Credits { get; set; }

        [JsonPropertyName("discriminator")] public string Discriminator { get; set; }

        [JsonPropertyName("id")] public string ID { get; set; }

        [JsonPropertyName("info_box")] public string InfoBox { get; set; }

        [JsonPropertyName("reputation")] public ulong Reputation { get; set; }

        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("tokens")] public ulong Tokens { get; set; }

        [JsonPropertyName("username")] public string Username { get; set; }

        [JsonPropertyName("xp")] public ulong XP { get; set; }
    }
}