# Car Loan Management System

A comprehensive ASP.NET Core 8.0 web application for managing car loans with authentication, loan tracking, and payoff functionality.

## Features

- **User Authentication**: Secure login system using cookie authentication
- **Car Management**: View cars associated with user accounts
- **Loan Tracking**: Support for both Retail and Lease loan types
- **Loan Payoff**: Mark loans as paid off with user attribution
- **Responsive Design**: Bootstrap-based UI that works on all devices
- **Clean Architecture**: Implements SOLID principles and OOP best practices

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Template**: Razor Pages
- **Authentication**: Cookie Authentication
- **UI Framework**: Bootstrap 5.1.3
- **Testing**: xUnit, Moq
- **IDE**: Visual Studio 2022

## Project Structure

The solution follows clean architecture principles with clear separation of concerns:

- **Models**: Domain entities (User, Car, Loan, RetailLoan, LeaseLoan)
- **Services**: Business logic layer with interfaces
- **Data**: In-memory data store for demo purposes
- **Pages**: Razor Pages for UI
- **ViewModels**: Data transfer objects for forms

## Getting Started

### Prerequisites

- Visual Studio 2022
- .NET 8.0 SDK
- IIS Express or Kestrel server

### Running the Application

1. **Clone or create the project**:
   ```bash
   dotnet new webapp -n CarLoanManagement
   cd CarLoanManagement
   ```


2. **Restore packages**:
   ```bash
   dotnet restore
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```

5. **Access the application**:
   - Open browser to `https://localhost:5001` or `http://localhost:5000` (or check your own instance port)
   - Use demo credentials to login

6.  **Access the application via Azure**:
    -You can access the app on the following link it was deployed on Azure using CI conected via git pipelines 
      [Car loan management](carloanmanagement-a3debtbwh5a3gceq.canadacentral-01.azurewebsites.net)

### Demo Accounts & Sample Data

The application comes with comprehensive sample data to demonstrate all features:

| Username | Password     | Vehicles | Loan Mix |
|----------|--------------|----------|----------|
| john     | password123  | 4 cars   | Tesla Model 3 (Active Retail), BMW 330i (Active Lease), Toyota Prius (Paid Off), Ford F-150 (Active Retail) |
| jane     | password456  | 5 cars   | Audi Q7 (Active Lease), Mercedes C-Class (Low Balance), Lexus RX 350 (New Lease), Honda Accord (Paid Off), Porsche Macan (No Loan) |

**Sample Data Features:**
- **Mixed loan types**: Both Retail financing and Lease agreements
- **Various loan statuses**: Active loans, paid-off loans, and vehicles with no loans
- **Realistic data**: Authentic VINs, current model years, and realistic loan amounts
- **Progress tracking**: Visual indicators showing loan payoff progress
- **Financial diversity**: Range from economy to luxury vehicles with appropriate loan amounts



## Architecture & Design Patterns

### SOLID Principles Implementation

1. **Single Responsibility Principle (SRP)**:
   - Each service class has one responsibility
   - Models represent single business concepts
   - Pages handle specific user interactions

2. **Open/Closed Principle (OCP)**:
   - Abstract Loan base class allows extension without modification
   - Service interfaces enable easy implementation changes

3. **Liskov Substitution Principle (LSP)**:
   - RetailLoan and LeaseLoan can replace Loan base class
   - All service implementations can substitute their interfaces

4. **Interface Segregation Principle (ISP)**:
   - Small, focused interfaces (IUserService, ICarService, ILoanService)
   - Clients depend only on methods they use

5. **Dependency Inversion Principle (DIP)**:
   - High-level modules depend on interfaces, not implementations
   - Dependency injection used throughout

### Object-Oriented Programming Features

- **Inheritance**: Loan base class with RetailLoan and LeaseLoan derivatives
- **Polymorphism**: Virtual methods and abstract properties
- **Encapsulation**: Private fields with public properties
- **Abstraction**: Service interfaces and abstract base classes

## Key Features Demonstration

### Loan Types

**Retail Loans**:
- Traditional car financing
- Interest rate calculations
- Monthly payment computation
- Fixed term periods

**Lease Loans**:
- Vehicle leasing arrangements
- Monthly lease payments
- Residual value tracking
- Early termination fee calculation

### Business Logic

- Loan payoff workflow with validation
- User authentication and authorization
- Data persistence simulation
- Error handling and user feedback

### Testing Strategy

- Unit tests for business logic
- Service layer testing
- Model validation testing
- Mock dependencies for isolation


## Contributing

1. Follow the existing code structure
2. Maintain SOLID principles
3. Add tests for new functionality
4. Update documentation as needed

## Author

Developed by Alberto Ruiz Rodríguez – Full-Stack Software Engineer  
[GitHub](https://github.com/Albertoruiz37) · [LinkedIn](https://www.linkedin.com/in/jesus-alberto-ruiz-rodriguez-63456836/)

## License

This project is licensed under the MIT License – see the [LICENSE](LICENSE) file for details.
