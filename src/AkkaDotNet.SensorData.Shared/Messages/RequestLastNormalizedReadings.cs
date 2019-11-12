namespace AkkaDotNet.SensorData.Shared.Messages
{
    public class RequestLastNormalizedReadings
    {
        public int NumberOfReadings { get; }

        public RequestLastNormalizedReadings(int numberOfReadings)
        {
            NumberOfReadings = numberOfReadings;
        }
    }
}
