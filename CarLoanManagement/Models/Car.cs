namespace CarLoanManagement.Models
{
    /// <summary>
    /// Represents a vehicle in the car loan management system.
    /// Demonstrates the composition pattern with Loan entity.
    /// Follows SRP by only handling car-specific properties.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Unique identifier for the car
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Vehicle manufacturer (e.g., Toyota, BMW, Tesla)
        /// </summary>
        public string Make { get; set; } = string.Empty;

        /// <summary>
        /// Vehicle model name (e.g., Camry, 330i, Model 3)
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Model year of the vehicle
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Vehicle Identification Number - unique identifier for the physical vehicle
        /// </summary>
        public string VIN { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key reference to the owning user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Associated loan for this vehicle (if any).
        /// Can be null if the vehicle is owned outright.
        /// Demonstrates optional composition relationship.
        /// </summary>
        public Loan? Loan { get; set; }
    }
}