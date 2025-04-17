using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoTimelyAPI
{
    public class TimeTable
    {
        [Key]
        public int Id { get; set; } // Adding a primary key for EF Core to track entities

        [Required]
        public int BusNumber { get; set; }

        // Foreign Key to Bus
        [ForeignKey("BusNumber")]
        public Bus Bus { get; set; } // Navigation property to Bus

        [Required]
        [MaxLength(10)]
        public string Day { get; set; }

        [Required]
        [MaxLength(100)]
        public string Station { get; set; }

        [Required]
        public TimeSpan ArrivalTime { get; set; }

        [Range(0, 180)]
        public int Delay { get; set; } // in minutes

        // Constructor
        public TimeTable(int busNumber, string day, string station, TimeSpan arrivalTime, int delay)
        {
            BusNumber = busNumber;
            Day = day;
            Station = station;
            ArrivalTime = arrivalTime;
            Delay = delay;
        }

        // Method to get adjusted schedule with delay
        public string GetSchedule()
        {
            var adjustedTime = ArrivalTime.Add(TimeSpan.FromMinutes(Delay));
            return $"Bus {BusNumber} arrives at {Station} on {Day} at {adjustedTime:hh\\:mm} (Delayed by {Delay} min)";
        }
    }
}
