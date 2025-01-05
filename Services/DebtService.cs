using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.JsonHandler;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public class DebtService : IDebtService
    {
        private readonly ITransactionService _transactionService;

        public DebtService(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Get a list of pending debts
        public async Task<List<Debt>> GetPendingDebtsAsync()
        {
            // Fetch all transactions
            var transactions = await _transactionService.GetAllTransactionsAsync();

            // Filter out pending debts
            return transactions
                .Where(t => t.TransactionType == "Debt" && t.Amount > 0)
                .Select(t => new Debt
                {
                    Amount = t.Amount,
                    TransactionId = t.TransactionId,
                    IsPaid = false

                })
                .ToList();
        }

        // Clear debt based on credit transactions
        //    public async Task ClearDebtFromInflowAsync(Transaction creditTransaction)
        //    {
        //        await _transactionService.ClearDebtsFromTransactionAsync(creditTransaction.TransactionId, availableCredit);
        //    }
        //}
    }
}
