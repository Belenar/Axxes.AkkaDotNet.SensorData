using Akka.Actor;

namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class DeviceConnected
    {
        public IActorRef DeviceRef { get; }

        public DeviceConnected(IActorRef deviceRef)
        {
            DeviceRef = deviceRef;
        }
    }
}