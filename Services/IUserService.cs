using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public interface IUserService
    {
        Task LoadUsersAsync(); // Load users from file
        Task SaveUsersAsync(); // Save users to file
        Task<bool> AuthenticateAsync(Users user); // Authenticate user credentials
        string HashPassword(string password);
        Task ViewUsersJsonAsync();// Hash a password for secure storage
    }
}
