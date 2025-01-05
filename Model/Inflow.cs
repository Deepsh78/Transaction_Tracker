using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTracker.Model
{
    public class Inflow
    {
        public Guid InflowId { get; set; }
        public string InflowType { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        [ForeignKey("Transaction")]
        public string TransactionId { get; set; }
        public Transaction Transaction { get; set; }  // Navigation property
    }
}
