using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.Shared.Actors.Device
{
    public class PeriodicAlertActor : ReceiveActor
    {
        private readonly Guid _deviceId;
        private readonly int _numberOfMinutes;
        private readonly decimal _consumptionThreshold;
        private readonly PeriodicAlertActorState _state;
        

        public PeriodicAlertActor(Guid deviceId, int numberOfMinutes, decimal consumptionThreshold)
        {
            var numberOfReadings = (int)Math.Ceiling(numberOfMinutes / 5M);
            _deviceId = deviceId;
            _numberOfMinutes = numberOfMinutes;
            _consumptionThreshold = consumptionThreshold; 
            _state = new PeriodicAlertActorState(numberOfReadings);

            Receive<NormalizedMeterReading>(HandleNormalizedMeterReading);
            Receive<ReturnLastNormalizedReadings>(HandleReturnLastNormalizedReadings);
        }

        private void HandleNormalizedMeterReading(NormalizedMeterReading message)
        {
            _state.AddReading(message);
            if (_state.CheckIfThresholdExceeded(_consumptionThreshold))
            {
                // Raise an alert. Maybe to Notification Hubs or whatever.
                // Remember to use a character actor if the operation is risky.
                Console.WriteLine($"PERIODIC ALERT FOR ACTOR {_deviceId}: Consumption exceeded {_consumptionThreshold} for more than {_numberOfMinutes} minutes.");
            }
        }

        private void HandleReturnLastNormalizedReadings(ReturnLastNormalizedReadings message)
        {
            foreach (var normalizedMeterReading in message.Readings)
            {
                _state.AddReading(normalizedMeterReading);
            }
        }

        public static Props CreateProps(Guid deviceId, int numberOfMinutes, decimal consumptionThreshold)
        {
            return Props.Create<PeriodicAlertActor>(deviceId, numberOfMinutes, consumptionThreshold);
        }
    }


    class PeriodicAlertActorState
    {
        private readonly int _numberOfReadings;
        private readonly Queue<NormalizedMeterReading> _lastReadings = new Queue<NormalizedMeterReading>();

        public PeriodicAlertActorState(int numberOfReadings)
        {
            _numberOfReadings = numberOfReadings;
        }

        public void AddReading(NormalizedMeterReading reading)
        {
            _lastReadings.Enqueue(reading);
            while (_lastReadings.Count > _numberOfReadings)
            {
                _lastReadings.Dequeue();
            }
        }

        public bool CheckIfThresholdExceeded(decimal thresholdReading)
        {
            if (_lastReadings.Count < _numberOfReadings)
                return false;

            var alertOccurred = _lastReadings.All(r => r.Consumption >= thresholdReading);

            return alertOccurred;
        }
    }
}