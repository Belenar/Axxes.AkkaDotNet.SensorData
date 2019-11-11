using System;

namespace AkkaDotNet.SensorData.Shared.Helpers
{
    /// <summary>
    /// For logical purposes, we will number every 5 minute mark since 2000-01-01 with an integer number
    /// This makes the logic of thinking about time buckets easier.
    /// This helper class converts between DateTime and the Bucket number
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime ReferenceDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static int BucketNumber(this DateTime date)
        {
            var timespan = date - ReferenceDate;

            return (int) Math.Floor(timespan.TotalMinutes / 5);
        }

        public static DateTime BucketDate(this int bucketNumber)
        {
            return ReferenceDate.AddMinutes(bucketNumber * 5);
        }
    }
}
