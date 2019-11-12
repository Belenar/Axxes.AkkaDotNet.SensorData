using System;
using System.Linq;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Helpers;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    class ValueNormalizationActor : ReceiveActor
    {
        private readonly IActorRef _persistenceActor;
        private DateTime? _referenceTimestamp;
        private int _referenceBucket;
        private decimal _referenceReading;
        private MeterReadingReceived _lastMessage;


        public ValueNormalizationActor(IActorRef persistenceActor)
        {
            _persistenceActor = persistenceActor;
            Receive<ReturnLastNormalizedReadings>(HandleReturnLastMeterReading);
            Receive<MeterReadingReceived>(HandleMeterReadingReceived);
        }

        protected override void PreStart()
        {
           _persistenceActor.Tell(new RequestLastNormalizedReadings(1));
        }

        private void HandleReturnLastMeterReading(ReturnLastNormalizedReadings message)
        {
            if (message.Readings != null && message.Readings.Any())
            {
                var reading = message.Readings.First();
                SetReferenceValues(reading.Timestamp, reading.MeterReading);
                _lastMessage = new MeterReadingReceived(reading.Timestamp, reading.MeterReading);
            }
        }

        private void HandleMeterReadingReceived(MeterReadingReceived message)
        {
            if (_referenceTimestamp == null)
            {
                SetReferenceValues(message.Timestamp, message.MeterReading);
            }
            else
            {
                if (message.Timestamp.BucketNumber() > _referenceBucket)
                {
                    var normalizeRequest = new NormalizedValuesParameters
                    {
                        CurrentMessage = message,
                        PreviousMessage = _lastMessage,
                        StartDate = _referenceTimestamp.Value,
                        StartMeterReading = _referenceReading
                    };
                    var result = BucketConsumptionHelper.ComputeNormalizedValues(normalizeRequest);
                    ProcessNormalizationResult(result);
                }
                    
            }
            _lastMessage = message;
        }

        private void ProcessNormalizationResult(NormalizedValuesResult result)
        {
            SetReferenceValues(result.NewReferenceDate, result.NewReferenceReading);

            foreach (var normalizedValue in result.NormalizedValues)
            {
                Context.Parent.Tell(normalizedValue);
            }
        }

        private void SetReferenceValues(DateTime referenceTime, decimal referenceReading)
        {
            _referenceTimestamp = referenceTime;
            _referenceBucket = referenceTime.BucketNumber();
            _referenceReading = referenceReading;
        }

        public static Props CreateProps(IActorRef persistenceActor)
        {
            return Props.Create<ValueNormalizationActor>(persistenceActor);
        }
    }
}
