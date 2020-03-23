using System;
using System.Diagnostics.CodeAnalysis;

namespace Microservices
{
    [ExcludeFromCodeCoverage]
    public class SystemClockProvider : IClockProvider
    {
        public DateTimeOffset GetDateTimeOffsetNow()
        {
            return DateTimeOffset.Now;
        }
    }
}