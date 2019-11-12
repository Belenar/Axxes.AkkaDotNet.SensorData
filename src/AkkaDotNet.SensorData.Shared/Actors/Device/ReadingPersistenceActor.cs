using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class ReadingPersistenceActor : ReceivePersistentActor
    {
        private readonly Guid _deviceId;
        private ReadingPersistenceState _state = new ReadingPersistenceState();
        private readonly IActorRef _dbWriterActor;

        public ReadingPersistenceActor(Guid deviceId)
        {
            _deviceId = deviceId;

            Command<NormalizedMeterReading>(HandleNormalizeMeterReadingCommand);
            Command<RequestLastNormalizedReadings>(HandleRequestLastNormalizedReading);
            Command<TakeHourlySnapshotMessage>(_ => TakeHourlySnapshot());
            Command<WrittenReadingsToDatabase>(HandleWrittenReadingsToDatabaseCommand);

            Recover<SnapshotOffer>(HandleSnapshotOffer);
            Recover<NormalizedMeterReading>(HandleNormalizeMeterReading);
            Recover<WrittenReadingsToDatabase>(HandleWrittenReadingsToDatabase);

            _dbWriterActor = Context.ActorOf(ReadingDbWriterActor.CreateProps(_deviceId));

            ScheduleSnapshots();
        }


        #region Snapshots

        /// <summary>
        /// To spread all the snapshot activity over the hour, we schedule these messages at a random
        /// time in the first hour, and every hour after that. It will trigger the following:
        /// - save of a snapshot
        /// - trigger the save of historic values
        /// - truncate the current state back to 12 hours
        /// </summary>
        private void ScheduleSnapshots()
        {
            var seconds = new Random().Next(3600);
            var initialDelay = new TimeSpan(0, 0, 0, seconds);
            var interval = new TimeSpan(0, 1, 0, 0);
            Context.System.Scheduler.ScheduleTellRepeatedly(initialDelay, interval, Context.Self, new TakeHourlySnapshotMessage(), Context.Self);
        }

        /// <summary>
        /// Restores the last snapshot
        /// </summary>
        private void HandleSnapshotOffer(SnapshotOffer offer)
        {
            if (offer.Snapshot is ReadingPersistenceState state)
                _state = state;
        }

        private void TakeHourlySnapshot()
        { 
            SaveSnapshot(_state);
            _dbWriterActor.Tell(new WriteReadingsToDatabase(_state.GetUnsavedItems()));
        }

        #endregion

        #region NormalizedMeterReading

        private void HandleNormalizeMeterReadingCommand(NormalizedMeterReading message)
        {
            Persist(message, msg => HandleNormalizeMeterReading(message));
        }

        private void HandleNormalizeMeterReading(NormalizedMeterReading message)
        {
            _state.Add(message);
        }

        #endregion

        #region WrittenReadingsToDatabase

        private void HandleWrittenReadingsToDatabaseCommand(WrittenReadingsToDatabase message)
        {
            Persist(message, msg => HandleWrittenReadingsToDatabase(message));
        }

        private void HandleWrittenReadingsToDatabase(WrittenReadingsToDatabase message)
        {
            _state.SetSavedUntil(message.WrittenToDate);
            _state.Truncate();
        }

        #endregion

        #region RequestLastNormalizedReadings

        private void HandleRequestLastNormalizedReading(RequestLastNormalizedReadings message)
        {
            var lastReadings = _state.GetLastReadings(message.NumberOfReadings);
            var response = new ReturnLastNormalizedReadings(lastReadings);
            Sender.Tell(response);
        }

        #endregion

        public static Props CreateProps(Guid deviceId)
        {
            return Props.Create<ReadingPersistenceActor>(deviceId);
        }

        public override string PersistenceId => $"value-persistence-{_deviceId}";
    }

    class ReadingPersistenceState
    {
        public List<ReadingPersistenceStateItem> Items { get; } = new List<ReadingPersistenceStateItem>();

        public void Add(NormalizedMeterReading reading)
        {
            Items.Add(new ReadingPersistenceStateItem { Reading = reading, Saved = false });
        }

        public NormalizedMeterReading[] GetUnsavedItems()
        {
            return Items.Where(i => !i.Saved).Select(i => i.Reading).ToArray();
        }

        public void SetSavedUntil(DateTime until)
        {
            foreach (var item in Items.Where(i => i.Reading.Timestamp <= until))
            {
                item.Saved = true;
            }
        }

        public NormalizedMeterReading[] GetLastReadings(int numberOfReadings)
        {
            var numberOfReturnedReadings = Math.Min(numberOfReadings, Items.Count);

            if (numberOfReturnedReadings == 0)
                return Array.Empty<NormalizedMeterReading>();

            return Items
                .Select(i => i.Reading)
                .OrderByDescending(r => r.Timestamp)
                .Take(numberOfReturnedReadings)
                .OrderBy(r => r.Timestamp)
                .ToArray();
        }

        public void Truncate()
        {
            if (Items.Any())
            {
                var bottomDate = Items.Last().Reading.Timestamp.AddHours(-12);
                Items.RemoveAll(i => i.Reading.Timestamp < bottomDate && i.Saved);
            }
        }
    }

    class ReadingPersistenceStateItem
    {
        public NormalizedMeterReading Reading { get; set; }
        public bool Saved { get; set; }
    }

    class TakeHourlySnapshotMessage
    {
    }
}
