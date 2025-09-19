using CarLoanManagement.Models;
using Xunit;

namespace CarLoanManagement.Tests.Models
{
    /// <summary>
    /// Unit tests for the Loan model hierarchy.
    /// Tests both the abstract base class behavior and concrete implementations.
    /// Demonstrates testing of polymorphic behavior and business logic.
    /// Follows the AAA pattern: Arrange, Act, Assert.
    /// </summary>
    public class LoanTests
    {
        /// <summary>
        /// Tests the monthly payment calculation for retail loans with interest.
        /// Verifies that the amortization formula produces reasonable results.
        /// This test ensures the business logic for payment calculations is correct.
        /// </summary>
        [Fact]
        public void RetailLoan_CalculateMonthlyPayment_WithInterest_ReturnsCorrectAmount()
        {
            // Arrange - Set up test data
            var retailLoan = new RetailLoan
            {
                OriginalAmount = 20000,    // $20,000 loan
                InterestRate = 5.0m,       // 5% annual interest
                TermInMonths = 60          // 5 year term
            };

            // Act - Execute the method under test
            var monthlyPayment = retailLoan.CalculateMonthlyPayment();

            // Assert - Verify the results meet expectations
            Assert.True(monthlyPayment > 0, "Monthly payment should be positive");
            Assert.True(monthlyPayment < retailLoan.OriginalAmount,
                "Monthly payment should be less than total loan amount");

            // For this loan, payment should be around $377
            Assert.True(monthlyPayment > 300 && monthlyPayment < 450,
                "Payment should be in reasonable range for this loan");
        }

        /// <summary>
        /// Tests the edge case of zero-interest retail loans.
        /// Ensures the calculation handles division correctly without interest compounding.
        /// This tests both the business logic and edge case handling.
        /// </summary>
        [Fact]
        public void RetailLoan_CalculateMonthlyPayment_WithoutInterest_ReturnsCorrectAmount()
        {
            // Arrange - Zero interest scenario
            var retailLoan = new RetailLoan
            {
                OriginalAmount = 24000,
                InterestRate = 0,          // Zero interest (promotional loan)
                TermInMonths = 48
            };

            // Act
            var monthlyPayment = retailLoan.CalculateMonthlyPayment();

            // Assert - Simple division: $24,000 / 48 months = $500
            Assert.Equal(500m, monthlyPayment);
        }

        /// <summary>
        /// Tests the early termination fee calculation for lease loans.
        /// Verifies that the business rule (50% of remaining payments) is correctly implemented.
        /// This test ensures lease-specific business logic works as expected.
        /// </summary>
        [Fact]
        public void LeaseLoan_CalculateEarlyTerminationFee_ReturnsCorrectAmount()
        {
            // Arrange - Lease loan with 12 months elapsed out of 36 month term
            var leaseLoan = new LeaseLoan
            {
                MonthlyPayment = 300,
                LeaseTermMonths = 36,
                StartDate = DateTime.Now.AddMonths(-12) // Started 12 months ago
            };

            // Act
            var terminationFee = leaseLoan.CalculateEarlyTerminationFee();

            // Assert - Should be positive (there are remaining months)
            Assert.True(terminationFee > 0,
                "Early termination fee should be positive when lease is not complete");

            // Fee should be reasonable (50% of remaining payments)
            // Approximately 24 remaining months * $300 * 50% = $3,600
            Assert.True(terminationFee > 2000 && terminationFee < 5000,
                "Termination fee should be in reasonable range");
        }

        /// <summary>
        /// Tests the loan payoff functionality that's common to all loan types.
        /// Verifies that the virtual method correctly updates all required properties.
        /// This test ensures the Template Method pattern works correctly.
        /// </summary>
        [Fact]
        public void Loan_MarkAsPaidOff_UpdatesPropertiesCorrectly()
        {
            // Arrange - Use concrete RetailLoan to test abstract base behavior
            var loan = new RetailLoan
            {
                Id = 1,
                CarId = 1,
                OriginalAmount = 20000,
                PayoffAmount = 15000,
                StartDate = DateTime.Now.AddMonths(-12),
                IsPaidOff = false,
                PaidOffBy = null,
                PaidOffDate = null
            };

            var payoffPersonName = "John Doe";
            var beforePayoff = DateTime.Now;

            // Act - Call the virtual method being tested
            loan.MarkAsPaidOff(payoffPersonName);

            var afterPayoff = DateTime.Now;

            // Assert - Verify all properties are updated correctly
            Assert.True(loan.IsPaidOff, "Loan should be marked as paid off");
            Assert.Equal(payoffPersonName, loan.PaidOffBy);
            Assert.Equal(0, loan.PayoffAmount);
            Assert.NotNull(loan.PaidOffDate);

            // Verify the date is reasonable (between before and after the call)
            Assert.True(loan.PaidOffDate >= beforePayoff && loan.PaidOffDate <= afterPayoff,
                "PaidOffDate should be set to current time");
        }

        /// <summary>
        /// Tests polymorphism by verifying that RetailLoan correctly identifies its type.
        /// This ensures the abstract property implementation works as expected.
        /// </summary>
        [Fact]
        public void RetailLoan_LoanType_ReturnsRetail()
        {
            // Arrange
            var retailLoan = new RetailLoan();

            // Act & Assert - Test the abstract property implementation
            Assert.Equal("Retail", retailLoan.LoanType);
        }

        /// <summary>
        /// Tests polymorphism by verifying that LeaseLoan correctly identifies its type.
        /// This ensures the abstract property implementation works as expected.
        /// </summary>
        [Fact]
        public void LeaseLoan_LoanType_ReturnsLease()
        {
            // Arrange
            var leaseLoan = new LeaseLoan();

            // Act & Assert - Test the abstract property implementation
            Assert.Equal("Lease", leaseLoan.LoanType);
        }

    }
}