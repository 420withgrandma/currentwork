using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoTimelyAPI
{
    public class Ticket
    {
        [Key]
        public int TicketNumber { get; set; }

        // Foreign Key to Bus
        [ForeignKey("Bus")]
        public int BusNumber { get; set; }
        public Bus Bus { get; set; } // Navigation property to Bus

        // Use DateTime for proper date handling
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        // Foreign Key to Passenger
        [ForeignKey("Passenger")]
        public int PassengerID { get; set; }
        public Passenger Passenger { get; set; } // Navigation property to Passenger

        public decimal Price { get; set; }

        // Parameterless constructor (required by EF Core)
        public Ticket() {}

        // Optional constructor for manual use (not required by EF)
        public Ticket(int ticketNumber, int busNumber, DateTime fromDate, DateTime toDate, int passengerID, decimal price)
        {
            TicketNumber = ticketNumber;
            BusNumber = busNumber;
            FromDate = fromDate;
            ToDate = toDate;
            PassengerID = passengerID;
            Price = price;
        }

        // Method to get ticket information as a string
        public string GetTicketInfo()
        {
            return $"Ticket #{TicketNumber}\n" +
                $"Bus: {BusNumber}\n" +
                $"Travel: {FromDate:yyyy-MM-dd} to {ToDate:yyyy-MM-dd}\n" +
                $"Passenger: {Passenger.NameID}\n" + // Ensure you're using the correct property, e.g., "NameID"
                $"Price: ${Price:F2}";
        }
    }
}
