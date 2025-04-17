namespace GoTimelyAPI
{
    public class BusExpress : Bus
    {
        public string ExpressFeatures { get; set; }  // Example: Express-only features like faster travel

        // Constructor
        public BusExpress(int busNumber, string fromDestination, string toDestination, int capacity, string expressFeatures)
            : base(busNumber, fromDestination, toDestination, capacity)
        {
            ExpressFeatures = expressFeatures;
        }
    }
}
