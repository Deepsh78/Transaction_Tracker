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
        public Guid TransactionId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } // Adding date field
        public string Notes { get; set; }

        [ForeignKey("User")]
        public string UserName { get; set; }
        public Users User { get; set; }  // Navigation property

        // Foreign key for Tag (TagId)
        [ForeignKey("Tags")]
        public string TagId { get; set; }
        public Tags Tag { get; set; }  // Navigation property for Tag
        public string CustomTag { get; set; }  


        // Foreign key for TransactionHistory (HistoryId)
        [ForeignKey("TransactionHistory")]
        public string HistoryId { get; set; }
        public TransactionHistory History { get; set; }  // Navigation property for TransactionHistory
    }

}