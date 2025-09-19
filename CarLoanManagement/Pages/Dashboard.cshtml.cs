using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using CarLoanManagement.Models;
using CarLoanManagement.Services.Interfaces;

namespace CarLoanManagement.Pages
{
    /// <summary>
    /// Page model for the main dashboard functionality.
    /// Requires user authentication via the [Authorize] attribute.
    /// Handles displaying user's cars and processing loan payoffs.
    /// Demonstrates separation of concerns between UI and business logic.
    /// </summary>
    [Authorize] // Ensures only authenticated users can access this page
    public class DashboardModel : PageModel
    {
        private readonly ICarService _carService;
        private readonly ILoanService _loanService;

        /// <summary>
        /// List of cars owned by the current user.
        /// Populated during page load and displayed in the UI.
        /// </summary>
        public List<Car> Cars { get; set; } = new List<Car>();

        /// <summary>
        /// Constructor with dependency injection.
        /// Demonstrates the Dependency Inversion Principle - depends on interfaces, not concrete classes.
        /// </summary>
        /// <param name="carService">Service for car-related operations</param>
        /// <param name="loanService">Service for loan-related operations</param>
        public DashboardModel(ICarService carService, ILoanService loanService)
        {
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
            _loanService = loanService ?? throw new ArgumentNullException(nameof(loanService));
        }

        /// <summary>
        /// Handles HTTP GET requests to load the dashboard page.
        /// Retrieves user's cars from the business layer.
        /// Demonstrates the separation between presentation and business logic.
        /// </summary>
        /// <returns>Task for async operation</returns>
        public async Task OnGetAsync()
        {
            try
            {
                // Extract user ID from authentication claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    // Load cars for the authenticated user
                    Cars = await _carService.GetCarsByUserIdAsync(userId);
                }
                else
                {
                    // Handle case where user claims are invalid (shouldn't happen with proper auth)
                    Cars = new List<Car>();
                }
            }
            catch (Exception ex)
            {
                // In production, log the exception
                // For now, ensure Cars is empty to prevent display issues
                Cars = new List<Car>();

                // Could set TempData error message here
                TempData["Error"] = "Unable to load your vehicles. Please try again.";
            }
        }

        /// <summary>
        /// Handles HTTP POST requests for paying off loans.
        /// Processes loan payoff through the business layer.
        /// Implements the Post-Redirect-Get pattern to prevent duplicate submissions.
        /// </summary>
        /// <param name="carId">ID of the car whose loan is being paid off</param>
        /// <param name="name">Name of the person paying off the loan</param>
        /// <returns>Redirect to same page to display updated data</returns>
        public async Task<IActionResult> OnPostPayoffLoanAsync(int carId, string name)
        {
            try
            {
                // Validate input parameters
                if (carId <= 0)
                {
                    TempData["Error"] = "Invalid car selection.";
                    return RedirectToPage();
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    TempData["Error"] = "Name is required to pay off the loan.";
                    return RedirectToPage();
                }

                // Process the loan payoff through business layer
                var success = await _loanService.PayoffLoanAsync(carId, name.Trim());

                if (success)
                {
                    // Success feedback to user
                    TempData["Success"] = $"Loan paid off successfully by {name.Trim()}!";
                }
                else
                {
                    // Business rule violation or loan not found
                    TempData["Error"] = "Failed to pay off loan. The loan may not exist or is already paid off.";
                }
            }
            catch (ArgumentException ex)
            {
                // Handle business rule violations
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                // In production, log the full exception details
                TempData["Error"] = "An unexpected error occurred. Please try again.";
            }

            // Redirect to same page (Post-Redirect-Get pattern)
            // Prevents duplicate form submissions on page refresh
            return RedirectToPage();
        }
    }
}