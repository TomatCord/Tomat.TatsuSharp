using System;

namespace Tomat.TatsuSharp
{
    public sealed class RateLimitException : Exception
    {
        public override string Message =>
            "Tatsu's rate limit was exceeded! Please wait until the request bucket resets.";
    }
}