using System.Text.Json;
using TransactionTracker.Model;
using System.Linq;
using System.IO;
using TransactionTracker.Services;

public class TransactionService : ITransactionService
{
    private static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "Transactions.json");

    #region TransactionCrud
    public TransactionService() { }

    // Fetch all transactions
    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        return await Task.Run(() => LoadTransactions());
    }

    // Add a new transaction
    public async Task AddTransactionAsync(Transaction transaction)
    {
        var transactions = LoadTransactions();

        if (transaction.TransactionType == "Debt")
        {
            AddDebtToTransaction(transaction);
        }
        else if (transaction.TransactionType == "Credit")
        {
            AddInflowToTransaction(transaction);
        }

        transactions.Add(transaction);
        await Task.Run(() => SaveTransactions(transactions));
    }

    private void AddDebtToTransaction(Transaction transaction)
    {
        transaction.Notes = "Added as Debt";
        transaction.DueDate = DateTime.Now.AddDays(30); // Example Due Date
    }

    private void AddInflowToTransaction(Transaction transaction)
    {
        transaction.Notes = "Added as Credit";
    }

    // Update an existing transaction
    public async Task UpdateTransactionAsync(Transaction updatedTransaction)
    {
        var transactions = LoadTransactions();
        var transaction = transactions.FirstOrDefault(t => t.TransactionId == updatedTransaction.TransactionId);

        if (transaction != null)
        {
            transaction.TransactionType = updatedTransaction.TransactionType;
            transaction.Amount = updatedTransaction.Amount;
            transaction.Notes = updatedTransaction.Notes;
            transaction.TransactionDate = updatedTransaction.TransactionDate;

            await Task.Run(() => SaveTransactions(transactions));
        }
    }
    

    // Delete a transaction
    public async Task DeleteTransactionAsync(Guid transactionId)
    {
        var transactions = LoadTransactions();
        var transactionToRemove = transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        if (transactionToRemove != null)
        {
            transactions.Remove(transactionToRemove);
        }

        await Task.Run(() => SaveTransactions(transactions));
    }

    private List<Transaction> LoadTransactions()
    {
        if (!File.Exists(filePath))
        {
            SaveTransactions(new List<Transaction>());
            return new List<Transaction>();
        }

        try
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }
        catch
        {
            return new List<Transaction>();
        }
    }

    private void SaveTransactions(List<Transaction> transactions)
    {
        var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
    {
        var transactions = LoadTransactions();
        var transaction = transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        return await Task.FromResult(transaction);
    }
#endregion
    #region DashboardCalculations
    public async Task<DashboardSummary> GetDashboardSummaryAsync(DateTime? startDate, DateTime? endDate)
    {
        var transactions = await GetAllTransactionsAsync();

        if (startDate.HasValue && endDate.HasValue)
        {
            transactions = transactions.Where(t => t.TransactionDate >= startDate.Value && t.TransactionDate <= endDate.Value).ToList();
        }

        var totalInflows = transactions.Where(t => t.TransactionType == "Credit").Sum(t => t.Amount);
        var totalOutflows = transactions.Where(t => t.TransactionType == "Debit").Sum(t => t.Amount);
        var totalDebts = transactions.Where(t => t.TransactionType == "Debt" && t.Notes=="Cleared").Sum(t => t.Amount);

        decimal totalBalance = totalInflows + totalDebts - totalOutflows;

        var highestInflow = transactions.Where(t => t.TransactionType == "Credit").Max(t => t.Amount);
        var lowestInflow = transactions.Where(t => t.TransactionType == "Credit").Min(t => t.Amount);

        var highestOutflow = transactions.Where(t => t.TransactionType == "Debit").Max(t => t.Amount);
        var lowestOutflow = transactions.Where(t => t.TransactionType == "Debit").Min(t => t.Amount);

        var highestDebt = transactions.Where(t => t.TransactionType == "Debt").Max(t => t.Amount);
        var lowestDebt = transactions.Where(t => t.TransactionType == "Debt").Min(t => t.Amount);

        var pendingDebts = transactions
            .Where(t => t.TransactionType == "Debt" && !string.IsNullOrEmpty(t.Notes) && !t.Notes.Trim().Equals("Cleared", StringComparison.OrdinalIgnoreCase))
            .ToList();

        decimal clearedDebt = 0;
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
    #endregion
}
