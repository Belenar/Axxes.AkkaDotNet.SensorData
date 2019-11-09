using System;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class ValuePersistenceActor : ReceiveActor
    {
        private readonly Guid _deviceId;
        private NormalizedMeterReading _lastReading;

        public ValuePersistenceActor(Guid deviceId)
        {
            _deviceId = deviceId;

            Receive<NormalizedMeterReading>(HandleNormalizeMeterReading);
            Receive<RequestLastNormalizedReading>(HandleRequestLastNormalizedReading);
        }

        private void HandleNormalizeMeterReading(NormalizedMeterReading message)
        {
            _lastReading = message;
        }

        private void HandleRequestLastNormalizedReading(RequestLastNormalizedReading message)
        {
            var response = new ReturnLastNormalizedReading(_lastReading);
            Sender.Tell(response);
        }

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<ValuePersistenceActor>(deviceId);
        }
    }
}
