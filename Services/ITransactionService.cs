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
        Task UpdateTransactionAsync(Transaction updatedTransaction);
        Task DeleteTransactionAsync(Guid transactionId);
        Task<DashboardSummary> GetDashboardSummaryAsync(DateTime? startDate, DateTime? endDate);
        Task<Transaction> GetTransactionByIdAsync(Guid transactionId);
        
    }

}
