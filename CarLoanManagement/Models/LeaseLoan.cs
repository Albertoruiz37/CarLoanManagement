namespace CarLoanManagement.Models
{
    /// <summary>
    /// Represents a vehicle lease agreement.
    /// Inherits from Loan base class, demonstrating polymorphism and inheritance.
    /// Contains lease-specific business logic and calculations.
    /// Follows the Single Responsibility Principle by handling only lease-related functionality.
    /// </summary>
    public class LeaseLoan : Loan
    {
        /// <summary>
        /// Implementation of abstract property from base class.
        /// Returns the specific loan type identifier for lease agreements.
        /// </summary>
        public override string LoanType => "Lease";

        /// <summary>
        /// Fixed monthly lease payment amount
        /// </summary>
        public decimal MonthlyPayment { get; set; }

        /// <summary>
        /// Total lease term duration in months
        /// </summary>
        public int LeaseTermMonths { get; set; }

        /// <summary>
        /// Estimated vehicle value at the end of the lease term.
        /// Used for calculating early termination penalties and buyout options.
        /// </summary>
        public decimal ResidualValue { get; set; }

        /// <summary>
        /// Calculates the penalty fee for early lease termination.
        /// Demonstrates business rule encapsulation within the domain model.
        /// Uses industry-standard calculation (50% of remaining payments).
        /// </summary>
        /// <returns>Early termination fee in dollars, or 0 if lease is complete</returns>
        public decimal CalculateEarlyTerminationFee()
        {
            var elapsedMonths = GetElapsedMonths();
            var remainingMonths = LeaseTermMonths - elapsedMonths;

            // No fee if lease term is complete or exceeded
            if (remainingMonths <= 0) return 0;

            // Industry standard: 50% of remaining monthly payments
            return remainingMonths * MonthlyPayment * 0.5m;
        }

        /// <summary>
        /// Helper method to calculate elapsed months since lease start.
        /// Encapsulates the time calculation logic for reuse within the class.
        /// </summary>
        /// <returns>Number of months elapsed since lease start date</returns>
        private int GetElapsedMonths()
        {
            var elapsed = DateTime.Now - StartDate;
            // Approximate calculation: 30 days per month
            // In production, this would use more precise date arithmetic
            return (int)(elapsed.TotalDays / 30);
        }
    }
}