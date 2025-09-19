using CarLoanManagement.Data;
using CarLoanManagement.Services;
using Xunit;

namespace CarLoanManagement.Tests.Services
{
    public class LoanServiceTests
    {
        [Fact]
        public async Task PayoffLoanAsync_WithValidCarId_ReturnsTrueAndUpdatesLoan()
        {
            // Arrange
            var loanService = new LoanService();
            const int carId = 1;
            const string paidOffBy = "Test User";

            // Ensure loan is not paid off initially
            var initialLoan = await loanService.GetLoanByCarIdAsync(carId);
            if (initialLoan != null)
            {
                initialLoan.IsPaidOff = false;
                initialLoan.PaidOffBy = null;
                initialLoan.PaidOffDate = null;
            }

            // Act
            var result = await loanService.PayoffLoanAsync(carId, paidOffBy);

            // Assert
            Assert.True(result);

            var updatedLoan = await loanService.GetLoanByCarIdAsync(carId);
            Assert.NotNull(updatedLoan);
            Assert.True(updatedLoan.IsPaidOff);
            Assert.Equal(paidOffBy, updatedLoan.PaidOffBy);
            Assert.NotNull(updatedLoan.PaidOffDate);
        }

        [Fact]
        public async Task PayoffLoanAsync_WithInvalidCarId_ReturnsFalse()
        {
            // Arrange
            var loanService = new LoanService();

            // Act
            var result = await loanService.PayoffLoanAsync(999, "Test User");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetLoanByCarIdAsync_WithValidCarId_ReturnsLoan()
        {
            // Arrange
            var loanService = new LoanService();

            // Act
            var result = await loanService.GetLoanByCarIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CarId);
        }

        [Fact]
        public async Task GetLoanByCarIdAsync_WithInvalidCarId_ReturnsNull()
        {
            // Arrange
            var loanService = new LoanService();

            // Act
            var result = await loanService.GetLoanByCarIdAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}