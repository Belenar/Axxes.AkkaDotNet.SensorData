using System;
using Akka.Actor;
using AkkaDotNet.SensorData.MessageGenerator.Config;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.MessageGenerator.Generator
{
    /// <summary>
    /// The rest of the simulator is there to feed this actor with messages.
    /// In a real world application, this is the piece you need to pass messages to the remote ActorSystem
    /// </summary>
    class DeviceActorProxy : ReceiveActor
    {
        private readonly Guid _deviceId;
        private IActorRef _deviceActor;

        public DeviceActorProxy(Guid deviceId)
        {
            _deviceId = deviceId;
            Receive<MeterReadingReceived>(HandleMeterReadingReceived);
            Receive<DeviceConnected>(HandleDeviceConnected);
        }

        protected override void PreStart()
        {
            var devicesActorPath = $"{Constants.RemoteActorSystemAddress}/user/devices";
            var devicesActor = Context.ActorSelection(devicesActorPath);
            
            var request = new ConnectDevice(_deviceId);
            devicesActor.Tell(request);
        }

        private void HandleDeviceConnected(DeviceConnected message)
        {
            _deviceActor = message.DeviceRef;
        }

        private void HandleMeterReadingReceived(MeterReadingReceived message)
        {
            _deviceActor?.Tell(message);
        }

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<DeviceActorProxy>(deviceId);
        }
    }
}
