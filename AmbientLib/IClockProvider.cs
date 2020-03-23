using System;

namespace Microservices
{
    public interface IClockProvider
    {
        DateTimeOffset GetDateTimeOffsetNow();
    }
}