namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class CreatePeriodicAlert
    {
        public int NumberOfMinutes { get; }
        public decimal ThresholdConsumption { get; }

        public CreatePeriodicAlert(int numberOfMinutes, decimal thresholdConsumption)
        {
            NumberOfMinutes = numberOfMinutes;
            ThresholdConsumption = thresholdConsumption;
        }
    }
}