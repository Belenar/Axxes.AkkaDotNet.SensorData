using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Actors;
using AkkaDotNet.SensorData.Shared.Helpers;

namespace AkkaDotNet.SensorData.ActorSystemHost
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Starting the ActorSystem ...");

            var config = ConfigurationReader.ReadAkkaConfigurationFile();

            using (var actorSystem = ActorSystem.Create("SensorDataDemo", config))
            {
                InitializeDevicesActor(actorSystem);
                await ProcessConsoleCommandsUntilExit(actorSystem);
            }

            Console.WriteLine("ActorSystem stopped. Press any key to exit ...");
            Console.ReadKey();
        }

        private static void InitializeDevicesActor(ActorSystem actorSystem)
        {
            var devicesActorRef = actorSystem.ActorOf(DevicesActor.CreateProps(), "devices");
        }


        private static async Task ProcessConsoleCommandsUntilExit(ActorSystem actorSystem)
        {
            var stopped = false;

            while (!stopped)
            {
                Console.WriteLine("Please enter your command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "exit":
                        await actorSystem.Terminate();
                        stopped = true;
                        break;
                }
            }
        }
    }
}
