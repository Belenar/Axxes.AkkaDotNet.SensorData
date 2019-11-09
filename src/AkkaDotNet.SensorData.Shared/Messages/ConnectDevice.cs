using System;

namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class ConnectDevice
    {
        public Guid Id { get; }


        public ConnectDevice(Guid id)
        {
            Id = id;
        }
    }
}
