namespace GoTimelyAPI
{
    public class BusLocal : Bus
    {
        public string LocalFeatures { get; set; }  // Example: Local stops and features

        // Constructor
        public BusLocal(int busNumber, string fromDestination, string toDestination, int capacity, string localFeatures)
            : base(busNumber, fromDestination, toDestination, capacity)
        {
            LocalFeatures = localFeatures;
        }
    }
}
