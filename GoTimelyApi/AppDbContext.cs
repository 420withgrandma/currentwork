using Microsoft.EntityFrameworkCore;
using GoTimelyAPI; // 👈 this gives access to Bus, Passenger, Ticket, etc.

namespace GoTimelyAPI // ✅ must match this exactly
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for the tables
        public DbSet<User> Users { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Seat> Seats { get; set; }  // Added Seat for seat reservation
        public DbSet<Admin> Admins { get; set; }  // Added Admin for admin management
        public DbSet<TimeTable> TimeTables { get; set; } // Added timetable for bus schedules

        // You can define relationships here if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships, keys, and constraints (if necessary)
            
            // User-Passenger Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Passengers)
                .WithOne() // Assuming no navigation property in User for Passengers
                .HasForeignKey(p => p.PassengerID); // Correct the foreign key reference
            
            // User-Ticket Relationship (assuming tickets are booked by User)
            modelBuilder.Entity<User>()
                .HasMany(u => u.BookedTickets)
                .WithOne() // Assuming no navigation property in User for BookedTickets
                .HasForeignKey(t => t.TicketNumber); // Correct the foreign key reference
            
            // Ticket-Passenger Relationship
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Passenger)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PassengerID); // Correct the foreign key reference to PassengerID

            // Payment-Ticket Relationship
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Ticket)
                .WithMany()
                .HasForeignKey(p => p.TicketID);

            // Seat-Bus Relationship
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Bus)
                .WithMany()
                .HasForeignKey(s => s.BusID); // Corrected BusID for Seat

            // TimeTable-Bus Relationship
            modelBuilder.Entity<TimeTable>()
                .HasOne(t => t.Bus)
                .WithMany()
                .HasForeignKey(t => t.BusNumber); // Corrected BusNumber for TimeTable
        }
    }
}
