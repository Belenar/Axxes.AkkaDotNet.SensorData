using System;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Linq;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Database;
using AkkaDotNet.SensorData.Shared.Messages;
using Dapper;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class ReadingDbWriterActor : ReceiveActor
    {
        private readonly Guid _deviceId;

        public ReadingDbWriterActor(Guid deviceId)
        {
            _deviceId = deviceId;
            Receive<WriteReadingsToDatabase>(HandleWriteReadingsToDatabase);
        }

        private void HandleWriteReadingsToDatabase(WriteReadingsToDatabase message)
        {
            using (var connection = new SqlConnection(DbSettings.HistoryConnectionString))
            { 
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    WriteReadings(message.Readings, connection, transaction);
                    transaction.Commit();
                }
            }

            ReplyToSender(message);
        }

        private void WriteReadings(ImmutableList<NormalizedMeterReading> messageReadings, SqlConnection connection, SqlTransaction transaction)
        {
            var query = $@"INSERT INTO dbo.MeterReadings (DeviceId, Timestamp, MeterReading, Consumption)  
                            (SELECT '{_deviceId}', @Timestamp, @MeterReading, @Consumption
                            WHERE NOT EXISTS(SELECT DeviceId FROM dbo.MeterReadings r WHERE r.DeviceId = '{_deviceId}' AND r.Timestamp = @Timestamp)); ";
            connection.Execute(query, messageReadings, transaction);
        }

        private void ReplyToSender(WriteReadingsToDatabase message)
        {
            if (!message.Readings.Any())
                return;
            
            var lastValue = message.Readings.Max(r => r.Timestamp);
            Sender.Tell(new WrittenReadingsToDatabase(lastValue));
        }


        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<ReadingDbWriterActor>(deviceId);
        }
    }
}
