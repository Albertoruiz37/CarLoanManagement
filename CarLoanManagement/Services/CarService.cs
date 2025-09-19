using CarLoanManagement.Data;
using CarLoanManagement.Models;
using CarLoanManagement.Services.Interfaces;

namespace CarLoanManagement.Services
{
    public class CarService : ICarService
    {
        public async Task<List<Car>> GetCarsByUserIdAsync(int userId)
        {
            await Task.Delay(1);
            return InMemoryDataStore.Cars
                .Where(c => c.UserId == userId)
                .Select(c => new Car
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    VIN = c.VIN,
                    UserId = c.UserId,
                    Loan = InMemoryDataStore.Loans.FirstOrDefault(l => l.CarId == c.Id)
                })
                .ToList();
        }

        public async Task<Car?> GetCarByIdAsync(int carId)
        {
            await Task.Delay(1);
            var car = InMemoryDataStore.Cars.FirstOrDefault(c => c.Id == carId);
            if (car != null)
            {
                car.Loan = InMemoryDataStore.Loans.FirstOrDefault(l => l.CarId == carId);
            }
            return car;
        }

        public async Task UpdateCarAsync(Car car)
        {
            await Task.Delay(1);
            var existingCar = InMemoryDataStore.Cars.FirstOrDefault(c => c.Id == car.Id);
            if (existingCar != null)
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.VIN = car.VIN;
            }
        }
    }
}