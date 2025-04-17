using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoTimelyAPI
{
    public class Passenger
    {
        [Key]
        [Column("passengerID")]
        public int PassengerID { get; set; }

        [Column("Name")]
        public string NameID { get; set; } // Corrected the getter/setter syntax

        [Column("emailID")]
        [EmailAddress]
        public string EmailID { get; set; }

        [Column("phoneID")]
        public long PhoneID { get; set; }

        // Navigation property for Tickets
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        // Password hash for authentication
        public string PasswordHash { get; set; } = string.Empty;

        // Method to get passenger information
        public string GetPassengerInfo()
        {
            return $"Passenger: {PassengerID}, Email: {EmailID}, Phone: {PhoneID}";
        }
    }
}
