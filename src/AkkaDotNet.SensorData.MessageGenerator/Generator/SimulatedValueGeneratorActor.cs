using System;
using Akka.Actor;
using AkkaDotNet.SensorData.Shared.Messages;

namespace AkkaDotNet.SensorData.MessageGenerator.Generator
{
    class SimulatedValueGeneratorActor : ReceiveActor
    {
        private Random _random;
        private int _currentValue;
        private DateTime _currentTime;

        public SimulatedValueGeneratorActor()
        {
            InitValues();
            Started();
        }

        private void InitValues()
        {
            _random = new Random();
            _currentValue = _random.Next(100000);
            _currentTime = DateTime.Now;
        }

        private void HandlePauseSending(PauseSending message)
        {
            Become(Paused);
        }

        private void Paused()
        {
            Receive<StartSending>(HandleStartSending);
            Receive<PauseSending>(HandlePauseSending);
            Receive<SendNextReading>(_ => { });
        }

        private void HandleStartSending(StartSending message)
        {
            Become(Started);
        }

        private void Started()
        {
            Receive<StartSending>(HandleStartSending);
            Receive<PauseSending>(HandlePauseSending);
            Receive<SendNextReading>(_ => SendNextReadingToDevice());
        }

        private void SendNextReadingToDevice()
        {
            _currentTime = _currentTime.AddMinutes(1);
            _currentValue = _currentValue + _random.Next(11); // 0 - 10
            var message = new MeterReadingReceived(_currentTime, _currentValue);
            Context.Parent.Tell(message);
        }

        protected override void PreStart()
        {
            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(1),
                Self,
                new SendNextReading(),
                Self);
        }

        public static Props CreateProps()
        {
            return Props.Create<SimulatedValueGeneratorActor>();
        }
    }
}
