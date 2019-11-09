using System;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Async;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    class ValueNormalizationActor : ReceiveActor
    {
        private readonly IActorRef _persistenceActor;
        private DateTime? _referenceTime;
        private decimal? _referenceReading;
        private MeterReadingReceived _lastMessage;


        public ValueNormalizationActor(IActorRef persistenceActor)
        {
            _persistenceActor = persistenceActor;
            Receive<MeterReadingReceived>(HandleMeterReadingReceived);
        }

        protected override void PreStart()
        {
            var lastValues = AsyncHelper.RunSync(() => 
                _persistenceActor.Ask<ReturnLastNormalizedReading>(new RequestLastNormalizedReading()));
            
            if (lastValues.Reading != null)
            {
                SetReferenceValues(lastValues.Reading.Timestamp, lastValues.Reading.MeterReading);
            }
        }

        private void HandleMeterReadingReceived(MeterReadingReceived message)
        {
            if (_referenceTime == null)
            {
                SetReferenceValues(message.Timestamp, message.MeterReading);
            }
            else
            {

            }
            _lastMessage = message;
        }

        private void SetReferenceValues(DateTime referenceTime, decimal referenceReading)
        {
            _referenceTime = referenceTime;
            _referenceReading = referenceReading;
        }

        public static Props CreateProps(IActorRef persistenceActor)
        {
            return Props.Create<ValueNormalizationActor>(persistenceActor);
        }
    }
}
