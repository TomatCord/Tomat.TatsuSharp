# Tomat.TatsuSharp
Tatsumaki wrapper in C# for .NET. Adapated from the official [Golang wrapper](https://github.com/tatsuworks/tatsu-api-go).

## Using
Usage is incredibly simple. All you need to do is add the assembly as an assembly reference (fetch the latest release from the Releases page), initialize a `TatsuClient` with your API key, and do honestly whatever.

### Examples
```cs
                // Create a TatsuClient by inputting an API key, which is read from a text file in this example.
                TatsuClient client = new(await File.ReadAllTextAsync("tatsu.txt"));
                
                // Context is provided by the Discord.NET API, what matters is we're using the client's IDs.
                TatsuUser user = await client.GetUserProfile(Context.User.Id.ToString());
                
                // Example usage of displaying the information in an embed.
                await ReplyAsync(embed: new BaseEmbed(Context.User)
                {
                    // This serves as an example of using the given fields of a TatsuUser, which stores global Tatsu information (XP, rep, etc.)
                    Title = user.Title,
                    Description = $"Avatar URL: {user.AvatarURL}" +
                                  $"\nCredits: {user.Credits}" +
                                  $"\nDiscriminator: {user.Discriminator}" +
                                  $"\nID: {user.ID}" +
                                  $"\nInfo box: {user.InfoBox}" +
                                  $"\nReputation: {user.Reputation}" +
                                  $"\nTokens: {user.Tokens}" +
                                  $"\nUsername: {user.Username}" +
                                  $"\nXP: {user.XP}"
                }.Build());
```

## Rate Limiting
You should be automatically rate-limited after attempting to make `60` requests in a single minute. Your remaining requests are reset to `60` every minute.
