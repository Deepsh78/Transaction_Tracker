using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.JsonHandler
{
    public class TransactionBase
    {
        protected static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "Transactions.json");

        public List<Transaction> LoadTransactions()
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found, returning empty list.");
                return new List<Transaction>(); // Return an empty list if the file doesn't exist
            }

            // Read the content of the file
            var json = File.ReadAllText(filePath);

            // Print the file path for debugging
            Console.WriteLine("File path: " + filePath);

            // Deserialize the JSON content into a list of Transaction objects
            var transactions = JsonSerializer.Deserialize<List<Transaction>>(json);

            // If deserialization fails, log it
            if (transactions == null)
            {
                Console.WriteLine("Error: Failed to deserialize transactions from the JSON file.");
                return new List<Transaction>(); // Return an empty list if deserialization fails
            }

            return transactions;
        }

        // Save transactions to the JSON file
        public void SaveTransactions(IEnumerable<Transaction> transactions)
        {
            // Serialize the transactions list to JSON
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

            // Print the file path for debugging
            Console.WriteLine("Saving transactions to file at: " + filePath);

            // Write the JSON content to the file
            File.WriteAllText(filePath, json);
        }
    }
}
