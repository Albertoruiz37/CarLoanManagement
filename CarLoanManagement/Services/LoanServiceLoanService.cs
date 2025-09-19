using CarLoanManagement.Data;
using CarLoanManagement.Models;
using CarLoanManagement.Services.Interfaces;

namespace CarLoanManagement.Services
{
    /// <summary>
    /// Service class responsible for loan-related business operations.
    /// Implements ILoanService interface following the Dependency Inversion Principle.
    /// Encapsulates loan business logic and maintains separation of concerns.
    /// </summary>
    public class LoanService : ILoanService
    {
        /// <summary>
        /// Processes the payoff of a loan for a specific vehicle.
        /// Implements the core business logic for loan completion.
        /// Includes validation to ensure business rules are followed.
        /// </summary>
        /// <param name="carId">Unique identifier of the car whose loan is being paid off</param>
        /// <param name="paidOffBy">Name of the person paying off the loan</param>
        /// <returns>True if payoff successful, false if loan not found or already paid</returns>
        public async Task<bool> PayoffLoanAsync(int carId, string paidOffBy)
        {
            // Simulate async database operation
            await Task.Delay(1);

            // Input validation
            if (string.IsNullOrWhiteSpace(paidOffBy))
            {
                throw new ArgumentException("PaidOffBy name cannot be empty", nameof(paidOffBy));
            }

            // Find the loan associated with the specified car
            var loan = InMemoryDataStore.Loans.FirstOrDefault(l => l.CarId == carId);

            // Validate loan exists and is not already paid off
            if (loan != null && !loan.IsPaidOff)
            {
                // Use the polymorphic MarkAsPaidOff method
                // This works for both RetailLoan and LeaseLoan due to inheritance
                loan.MarkAsPaidOff(paidOffBy.Trim());
                return true;
            }

            // Return false if loan not found or already paid off
            return false;
        }

        /// <summary>
        /// Retrieves the loan associated with a specific vehicle.
        /// Provides read-only access to loan information.
        /// </summary>
        /// <param name="carId">Unique identifier of the car</param>
        /// <returns>Loan object if found, null otherwise</returns>
        public async Task<Loan?> GetLoanByCarIdAsync(int carId)
        {
            // Simulate async database operation
            await Task.Delay(1);

            return InMemoryDataStore.Loans.FirstOrDefault(l => l.CarId == carId);
        }

        /// <summary>
        /// Updates an existing loan with new information.
        /// Implements the repository pattern for data persistence.
        /// In production, this would handle database updates and change tracking.
        /// </summary>
        /// <param name="loan">Loan object with updated information</param>
        /// <returns>Task representing the async operation</returns>
        public async Task UpdateLoanAsync(Loan loan)
        {
            // Simulate async database operation
            await Task.Delay(1);

            // Validate input
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }

            // Find existing loan in data store
            var existingLoan = InMemoryDataStore.Loans.FirstOrDefault(l => l.Id == loan.Id);
            if (existingLoan != null)
            {
                // Update key properties (in production, would use object mapping)
                existingLoan.PayoffAmount = loan.PayoffAmount;
                existingLoan.IsPaidOff = loan.IsPaidOff;
                existingLoan.PaidOffBy = loan.PaidOffBy;
                existingLoan.PaidOffDate = loan.PaidOffDate;
            }
        }
    }
}