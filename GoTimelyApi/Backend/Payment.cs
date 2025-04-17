using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoTimelyAPI
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        
        // Foreign key for Ticket
        [ForeignKey("Ticket")]
        public int TicketID { get; set; }
        public Ticket Ticket { get; set; } // Navigation property

        public decimal Amount { get; set; }
        
        public string PaymentMethod { get; set; }
        
        public string Status { get; set; }

        // Parameterless constructor required by EF Core
        public Payment() { }

        public Payment(int ticketID, decimal amount, string paymentMethod, string status)
        {
            TicketID = ticketID;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = status;
        }

        // Optional: Public methods to process or refund payment
        public bool ProcessPayment()
        {
            Status = "Processed";
            return true;
        }

        public bool RefundPayment()
        {
            Status = "Refunded";
            return true;
        }

        public void GetPaymentDetails()
        {
            Console.WriteLine($"Payment ID: {PaymentID}, Ticket: {TicketID}, Amount: {Amount:C}, Method: {PaymentMethod}, Status: {Status}");
        }
    }
}
