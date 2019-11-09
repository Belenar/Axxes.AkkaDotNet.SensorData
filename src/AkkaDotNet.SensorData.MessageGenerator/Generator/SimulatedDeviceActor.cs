using System;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.MessageGenerator.Generator
{
    public class SimulatedDeviceActor : ReceiveActor
    {
        private readonly Guid _deviceId;
        private readonly IActorRef _generatorActor;
        private readonly IActorRef _deviceProxy;

        public SimulatedDeviceActor(Guid deviceId)
        {
            _deviceId = deviceId;
            _generatorActor = Context.ActorOf(SimulatedValueGeneratorActor.CreateProps(), "value-generator");
            _deviceProxy = Context.ActorOf(props: DeviceActorProxy.CreateProps(_deviceId), "device-proxy");
            Receive<StartSending>(HandleStartSending);
            Receive<PauseSending>(HandlePauseSending);
            Receive<MeterReadingReceived>(HandleMeterReadingReceived);
        }

        private void HandleStartSending(StartSending message)
        {
            _generatorActor.Forward(message);
        }

        private void HandlePauseSending(PauseSending message)
        {
            _generatorActor.Forward(message);
        }

        private void HandleMeterReadingReceived(MeterReadingReceived message)
        {
            _deviceProxy.Tell(message);
        }

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<SimulatedDeviceActor>(deviceId);
        }
    }
}
