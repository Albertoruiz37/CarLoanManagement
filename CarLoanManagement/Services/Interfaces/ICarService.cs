using CarLoanManagement.Models;

namespace CarLoanManagement.Services.Interfaces
{
    public interface ICarService
    {
        Task<List<Car>> GetCarsByUserIdAsync(int userId);
        Task<Car?> GetCarByIdAsync(int carId);
        Task UpdateCarAsync(Car car);
    }
}