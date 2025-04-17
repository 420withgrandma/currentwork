using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoTimelyAPI
{
    public class Seat
    {
        [Key] // Marks SeatID as the primary key
        public int SeatID { get; set; }
        
        // Foreign key to Bus
        [ForeignKey("Bus")]
        public int BusID { get; set; }
        public Bus Bus { get; set; } // Navigation property to Bus

        public bool IsReserved { get; set; }

        public Seat() { }

        public Seat(int seatID, int busID, bool isReserved)
        {
            SeatID = seatID;
            BusID = busID;
            IsReserved = isReserved;
        }

        // Reserve seat for user
        public bool ReserveSeat()
        {
            if (!IsReserved)
            {
                IsReserved = true;
                // Logic to save changes in the database (e.g., update the record in DB)
                return true;
            }
            return false; // If seat is already reserved
        }

        // Cancel reservation for seat
        public bool CancelReservation()
        {
            if (IsReserved)
            {
                IsReserved = false;
                // Logic to save changes in the database (e.g., update the record in DB)
                return true;
            }
            return false; // If seat is not reserved
        }

        // Check if seat is reserved or available
        public bool GetSeatStatus()
        {
            return IsReserved;
        }
    }
}
