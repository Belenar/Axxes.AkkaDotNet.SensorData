using System;
using System.IO;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaDotNet.SensorData.MessageGenerator.Generator;
using AkkaDotNet.SensorData.Shared.Helpers;

namespace AkkaDotNet.SensorData.MessageGenerator
{
    class Program
    {
        private static ActorSystem _actorSystem;
        private static IActorRef _simulatedDevicesActor;

        static async Task Main()
        {
            CreateActorSystem();
            InitializeDevicesActor();

            ProcessConsoleCommandsUntilExit();

            await StopActorSystem();
        }

        private static void CreateActorSystem()
        {
            Console.WriteLine("Starting the ActorSystem ...");
            var config = ConfigurationReader.ReadAkkaConfigurationFile();
            _actorSystem = ActorSystem.Create("SensorDataDemo", config);
        }

        /// <summary>
        /// Creates a top level actor in the 'client' ActorSystem.
        /// </summary>
        private static void InitializeDevicesActor()
        {
            _simulatedDevicesActor = _actorSystem.ActorOf(SimulatedDevicesActor.CreateProps(), "simulated-devices");
        }

        /// <summary>
        /// Processes command line commands until the 'exit' command is passed
        /// - start: creates the simulated devices
        /// - exit: stops the loop, causing the program to end
        /// </summary>
        private static void ProcessConsoleCommandsUntilExit()
        {
            var stopped = false;

            while (!stopped)
            {
                Console.WriteLine("Please enter your command (start|exit):");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "start":
                        CreateSimulatedDevices();
                        break;
                    case "exit":
                        stopped = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Creates an actor for every device that will be simulated in the 'client' ActorSystem
        /// These actors will start sending messages to the remote system every 2 seconds.
        /// </summary>
        private static void CreateSimulatedDevices()
        {
            var devicesFile = Directory.GetCurrentDirectory() + "\\devices.conf"; 
            
            using var reader = File.OpenText(devicesFile);
            
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (Guid.TryParse(line, out var deviceId))
                {
                    _simulatedDevicesActor.Tell(new CreateSimulatedDevice(deviceId));
                }
            }
        }

        /// <summary>
        /// Ends all actors and stops the ActorSystem
        /// </summary>
        private static async Task StopActorSystem()
        {
            _simulatedDevicesActor.Tell(PoisonPill.Instance);
            await _actorSystem.Terminate();
            Console.WriteLine("ActorSystem stopped. Press any key to exit ...");
            Console.ReadKey();
        }
    }
}