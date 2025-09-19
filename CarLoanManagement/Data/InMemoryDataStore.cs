using CarLoanManagement.Models;

namespace CarLoanManagement.Data
{
    public class InMemoryDataStore
    {
        private static readonly List<User> _users = new List<User>();
        private static readonly List<Car> _cars = new List<Car>();
        private static readonly List<Loan> _loans = new List<Loan>();

        static InMemoryDataStore()
        {
            SeedData();
        }

        public static List<User> Users => _users;
        public static List<Car> Cars => _cars;
        public static List<Loan> Loans => _loans;

        private static void SeedData()
        {
            // Seed Users
            var user1 = new User { Id = 1, Username = "john", Password = "password123", FullName = "John Doe" };
            var user2 = new User { Id = 2, Username = "jane", Password = "password456", FullName = "Jane Smith" };
            _users.AddRange(new[] { user1, user2 });

            // Seed Cars - More diverse vehicle data
            var cars = new[]
            {
                // John's Cars
                new Car { Id = 1, Make = "Tesla", Model = "Model 3", Year = 2023, VIN = "5YJ3E1EA5PF123456", UserId = 1 },
                new Car { Id = 2, Make = "BMW", Model = "330i", Year = 2022, VIN = "WBA5R1C05NDT12345", UserId = 1 },
                new Car { Id = 3, Make = "Toyota", Model = "Prius", Year = 2024, VIN = "JTDKARFP8P3123456", UserId = 1 },
                new Car { Id = 4, Make = "Ford", Model = "F-150", Year = 2021, VIN = "1FTFW1E84MFC12345", UserId = 1 },
                
                // Jane's Cars
                new Car { Id = 5, Make = "Audi", Model = "Q7", Year = 2023, VIN = "WA1LMAF71PD123456", UserId = 2 },
                new Car { Id = 6, Make = "Mercedes-Benz", Model = "C-Class", Year = 2022, VIN = "55SWF8DB5NU123456", UserId = 2 },
                new Car { Id = 7, Make = "Lexus", Model = "RX 350", Year = 2024, VIN = "2T2BZMCA8PC123456", UserId = 2 },
                new Car { Id = 8, Make = "Honda", Model = "Accord", Year = 2020, VIN = "1HGCV1F36LA123456", UserId = 2 },
                new Car { Id = 9, Make = "Porsche", Model = "Macan", Year = 2023, VIN = "WP1AB2A59PLB12345", UserId = 2 }
            };
            _cars.AddRange(cars);

            // Seed Loans - Mix of retail and lease with various statuses
            var loans = new Loan[]
            {
                // Tesla Model 3 - Active Retail Loan
                new RetailLoan
                {
                    Id = 1,
                    CarId = 1,
                    OriginalAmount = 52000,
                    PayoffAmount = 44200,
                    StartDate = DateTime.Now.AddMonths(-8),
                    InterestRate = 3.25m,
                    TermInMonths = 72
                },
                
                // BMW 330i - Active Lease
                new LeaseLoan
                {
                    Id = 2,
                    CarId = 2,
                    OriginalAmount = 48000,
                    PayoffAmount = 35000,
                    StartDate = DateTime.Now.AddMonths(-14),
                    MonthlyPayment = 525,
                    LeaseTermMonths = 36,
                    ResidualValue = 28000
                },
                
                // Toyota Prius - Recently Paid Off
                new RetailLoan
                {
                    Id = 3,
                    CarId = 3,
                    OriginalAmount = 28000,
                    PayoffAmount = 0,
                    StartDate = DateTime.Now.AddMonths(-24),
                    InterestRate = 2.9m,
                    TermInMonths = 60,
                    IsPaidOff = true,
                    PaidOffBy = "John Doe",
                    PaidOffDate = DateTime.Now.AddDays(-15)
                },
                
                // Ford F-150 - High Balance Retail
                new RetailLoan
                {
                    Id = 4,
                    CarId = 4,
                    OriginalAmount = 65000,
                    PayoffAmount = 58900,
                    StartDate = DateTime.Now.AddMonths(-6),
                    InterestRate = 4.75m,
                    TermInMonths = 84
                },
                
                // Audi Q7 - Luxury Lease
                new LeaseLoan
                {
                    Id = 5,
                    CarId = 5,
                    OriginalAmount = 72000,
                    PayoffAmount = 52000,
                    StartDate = DateTime.Now.AddMonths(-10),
                    MonthlyPayment = 775,
                    LeaseTermMonths = 39,
                    ResidualValue = 42000
                },
                
                // Mercedes C-Class - Low Balance Retail
                new RetailLoan
                {
                    Id = 6,
                    CarId = 6,
                    OriginalAmount = 42000,
                    PayoffAmount = 12800,
                    StartDate = DateTime.Now.AddMonths(-36),
                    InterestRate = 3.5m,
                    TermInMonths = 60
                },
                
                // Lexus RX 350 - New Lease
                new LeaseLoan
                {
                    Id = 7,
                    CarId = 7,
                    OriginalAmount = 55000,
                    PayoffAmount = 51000,
                    StartDate = DateTime.Now.AddMonths(-3),
                    MonthlyPayment = 625,
                    LeaseTermMonths = 36,
                    ResidualValue = 32000
                },
                
                // Honda Accord - Paid Off
                new RetailLoan
                {
                    Id = 8,
                    CarId = 8,
                    OriginalAmount = 32000,
                    PayoffAmount = 0,
                    StartDate = DateTime.Now.AddMonths(-48),
                    InterestRate = 4.2m,
                    TermInMonths = 60,
                    IsPaidOff = true,
                    PaidOffBy = "Jane Smith",
                    PaidOffDate = DateTime.Now.AddMonths(-12)
                },
                
                // Porsche Macan - No loan (paid in cash)
                // Car ID 9 has no associated loan
            };

            _loans.AddRange(loans);

            // Associate loans with cars
            foreach (var car in _cars)
            {
                car.Loan = _loans.FirstOrDefault(l => l.CarId == car.Id);
            }

            // Associate cars with users
            user1.Cars.AddRange(_cars.Where(c => c.UserId == 1));
            user2.Cars.AddRange(_cars.Where(c => c.UserId == 2));
        }
    }
}