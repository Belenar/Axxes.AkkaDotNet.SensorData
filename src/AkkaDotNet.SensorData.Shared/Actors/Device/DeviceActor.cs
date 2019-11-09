using System;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class DeviceActor : ReceiveActor
    {
        private readonly Guid _deviceId;
        private readonly IActorRef _persistenceActor;
        private readonly IActorRef _normalizationActor;

        public DeviceActor(Guid deviceId)
        {
            _deviceId = deviceId;

            // Messages to handle
            Receive<MeterReadingReceived>(HandleMeterReadingReceived);

            // Create children
            _persistenceActor = Context.ActorOf(ValuePersistenceActor.CreateProps(_deviceId));
            _normalizationActor = Context.ActorOf(ValueNormalizationActor.CreateProps(_persistenceActor));
        }

        private void HandleMeterReadingReceived(MeterReadingReceived message)
        {
            _normalizationActor.Forward(message);
        }

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<DeviceActor>(deviceId);
        }
    }
}