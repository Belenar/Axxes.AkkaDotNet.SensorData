using System.IO;
using Akka.Configuration;

namespace AkkaDotNet.SensorData.Shared.Helpers
{
    public static class ConfigurationReader
    {
        public static Config ReadAkkaConfigurationFile()
        {
            // ReSharper disable IdentifierTypo
            var hoconFile = Directory.GetCurrentDirectory() + "\\akka.conf";
            var hoconContent = File.ReadAllText(hoconFile);
            // ReSharper restore IdentifierTypo

            var config = ConfigurationFactory.ParseString(hoconContent);
            return config;
        }
    }
}
