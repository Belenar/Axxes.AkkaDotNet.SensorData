using System;

namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class MeterReadingReceived
    {
        public DateTime Timestamp { get; }
        public decimal MeterReading { get; }

        public MeterReadingReceived(DateTime timestamp, decimal meterReading)
        {
            Timestamp = timestamp;
            MeterReading = meterReading;
        }
    }
}
