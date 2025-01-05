using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTracker.Model
{
    public class Debt
    {
        public Guid DebtId { get; set; }
        public string DebtName { get; set; }
        public string DebtSource { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DueDate { get; set; }
        [ForeignKey("Transaction")]
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }

    }
}
