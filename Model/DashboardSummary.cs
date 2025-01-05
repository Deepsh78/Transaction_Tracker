using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTracker.Model
{
    public class DashboardSummary
    {
        public int TotalTransactions { get; set; }
        public decimal TotalBalance { get; set; } // Inflows + Debts - Outflows
        public decimal TotalInflows { get; set; }
        public decimal TotalOutflows { get; set; }
        public decimal TotalDebts { get; set; }
        public decimal ClearedDebt { get; set; }
        public decimal RemainingDebt { get; set; }
        public decimal HighestInflow { get; set; }
        public decimal LowestInflow { get; set; }
        public decimal HighestOutflow { get; set; }
        public decimal LowestOutflow { get; set; }
        public decimal HighestDebt { get; set; }
        public decimal LowestDebt { get; set; }
        public List<Transaction> PendingDebts { get; set; }
    }

}
