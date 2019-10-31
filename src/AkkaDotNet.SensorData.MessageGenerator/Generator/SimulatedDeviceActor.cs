using System;
using Akka.Actor;

namespace AkkaDotNet.SensorData.MessageGenerator.Generator
{
    public class SimulatedDeviceActor : ReceiveActor
    {
        private readonly Guid _deviceId;

        public SimulatedDeviceActor(Guid deviceId)
        {
            _deviceId = deviceId;
            Receive<SendNextReading>(_ => SendNextReadingToDevice());
        }

        private void SendNextReadingToDevice()
        {
            Console.WriteLine($"Sending message from {_deviceId}");
        }

        protected override void PreStart()
        {
            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromSeconds(10), 
                TimeSpan.FromSeconds(1), 
                Self, 
                new SendNextReading(), 
                Self );
        }

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<SimulatedDeviceActor>(deviceId);
        }
    }
}
