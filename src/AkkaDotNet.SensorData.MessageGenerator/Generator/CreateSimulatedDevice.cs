using System;

namespace AkkaDotNet.SensorData.MessageGenerator.Generator
{
    public class CreateSimulatedDevice
    {
        public Guid DeviceId { get; }

        public CreateSimulatedDevice(Guid deviceId)
        {
            DeviceId = deviceId;
        }
    }
}