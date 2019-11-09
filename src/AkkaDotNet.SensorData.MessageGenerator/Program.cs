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
        private static bool _devicesCreated;

        static async Task Main()
        {
            CreateActorSystem();

            ProcessConsoleCommandsUntilExit();

            await StopActorSystem();
        }

        /// <summary>
        /// Creates the 'client' ActorSystem and a top level actor.
        /// </summary>
        private static void CreateActorSystem()
        {
            Console.WriteLine("Starting the ActorSystem ...");
            var config = ConfigurationReader.ReadAkkaConfigurationFile();
            _actorSystem = ActorSystem.Create("SensorDataDemo", config);
            _simulatedDevicesActor = _actorSystem.ActorOf(SimulatedDevicesActor.CreateProps(), "simulated-devices");
        }

        /// <summary>
        /// Processes command line commands until the 'exit' command is passed
        /// - start: creates the simulated devices or resumes sending
        /// - pause: suspends the sending of messages
        /// - exit: stops the loop, causing the program to end
        /// </summary>
        private static void ProcessConsoleCommandsUntilExit()
        {
            var stopped = false;

            while (!stopped)
            {
                Console.WriteLine("Please enter your command (start|pause|exit):");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "start":
                        StartSimulatedDevices();
                        break;
                    case "pause":
                        PauseSimulatedDevices();
                        break;
                    case "exit":
                        stopped = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the 'start'' command
        /// Either creates an actor for every device that will be simulated in the 'client' ActorSystem
        /// Or resumes sending messages from the simulated devices.
        /// </summary>
        private static void StartSimulatedDevices()
        {
            if (!_devicesCreated)
            {
                CreateDevices();
                
            }
            else
            {
                ResumeSending();
            }
        }

        /// <summary>
        /// Creates all simulated devices based on the Guid list in devices.conf
        /// These actors will start sending messages to the remote system every 2 seconds.
        /// </summary>
        private static void CreateDevices()
        {
            var devicesFile = Directory.GetCurrentDirectory() + "\\devices.conf";

            using var reader = File.OpenText(devicesFile);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (Guid.TryParse(line, out var deviceId))
                {
                    _simulatedDevicesActor.Tell(new CreateSimulatedDevice(deviceId));
                }
            }

            _devicesCreated = true;
            Console.WriteLine("Simulated devices started.");
        }

        /// <summary>
        /// Resumes sending messages from the simulated devices
        /// </summary>
        private static void ResumeSending()
        {
            _simulatedDevicesActor.Tell(new StartSending());
        }



        /// <summary>
        /// Temporarily stops all new messages to be sent from the simulated devices
        /// </summary>
        private static void PauseSimulatedDevices()
        {
            _simulatedDevicesActor.Tell(new PauseSending());
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