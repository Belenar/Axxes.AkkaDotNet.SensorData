using System.Collections.Generic;
using Akka.Actor;

namespace AkkaDotNet.SensorData.MessageGenerator.Generator
{
    public class SimulatedDevicesActor : ReceiveActor
    {
        private readonly List<IActorRef> _devices = new List<IActorRef>();
        public SimulatedDevicesActor()
        {
            
            Receive<StartSending>(HandleStartSending);
            Receive<PauseSending>(HandlePauseSending);
            Receive<CreateSimulatedDevice>(HandleCreateSimulatedDevice);
        }

        private void HandleStartSending(StartSending message)
        {
            foreach (var device in _devices)
            {
                device.Forward(message);
            }
        }

        private void HandlePauseSending(PauseSending message)
        {
            foreach (var device in _devices)
            {
                device.Forward(message);
            }
        }

        private void HandleCreateSimulatedDevice(CreateSimulatedDevice message)
        {
            _devices.Add(Context.ActorOf(SimulatedDeviceActor.CreateProps(message.DeviceId), $"simulated-device-{message.DeviceId}"));
        }

        public static Props CreateProps()
        {
            return Props.Create<SimulatedDevicesActor>();
        }
    }
}