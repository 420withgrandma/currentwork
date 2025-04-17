using System.ComponentModel.DataAnnotations;

namespace GoTimelyAPI
{
    public class Bus
    {
        [Key]
        public int BusNumber { get; set; }  // Unique identifier for the bus
        public string FromDestination { get; set; }  // Starting destination
        public string ToDestination { get; set; }  // End destination
        public int Capacity { get; set; }  // Bus capacity

        // Constructor
        public Bus(int busNumber, string fromDestination, string toDestination, int capacity)
        {
            if (busNumber <= 0)
                throw new ArgumentException("Bus number must be positive", nameof(busNumber));
            
            if (string.IsNullOrEmpty(fromDestination))
                throw new ArgumentException("Origin location cannot be empty", nameof(fromDestination));
            
            if (string.IsNullOrEmpty(toDestination))
                throw new ArgumentException("Destination location cannot be empty", nameof(toDestination));
            
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be positive", nameof(capacity));

            BusNumber = busNumber;
            FromDestination = fromDestination;
            ToDestination = toDestination;
            Capacity = capacity;
        }
    }
}
