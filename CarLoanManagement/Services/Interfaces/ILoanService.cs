using CarLoanManagement.Models;

namespace CarLoanManagement.Services.Interfaces
{
    public interface ILoanService
    {
        Task<bool> PayoffLoanAsync(int carId, string paidOffBy);
        Task<Loan?> GetLoanByCarIdAsync(int carId);
        Task UpdateLoanAsync(Loan loan);
    }
}