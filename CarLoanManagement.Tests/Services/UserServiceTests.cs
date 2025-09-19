using CarLoanManagement.Services;
using Xunit;

namespace CarLoanManagement.Tests.Services
{
    /// <summary>
    /// Unit tests for the UserService class.
    /// Tests the service layer business logic in isolation.
    /// Validates authentication logic and user retrieval functionality.
    /// Uses the in-memory data store for consistent, repeatable tests.
    /// </summary>
    public class UserServiceTests
    {
        /// <summary>
        /// Tests successful user authentication with valid credentials.
        /// Verifies that the service correctly validates credentials and returns user data.
        /// This test ensures the happy path of authentication works correctly.
        /// </summary>
        [Fact]
        public async Task AuthenticateAsync_WithValidCredentials_ReturnsUser()
        {
            // Arrange - Create service instance and use known test credentials
            var userService = new UserService();
            var expectedUsername = "john";
            var expectedPassword = "password123";

            // Act - Attempt authentication with valid credentials
            var result = await userService.AuthenticateAsync(expectedUsername, expectedPassword);

            // Assert - Verify successful authentication
            Assert.NotNull(result);
            Assert.Equal(expectedUsername, result.Username);
            Assert.Equal("John Doe", result.FullName);
            Assert.Equal(1, result.Id); // John's user ID from test data
        }

        /// <summary>
        /// Tests authentication failure with invalid credentials.
        /// Verifies that the service rejects invalid login attempts.
        /// This test ensures security by confirming failed authentication returns null.
        /// </summary>
        [Fact]
        public async Task AuthenticateAsync_WithInvalidCredentials_ReturnsNull()
        {
            // Arrange
            var userService = new UserService();

            // Act - Test various invalid credential combinations
            var resultInvalidUser = await userService.AuthenticateAsync("invalid", "password123");
            var resultInvalidPassword = await userService.AuthenticateAsync("john", "wrongpassword");
            var resultBothInvalid = await userService.AuthenticateAsync("invalid", "invalid");

            // Assert - All should return null (authentication failed)
            Assert.Null(resultInvalidUser);
            Assert.Null(resultInvalidPassword);
            Assert.Null(resultBothInvalid);
        }

        /// <summary>
        /// Tests user retrieval by ID with a valid user identifier.
        /// Verifies that the service can locate and return user data by ID.
        /// This test ensures the user lookup functionality works correctly.
        /// </summary>
        [Fact]
        public async Task GetUserByIdAsync_WithValidId_ReturnsUser()
        {
            // Arrange
            var userService = new UserService();
            var validUserId = 1; // John's ID from test data

            // Act
            var result = await userService.GetUserByIdAsync(validUserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(validUserId, result.Id);
            Assert.Equal("John Doe", result.FullName);
            Assert.Equal("john", result.Username);
            Assert.NotEmpty(result.Cars); // John should have cars in test data
        }

        /// <summary>
        /// Tests user retrieval by ID with an invalid identifier.
        /// Verifies that the service handles non-existent users gracefully.
        /// This test ensures proper error handling for edge cases.
        /// </summary>
        [Fact]
        public async Task GetUserByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var userService = new UserService();
            var invalidUserId = 999; // Non-existent user ID

            // Act
            var result = await userService.GetUserByIdAsync(invalidUserId);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAllUsers functionality.
        /// Verifies that the service returns all users in the system.
        /// This test ensures administrative/reporting functionality works correctly.
        /// </summary>
        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var userService = new UserService();

            // Act
            var result = await userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            // Should have at least the two test users (john and jane)
            Assert.True(result.Count >= 2, "Should have at least 2 users from test data");

            // Verify test users are present
            Assert.Contains(result, u => u.Username == "john");
            Assert.Contains(result, u => u.Username == "jane");
        }

        /// <summary>
        /// Tests authentication with edge case inputs (empty/null values).
        /// Verifies that the service handles invalid input gracefully without throwing exceptions.
        /// This test ensures robust error handling and input validation.
        /// </summary>
        [Fact]
        public async Task AuthenticateAsync_WithEdgeCaseInputs_HandlesGracefully()
        {
            // Arrange
            var userService = new UserService();

            // Act & Assert - Test various edge cases
            var nullUsernameResult = await userService.AuthenticateAsync(null!, "password");
            Assert.Null(nullUsernameResult);

            var nullPasswordResult = await userService.AuthenticateAsync("john", null!);
            Assert.Null(nullPasswordResult);

            var emptyUsernameResult = await userService.AuthenticateAsync("", "password");
            Assert.Null(emptyUsernameResult);

            var emptyPasswordResult = await userService.AuthenticateAsync("john", "");
            Assert.Null(emptyPasswordResult);

            var whitespaceUsernameResult = await userService.AuthenticateAsync("   ", "password");
            Assert.Null(whitespaceUsernameResult);
        }
    }
}