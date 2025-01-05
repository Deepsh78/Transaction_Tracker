using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TransactionTracker.Helper
{
    public static class FileHelper
    {
        public static async Task<T?> LoadFromFileAsync<T>(string filePath)
        {
            if (!File.Exists(filePath)) return default;

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }

        public static async Task SaveToFileAsync<T>(string filePath, T data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
