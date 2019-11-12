using System;
using System.Data.SqlClient;
using System.Linq;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Database;
using AkkaDotNet.SensorData.Shared.Messages;
using Dapper;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class AlertsDbReaderActor : ReceiveActor
    {
        private readonly Guid _deviceId;

        public AlertsDbReaderActor(Guid deviceId)
        {
            _deviceId = deviceId;
            Receive<ReadAlertConfigurations>(HandleReadAlertConfigurations);
        }

        private void HandleReadAlertConfigurations(ReadAlertConfigurations obj)
        {
            using (var connection = new SqlConnection(DbSettings.HistoryConnectionString))
            {
                var query =
                    "SELECT [NumberOfMinutes], [ThresholdConsumption] FROM [dbo].[PeriodicAlertConfigurations] WHERE [DeviceId] = @DeviceId";

                var alerts = connection
                    .Query(query, new {DeviceId = _deviceId})
                    .Select(alert => new CreatePeriodicAlert(alert.NumberOfMinutes, alert.ThresholdConsumption));

                foreach (var createPeriodicAlert in alerts)
                {
                    Context.Parent.Tell(createPeriodicAlert);
                }
            }
        }

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<AlertsDbReaderActor>(deviceId);
        }
    }
}