using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.JsonHandler
{
    public class TagsBase
    {
        // Define the file path for storing tags
        protected static readonly string tagsFilePath = Path.Combine(FileSystem.AppDataDirectory, "Tags.json");

        // Load tags from the JSON file
        public List<Tags> LoadTags()
        {
            // Check if the file exists
            if (!File.Exists(tagsFilePath))
            {
                Console.WriteLine("Tags file not found, returning empty list.");
                return new List<Tags>(); // Return an empty list if the file doesn't exist
            }

            // Read the content of the file
            var json = File.ReadAllText(tagsFilePath);

            // Print the file path for debugging
            Console.WriteLine("Tags file path: " + tagsFilePath);

            // Deserialize the JSON content into a list of Tags objects
            var tags = JsonSerializer.Deserialize<List<Tags>>(json);

            // If deserialization fails, log it
            if (tags == null)
            {
                Console.WriteLine("Error: Failed to deserialize tags from the JSON file.");
                return new List<Tags>(); // Return an empty list if deserialization fails
            }

            return tags;
        }

        // Save tags to the JSON file
        public void SaveTags(IEnumerable<Tags> tags)
        {
            var json = JsonSerializer.Serialize(tags, new JsonSerializerOptions { WriteIndented = true });

            // Print the file path for debugging
            Console.WriteLine("Saving tags to file at: " + tagsFilePath);

            // Write the JSON content to the file
            File.WriteAllText(tagsFilePath, json);
        }

    }
}
