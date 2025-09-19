using CarLoanManagement.Data;
using CarLoanManagement.Models;
using CarLoanManagement.Services.Interfaces;

namespace CarLoanManagement.Services
{
    /// <summary>
    /// Service class responsible for user-related business operations.
    /// Implements IUserService interface, demonstrating Dependency Inversion Principle.
    /// Follows Single Responsibility Principle by handling only user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Authenticates a user with username and password credentials.
        /// In a production system, passwords would be hashed and salted.
        /// </summary>
        /// <param name="username">Username for authentication</param>
        /// <param name="password">Password for authentication (plain text in demo)</param>
        /// <returns>User object if authentication successful, null otherwise</returns>
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            // Simulate async database operation
            await Task.Delay(1);

            // Simple credential validation (production would use hashed passwords)
            return InMemoryDataStore.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// Demonstrates separation of concerns - service handles business logic,
        /// data store handles data access.
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>User object if found, null otherwise</returns>
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            // Simulate async database operation
            await Task.Delay(1);

            return InMemoryDataStore.Users.FirstOrDefault(u => u.Id == userId);
        }

        /// <summary>
        /// Retrieves all users in the system.
        /// Returns a new list to prevent external modification of internal data.
        /// </summary>
        /// <returns>List of all users in the system</returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            // Simulate async database operation
            await Task.Delay(1);

            // Return a copy to prevent external modification
            return InMemoryDataStore.Users.ToList();
        }
    }
}