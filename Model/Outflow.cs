using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTracker.Model
{
    public class Outflow
    {
        public Guid OutflowId { get; set; }
        [ForeignKey("Transaction")]
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }  // Navigation property
        public string OutflowType { get; set; }
        public decimal Amount { get; set; }

    }
}
