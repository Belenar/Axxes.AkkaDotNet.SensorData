using System;
using Akka.Actor;
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
        public DevicesActor()
        {
            Receive<HelloMessage>(HandleHello);
        }

        private void HandleHello(HelloMessage msg)
        {
            Console.WriteLine("Hello Received");
        }

        public static Props CreateProps()
        {
            return Props.Create<DevicesActor>();
        }
    }
}
