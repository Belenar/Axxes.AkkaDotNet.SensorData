using System;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class DeviceActor : ReceiveActor
    {
        private readonly Guid _deviceId;
        private IActorRef _persistenceActor;
        private IActorRef _normalizationActor;

        public DeviceActor(Guid deviceId)
        {
            _deviceId = deviceId;

            // Messages to handle
            Receive<MeterReadingReceived>(HandleMeterReadingReceived);
            Receive<NormalizedMeterReading>(HandleNormalizedMeterReading);

            CreateChildren();
        }

        private void CreateChildren()
        {
            var persistenceProps = ReadingPersistenceActor.CreateProps(_deviceId);
            _persistenceActor = Context.ActorOf(persistenceProps, "value-persistence");

            var normalizationProps = ValueNormalizationActor.CreateProps(_persistenceActor);
            _normalizationActor = Context.ActorOf(normalizationProps, "value-normalization");
        }


        private void HandleMeterReadingReceived(MeterReadingReceived message)
        {
            _normalizationActor.Forward(message);
        }

        private void HandleNormalizedMeterReading(NormalizedMeterReading message)
        {
            _persistenceActor.Tell(message);
        }

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<DeviceActor>(deviceId);
        }
    }
}