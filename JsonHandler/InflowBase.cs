using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.JsonHandler
{
    public class InflowBase
    {
        // File path for inflows
        protected static readonly string InflowsFilePath = Path.Combine(FileSystem.AppDataDirectory, "Inflows.json");

        // Load Inflows from file
        public async Task<List<Inflow>> LoadInflowsAsync()
        {
            if (File.Exists(InflowsFilePath))
            {
                var json = await File.ReadAllTextAsync(InflowsFilePath);
                return JsonSerializer.Deserialize<List<Inflow>>(json) ?? new List<Inflow>();
            }
            return new List<Inflow>();
        }

        // Save Inflows to file
        public async Task SaveInflowsAsync(List<Inflow> inflows)
        {
            var json = JsonSerializer.Serialize(inflows, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(InflowsFilePath, json);
        }
    }
}
