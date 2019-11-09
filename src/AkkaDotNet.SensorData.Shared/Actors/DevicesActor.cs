using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Actors.Device;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors
{
    /// <summary>
    /// Address:
    ///     /user/devices
    /// Startup:
    ///     Creating a DevicesInitializationActor to get existing devices out of the database.
    /// Responsibilities:
    ///     - Creating device actors
    ///     - Supervising device actors
    /// </summary>
    public class DevicesActor : ReceiveActor
    {
        Dictionary<Guid, IActorRef> _deviceActors = new Dictionary<Guid, IActorRef>();

        public DevicesActor()
        {
            Receive<ConnectDevice>(HandleConnectDevice);
        }

        private void HandleConnectDevice(ConnectDevice request)
        {
            if (!_deviceActors.ContainsKey(request.Id))
            {
                CreateDeviceActor(request.Id);
            }
            var response = new DeviceConnected(_deviceActors[request.Id]);
            Sender.Tell(response);
        }

        private void CreateDeviceActor(Guid deviceId)
        {
            var props = DeviceActor.CreateProps(deviceId);
            var name = $"device-{deviceId}";
            var deviceActorRef = Context.ActorOf(props, name);
            
            _deviceActors[deviceId] = deviceActorRef;
        }

        public static Props CreateProps()
        {
            return Props.Create<DevicesActor>();
        }
    }
}