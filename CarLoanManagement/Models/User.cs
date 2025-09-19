namespace CarLoanManagement.Models
{
    /// <summary>
    /// Represents a user in the car loan management system.
    /// Follows the Single Responsibility Principle by only handling user-related data.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username used for authentication
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password for authentication (in production, this would be hashed)
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Full display name of the user
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Collection of cars owned by this user.
        /// Demonstrates composition relationship between User and Car entities.
        /// </summary>
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}