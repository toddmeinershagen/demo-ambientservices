namespace Microservices
{
    public sealed class Clock : AmbientService<IClockProvider, SystemClockProvider>
    {
    }
}