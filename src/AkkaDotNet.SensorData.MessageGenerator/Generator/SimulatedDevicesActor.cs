using Akka.Actor;

namespace AkkaDotNet.SensorData.MessageGenerator.Generator
{
    public class SimulatedDevicesActor : ReceiveActor
    {
        public SimulatedDevicesActor()
        {
            Receive<CreateSimulatedDevice>(HandleCreateSimulatedDevice);
        }

        private void HandleCreateSimulatedDevice(CreateSimulatedDevice message)
        {
            Context.ActorOf(SimulatedDeviceActor.CreateProps(message.DeviceId), $"simulated-device-{message.DeviceId}");
        }

        public static Props CreateProps()
        {
            return Props.Create<SimulatedDevicesActor>();
        }
    }
}