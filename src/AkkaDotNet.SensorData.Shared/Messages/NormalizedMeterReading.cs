using System;

namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class NormalizedMeterReading
    {
        public DateTime Timestamp { get; }
        public decimal Consumption { get; }
        public decimal MeterReading { get; }

        public NormalizedMeterReading(DateTime timestamp, decimal consumption, decimal meterReading)
        {
            Timestamp = timestamp;
            Consumption = consumption;
            MeterReading = meterReading;
        }
    }
}