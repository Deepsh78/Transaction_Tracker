using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(Transaction transaction);
            
        Task<DashboardSummary> GetDashboardSummaryAsync(DateTime? startDate, DateTime? endDate);    }
}
