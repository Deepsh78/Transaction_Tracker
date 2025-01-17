using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.Helper;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public class UserService : IUserService
    {
        private List<Users> _users = new();
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");
        private readonly NavigationManager _navigationManager;

        public UserService(NavigationManager navigationManager)
        {
            _ = InitializeAsync();
            _navigationManager = navigationManager;
        }

        // Initialize users and load from file if it exists
        public async Task InitializeAsync()
        {
            if (File.Exists(filePath))
            {
                await LoadUsersAsync();
            }
            else
            {
                // Seed the default user if no user exists
                _users = new List<Users>
                {
                    new Users
                    {
                        UserName = "admin", // Default Username
                        Password = HashPassword("admin123"), // Default Password
                    }
                };
                await SaveUsersAsync();
            }
        }

        // Load users from the JSON file
        public async Task LoadUsersAsync()
        {
            try
            {
                _users = await FileHelper.LoadFromFileAsync<List<Users>>(filePath) ?? new List<Users>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
                _users = new List<Users>();
            }
        }

        // Save users to the JSON file
        public async Task SaveUsersAsync()
        {
            await FileHelper.SaveToFileAsync(filePath, _users);
            Console.WriteLine("File path: " + filePath);

        }

        // Authenticate user and check for currency selection
        public async Task<bool> AuthenticateAsync(Users user)
        {
            var existingUser = _users.FirstOrDefault(u =>
                u.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == HashPassword(user.Password));

            return existingUser != null;
        }

        // Hash password securely
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public async Task LogoutAsync()
        {
            // Redirect to the login page
            _navigationManager.NavigateTo("/", true); // The 'true' forces a full page reload
        }
    }
}
