using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoTimelyAPI
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(510)]
        public string PasswordHash { get; set; }

        public string Phone { get; set; }

        // Optional: if you want to link to Passengers from here
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

        // Optional: if you want to show tickets directly from the user model
        public ICollection<Ticket> BookedTickets { get; set; } = new List<Ticket>();
    }
}
