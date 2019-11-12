using System.Collections.Immutable;

namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class ReturnLastNormalizedReadings
    {
        public ImmutableArray<NormalizedMeterReading> Readings { get; }

        public ReturnLastNormalizedReadings(params NormalizedMeterReading[] readings)
        {
            Readings = ImmutableArray.Create(readings);
        }
    }
}