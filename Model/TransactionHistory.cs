using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTracker.Model
{
    public class TransactionHistory
    {
        public Guid HistoryId { get; set; }
        public string HistoryName { get; set; }
        public ICollection<Transaction> Transactions { get; set; }  // Navigation property
    }
}
