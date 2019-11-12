using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class AlertsActor : ReceiveActor
    {
        private readonly Guid _deviceId;
        private readonly IActorRef _persistenceActor;
        private readonly IActorRef _dbReaderActor;
        private readonly List<IActorRef> _alertActors = new List<IActorRef>();

        public AlertsActor(Guid deviceId, IActorRef persistenceActor)
        {
            _deviceId = deviceId;
            _persistenceActor = persistenceActor;

            Receive<CreatePeriodicAlert>(HandleCreatePeriodicAlert);
            Receive<NormalizedMeterReading>(HandleMeterReading);

            _dbReaderActor = Context.ActorOf(AlertsDbReaderActor.CreateProps(_deviceId));
        }

        protected override void PreStart()
        {
            _dbReaderActor.Tell(new ReadAlertConfigurations());
        }

        private void HandleCreatePeriodicAlert(CreatePeriodicAlert message)
        {
            var props = PeriodicAlertActor.CreateProps(_deviceId, message.NumberOfMinutes, message.ThresholdConsumption);
            var alertActor = Context.ActorOf(props);
            _alertActors.Add(alertActor);
        }

        private void HandleMeterReading(NormalizedMeterReading message)
        {
            foreach (var alertActor in _alertActors)
            {
                alertActor.Forward(message);
            }
        }

        public static Props CreateProps(Guid deviceId, IActorRef persistenceActor)
        {
            return Props.Create<AlertsActor>(deviceId, persistenceActor);
        }
    }
}
