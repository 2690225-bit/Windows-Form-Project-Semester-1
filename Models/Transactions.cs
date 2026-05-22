using System;
using System.Collections.Generic;

namespace POSSystem.Models
{
    public class Transaction
    {
        // Transaction ID 
        public string TransactionId { get; set; } = string.Empty;

        // When the transaction occurred
        public DateTime Date { get; set; } = DateTime.Now;

        // The customer's ID 
        public string CustomerId { get; set; } = string.Empty;

        // The customer's name at time of sale 
        public string CustomerName { get; set; } = "Walk-in";

        // A list of what was purchased
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        // The subtotal, tax and total amount of money
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        // How the customer paid
        public string PaymentMethod { get; set; } = string.Empty;

        // Status of the transaction
        public string Status { get; set; } = "Completed";

        // If this was saved while offline, it needs to be synced later
        public bool IsPendingSync { get; set; } = false;

        // Display of what it will look like in the Transaction Log list
        public override string ToString() =>
            $"{TransactionId} | {CustomerName} | ${Total:F2} | {PaymentMethod} | {Status}";
    }
}
