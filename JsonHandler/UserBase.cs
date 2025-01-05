using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.JsonHandler
{
    public class UserBase
    {
        protected static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "Users.json");

        public List<Users> LoadUser()
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found, returning empty list.");
                return new List<Users>(); // Return an empty list if the file doesn't exist
            }

            // Read the content of the file
            var json = File.ReadAllText(filePath);

            // Print the file path for debugging
            Console.WriteLine("File path: " + filePath);

            // Deserialize the JSON content into a list of User objects
            var users = JsonSerializer.Deserialize<List<Users>>(json);

            // If deserialization fails, log it
            if (users == null)
            {
                Console.WriteLine("Error: Failed to deserialize users from the JSON file.");
                return new List<Users>(); // Return an empty list if deserialization fails
            }

            return users;
        }

        // Save users to the JSON file
        public void SaveUser(IEnumerable<Users> users)
        {
            Console.WriteLine("File path: " + Path.Combine(FileSystem.AppDataDirectory, "Users.json")); var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });

            // Print the file path for debugging
            Console.WriteLine("Saving users to file at: " + filePath);

            // Write the JSON content to the file
            File.WriteAllText(filePath, json);
        }
    }
}
