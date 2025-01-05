using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.JsonHandler
{
    public class DebtBase
    {
        // File path for debts
        protected static readonly string DebtsFilePath = Path.Combine(FileSystem.AppDataDirectory, "Debts.json");

        // Load Debts from file
        public async Task<List<Debt>> LoadDebtsAsync()
        {
            if (File.Exists(DebtsFilePath))
            {
                var json = await File.ReadAllTextAsync(DebtsFilePath);
                return JsonSerializer.Deserialize<List<Debt>>(json) ?? new List<Debt>();
            }
            return new List<Debt>();
        }

        // Save Debts to file
        public async Task SaveDebtsAsync(List<Debt> debts)
        {
            var json = JsonSerializer.Serialize(debts, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(DebtsFilePath, json);
        }
    }
}
