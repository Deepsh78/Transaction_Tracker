using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.JsonHandler
{
    public class OutflowBase
    {
        // File path for outflows
        protected static readonly string OutflowsFilePath = Path.Combine(FileSystem.AppDataDirectory, "Outflows.json");

        // Load Outflows from file
        public async Task<List<Outflow>> LoadOutflowsAsync()
        {
            if (File.Exists(OutflowsFilePath))
            {
                var json = await File.ReadAllTextAsync(OutflowsFilePath);
                return JsonSerializer.Deserialize<List<Outflow>>(json) ?? new List<Outflow>();
            }
            return new List<Outflow>();
        }

        // Save Outflows to file
        public async Task SaveOutflowsAsync(List<Outflow> outflows)
        {
            var json = JsonSerializer.Serialize(outflows, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(OutflowsFilePath, json);
        }
    }
}
