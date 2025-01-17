using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTracker.Model
{
    public class Transaction
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();
        [Required]

        public string TransactionType { get; set; } // Debt, Debit, Credit

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]

        public decimal Amount { get; set; }
        [Required]

        public DateTime TransactionDate { get; set; }
        public string Notes { get; set; }
        public Guid? TagId { get; set; }
        public Tags Tag { get; set; }

        public string CustomTag { get; set; }

        // Add the Source property to store the source of the transaction
        public string Source { get; set; }

        // Add the DueDate property for Debt transactions
        public DateTime? DueDate { get; set; }

        // Other properties related to debts and inflows (Debt and Inflow collections can be added here)
        public List<Debt> Debts { get; set; } = new List<Debt>();
        public List<Inflow> Inflows { get; set; } = new List<Inflow>();
    }


}