using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tomat.TatsuSharp.Data
{
    /// <summary>
    ///     Simple class allowing for parsing provided API headers. <br />
    ///     Adapted from: https://github.com/tatsuworks/tatsu-api-go/blob/main/bucket.go
    /// </summary>
    public class RequestBucket
    {
        public byte Max;
        public byte Remaining;
        public TimeSpan ResetInterval;
        public DateTime ResetTime;

        public RequestBucket(byte max, byte remaining, TimeSpan resetInterval, DateTime resetTime)
        {
            Max = max;
            Remaining = remaining;
            ResetInterval = resetInterval;
            ResetTime = resetTime;
        }

        public Task Acquire()
        {
            if (DateTime.Now > ResetTime)
                Refill();

            if (Remaining > 0)
            {
                Remaining--;
                return Task.CompletedTask;
            }

            try
            {
                Task.Delay(ResetTime.Subtract(DateTime.Now));
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignore
                // can be thrown when millisecondsDelay < -1 which is cringe
            }

            Refill();
            Remaining--;
            return Task.CompletedTask;
        }

        public Task Refill()
        {
            Remaining = Max;
            ResetTime = DateTime.Now.Add(ResetInterval);
            return Task.CompletedTask;
        }

        public Task ParseHeaders(HttpResponseHeaders headers)
        {
            if (!headers.TryGetValues("X-RateLimit-Remaining", out IEnumerable<string> remainingHeaders) ||
                !headers.TryGetValues("X-RateLimit-Reset", out IEnumerable<string> resetHeaders))
                return Task.CompletedTask;

            string remainingHeader = remainingHeaders.First();
            string resetHeader = resetHeaders.First();

            if (!int.TryParse(remainingHeader, out int remaining))
                throw new Exception("remaining");

            if (!int.TryParse(resetHeader, out int reset))
                throw new Exception("reset");

            Remaining = (byte) remaining;
            ResetTime = DateTime.UnixEpoch.AddSeconds(reset);

            return Task.CompletedTask;
        }
    }
}