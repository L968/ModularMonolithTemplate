# Modular Monolith .NET Template

This repository provides a template for creating .NET projects using **Modular Monolith Architecture**, integrated with **.NET Aspire**, **Entity Framework**, and other technologies. This template helps you quickly set up and organize your project with a modular structure, enabling easy maintenance and streamlined database management.

## Technologies and Patterns

- [Modular Monolith Architecture](https://www.milanjovanovic.tech/blog/what-is-a-modular-monolith) Structure your project by feature, making it easier to maintain and scale.
- [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/): Pre-configured for containerization, enabling you to run the entire project with one command.
- [MySQL](https://www.mysql.com/): The template uses MySQL as the default database, with easy setup via Docker.
- [Entity Framework](https://docs.microsoft.com/en-us/ef/core/): Simplifies database management with EF Core, enabling you to apply and manage migrations effortlessly.
- [Health Checks](https://www.nuget.org/packages/AspNetCore.HealthChecks.UI.Client): Integrated health checks for monitoring the application’s health.
- [FluentValidation](https://fluentvalidation.net/): Provides a clean and expressive way to validate models.
- [MediatR](https://github.com/jbogard/MediatR): Enables in-process messaging for better separation of concerns.
- [xUnit](https://xunit.net/), [Moq](https://github.com/moq): Testing libraries

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/L968/ModularMonolithTemplate.git

cd ModularMonolithTemplate

```

### 2. Install the Template

To install the template globally, run the following command:

```bash
dotnet new --install .
```

### 3. Create a New Project

To create a new project using the Vertical Slice template, use the command:

```bash
cd ..

dotnet new modularmonolith-template -o "YourProjectName"
```

## Running the Application

### 1. Ensure Docker is running

Before starting the application, make sure **Docker Desktop** (Windows/macOS) or the **Docker service** (Linux) is running on your system.

### 2. Run the application using .NET Aspire

```bash
dotnet run --project YourProjectName.AppHost
```

### 3. Running Tests
To run unit tests, execute:

```bash
 dotnet test
```

### 4. Adding Migrations

1. Set "YourProjectName.Api" as your start up project.
2. Open the Package Manager Console.
3. Set the default project to "YourProjectName.Modules.YourModule.Infrastructure".
4. Run the following command.

```bash
Add-Migration Init -Context YourModuleDbContext -o Database/Migrations
```

## API Endpoints  
Once the application is running, you can access the API via Scalar in the **YourProjectName.Api** project from the .NET Aspire dashboard.  

## Contributing
Feel free to open issues and pull requests to improve the project!

## License
This project is licensed under the [MIT License](LICENSE.txt).
