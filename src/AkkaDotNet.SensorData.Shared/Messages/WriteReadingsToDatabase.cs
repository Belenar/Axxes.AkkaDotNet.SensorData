using System.Collections.Immutable;

namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class WriteReadingsToDatabase
    {
        public ImmutableList<NormalizedMeterReading> Readings { get; }

        public WriteReadingsToDatabase(NormalizedMeterReading[] readings)
        {
            Readings = ImmutableList.Create(readings);
        }
    }
}
