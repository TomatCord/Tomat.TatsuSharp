using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tomat.TatsuSharp.Data
{
    public struct RequestBucket
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

        public async Task Acquire()
        {
            if (DateTime.Now > ResetTime)
                await Refill();

            if (Remaining > 0)
            {
                Remaining--;
                return;
            }

            await Task.Delay(ResetTime.Subtract(DateTime.Now));
            await Refill();
            Remaining--;
        }

        public Task Refill()
        {
            Remaining = Max;
            ResetTime = DateTime.Now.Add(ResetInterval);
            return Task.CompletedTask;
        }

        public Task ParseHeaders(HttpResponseHeaders headers)
        {
            if (!headers.GetValues("X-RateLimit-Remaining").Any() ||
                !headers.GetValues("X-RateLimit-Reset").Any())
                return Task.CompletedTask;

            string remainingHeader = headers.GetValues("X-RateLimit-Remaining").First();
            string resetHeader = headers.GetValues("X-RateLimit-Reset").First();

            if (!int.TryParse(remainingHeader, out int remaining))
                throw new Exception("remaining");

            if (!int.TryParse(resetHeader, out int reset))
                throw new Exception("reset");

            Remaining = (byte) remaining;
            ResetTime = DateTime.Parse(reset.ToString());
            return Task.CompletedTask;
        }
    }
}