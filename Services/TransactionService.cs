using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "Transactions.json");

        public TransactionService() { }

        // Fetch all transactions
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await Task.Run(() => LoadTransactions());
        }

        // Add a new transaction
        public async Task AddTransactionAsync(Transaction transaction)
        {
            // Load existing transactions from the file
            var transactions = LoadTransactions();

            // Add the new transaction
            transactions.Add(transaction);

            // Save the updated transactions list to the JSON file asynchronously
            await Task.Run(() => SaveTransactions(transactions));
        }

        // Helper method to load transactions from the JSON file
        private List<Transaction> LoadTransactions()
        {
            if (!File.Exists(filePath))
            {
                return new List<Transaction>(); // If no file exists, return an empty list
            }

            var json = File.ReadAllText(filePath);
            var transactions = JsonSerializer.Deserialize<List<Transaction>>(json);
            return transactions ?? new List<Transaction>();
        }

        // Helper method to save transactions to the JSON file
        private void SaveTransactions(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        // Additional logic if needed for other transactions, e.g., clearing debts, summaries, etc.
        public async Task<DashboardSummary> GetDashboardSummaryAsync(DateTime? startDate, DateTime? endDate)
        {
            var transactions = await GetAllTransactionsAsync();

            // Handle date range filtering
            if (startDate.HasValue && endDate.HasValue)
            {
                transactions = transactions.Where(t => t.TransactionDate >= startDate.Value && t.TransactionDate <= endDate.Value).ToList();
            }

            // Perform calculations and other logic here (same as before)
            var totalInflows = transactions.Where(t => t.TransactionType == "Credit").Sum(t => t.Amount);
            var totalOutflows = transactions.Where(t => t.TransactionType == "Debit").Sum(t => t.Amount);
            var totalDebts = transactions.Where(t => t.TransactionType == "Debt").Sum(t => t.Amount);

            decimal totalBalance = totalInflows + totalDebts - totalOutflows;

            var highestInflow = transactions.Where(t => t.TransactionType == "Credit").Max(t => t.Amount);
            var lowestInflow = transactions.Where(t => t.TransactionType == "Credit").Min(t => t.Amount);

            var highestOutflow = transactions.Where(t => t.TransactionType == "Debit").Max(t => t.Amount);
            var lowestOutflow = transactions.Where(t => t.TransactionType == "Debit").Min(t => t.Amount);

            var highestDebt = transactions.Where(t => t.TransactionType == "Debt").Max(t => t.Amount);
            var lowestDebt = transactions.Where(t => t.TransactionType == "Debt").Min(t => t.Amount);

            var pendingDebts = transactions.Where(t => t.TransactionType == "Debt" && t.Amount > 0).ToList();

            // Assuming ClearedDebt is calculated here or fetched from somewhere
            decimal clearedDebt = 0; // Add actual calculation
            decimal remainingDebt = totalDebts - clearedDebt;

            return new DashboardSummary
            {
                TotalTransactions = transactions.Count,
                TotalBalance = totalBalance,
                TotalInflows = totalInflows,
                TotalOutflows = totalOutflows,
                TotalDebts = totalDebts,
                ClearedDebt = clearedDebt,
                RemainingDebt = remainingDebt,
                HighestInflow = highestInflow,
                LowestInflow = lowestInflow,
                HighestOutflow = highestOutflow,
                LowestOutflow = lowestOutflow,
                HighestDebt = highestDebt,
                LowestDebt = lowestDebt,
                PendingDebts = pendingDebts
            };
        }
    }
}
