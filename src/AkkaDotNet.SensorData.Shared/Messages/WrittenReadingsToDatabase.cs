using System;

namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class WrittenReadingsToDatabase
    {
        public DateTime WrittenToDate { get; }

        public WrittenReadingsToDatabase(DateTime writtenToDate)
        {
            WrittenToDate = writtenToDate;
        }
    }
}