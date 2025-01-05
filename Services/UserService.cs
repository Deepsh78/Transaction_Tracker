using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.Helper;
using TransactionTracker.JsonHandler;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public class UserService : UserBase, IUserService
    {
        private List<Users> _users = new(); // List to hold users

        // Constructor that initializes the user service and calls the InitializeAsync method
        public UserService()
        {
            _ = InitializeAsync();
        }

        // Method to display the content of the JSON file for debugging purposes
        public async Task ViewUsersJsonAsync()
        {
            if (File.Exists(filePath))
            {
                // Read the file content asynchronously
                var jsonContent = await File.ReadAllTextAsync(filePath);

                // Print the content (or return it if you want to display in the UI)
                Console.WriteLine("Users JSON File Content:");
                Console.WriteLine(jsonContent);
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }
        }

        // Initialization method that seeds the default user if the file doesn't exist
        public async Task InitializeAsync()
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                // Seed with default user
                var defaultUser = new Users
                {
                    UserName = "admin",
                    Password = HashPassword("password"), // Store hashed password
                    Currency = "USD"
                };
                _users.Add(defaultUser);

                // Save users to the JSON file
                await SaveUsersAsync();
            }
            else
            {
                // Load users from the file if it exists
                await LoadUsersAsync();
            }

            // Optionally view the JSON file content after initialization
            await ViewUsersJsonAsync();
        }

        // Method to load users from the file
        public async Task LoadUsersAsync()
        {
            try
            {
                _users = await FileHelper.LoadFromFileAsync<List<Users>>(filePath) ?? new List<Users>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
                _users = new List<Users>(); // Handle cases where file is corrupted
            }
        }

        // Method to save users to the JSON file
        public async Task SaveUsersAsync()
        {
            await FileHelper.SaveToFileAsync(filePath, _users);
        }

        // Method to authenticate the user based on username and password
        public async Task<bool> AuthenticateAsync(Users user)
        {
            await Task.Delay(500); // Simulate authentication delay

            var existingUser = _users.FirstOrDefault(u =>
                u.UserName == user.UserName && u.Password == HashPassword(user.Password));

            // Print the file path for debugging
            Console.WriteLine($"File path: {filePath}");

            return existingUser != null;
        }

        // Method to hash passwords securely
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
