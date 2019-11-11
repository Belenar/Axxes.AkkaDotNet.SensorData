using System;
using System.Collections.Generic;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Helpers
{
    static class BucketConsumptionHelper
    {
        public static NormalizedValuesResult ComputeNormalizedValues (NormalizedValuesParameters request)
        {
            var result = new NormalizedValuesResult
            {
                NewReferenceReading = request.StartMeterReading
            };

            var startBucketNumber = request.StartDate.BucketNumber();
            var endBucketNumber = request.CurrentMessage.Timestamp.BucketNumber();

            for (int bucket = startBucketNumber + 1; bucket <= endBucketNumber; bucket++)
            {
                var normalizedValue = GetIntermediateValue(request, bucket.BucketDate());

                result.NormalizedValues.Add(normalizedValue);

                result.NewReferenceDate = normalizedValue.Timestamp;
                result.NewReferenceReading = normalizedValue.MeterReading;
            }

            return result;
        }

        private static NormalizedMeterReading GetIntermediateValue(NormalizedValuesParameters request, DateTime bucketDate)
        {
            if (bucketDate == request.CurrentMessage.Timestamp)
                return new NormalizedMeterReading(
                    bucketDate, 
                    request.CurrentMessage.MeterReading - request.StartMeterReading, 
                    request.CurrentMessage.MeterReading);

            var millisecondsBetweenMessages = (decimal)(request.CurrentMessage.Timestamp - request.PreviousMessage.Timestamp).TotalMilliseconds;
            var millisecondsFromPreviousMessage = (decimal)(bucketDate - request.PreviousMessage.Timestamp).TotalMilliseconds;

            var totalConsumption = request.CurrentMessage.MeterReading - request.PreviousMessage.MeterReading;
            var messageConsumption = totalConsumption * (millisecondsFromPreviousMessage / millisecondsBetweenMessages);

            var bucketReading = request.PreviousMessage.MeterReading + messageConsumption;
            var bucketConsumption = bucketReading - request.StartMeterReading;

            return new NormalizedMeterReading(
                bucketDate,
                bucketConsumption,
                bucketReading);
        }
    }

    public class NormalizedValuesParameters
    {
        public DateTime StartDate { get; set; }
        public decimal StartMeterReading { get; set; }
        public MeterReadingReceived PreviousMessage { get; set; }
        public MeterReadingReceived CurrentMessage { get; set; }
    }

    public class NormalizedValuesResult
    {
        public List<NormalizedMeterReading> NormalizedValues { get; set; } = new List<NormalizedMeterReading>();
        public DateTime NewReferenceDate { get; set; }
        public decimal NewReferenceReading { get; set; }
    }
}