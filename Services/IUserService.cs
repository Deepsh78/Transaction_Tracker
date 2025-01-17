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
        // Initialize users, loading from a file if it exists
        Task InitializeAsync();

        // Load users from the JSON file
        Task LoadUsersAsync();

        // Save users to the JSON file
        Task SaveUsersAsync();

        // Authenticate the user (verify username and password)
        Task<bool> AuthenticateAsync(Users user);

        Task LogoutAsync();
            }
}
