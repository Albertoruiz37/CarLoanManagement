namespace CarLoanManagement.Models
{
    /// <summary>
    /// Represents a traditional retail financing loan for vehicle purchases.
    /// Inherits from Loan base class, demonstrating the Liskov Substitution Principle -
    /// RetailLoan can be used anywhere a Loan is expected.
    /// Contains retail-specific business logic for payment calculations.
    /// </summary>
    public class RetailLoan : Loan
    {
        /// <summary>
        /// Implementation of abstract property from base class.
        /// Returns the specific loan type identifier.
        /// </summary>
        public override string LoanType => "Retail";

        /// <summary>
        /// Annual interest rate as a percentage (e.g., 4.5 for 4.5%)
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Total loan term in months (e.g., 60 for 5 years)
        /// </summary>
        public int TermInMonths { get; set; }

        /// <summary>
        /// Calculates the monthly payment amount using standard loan amortization formula.
        /// Demonstrates encapsulation of business logic within the appropriate domain model.
        /// Handles both interest-bearing and zero-interest loans.
        /// </summary>
        /// <returns>Monthly payment amount in dollars</returns>
        public decimal CalculateMonthlyPayment()
        {
            if (InterestRate == 0)
                return OriginalAmount / TermInMonths;

            var monthlyRate = (double)InterestRate / 100.0 / 12.0;

            var power = Math.Pow(1 + monthlyRate, TermInMonths);
            var numerator = (double)OriginalAmount * monthlyRate * power;
            var denominator = power - 1;

            return (decimal)(numerator / denominator);
        }
    }
}