namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class ReturnLastNormalizedReading
    {
        public NormalizedMeterReading Reading { get; }

        public ReturnLastNormalizedReading(NormalizedMeterReading reading)
        {
            Reading = reading;
        }
    }
}