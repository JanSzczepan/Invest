# Invest - Investment Management Platform

## Overview

Invest app is an advanced platform designed specifically for investors, providing them with a streamlined way to effectively manage their investment portfolios. The platform offers the ability for users to create their own accounts, through which they can effortlessly add and monitor their investments. It provides users with a detailed view of their entire investment portfolio and the tools needed to analyze their financial growth and return on investments.

## Features

-   **Account Management**: Securely register and manage user accounts.
-   **Investment Tracking**: Add and manage detailed investment records.
-   **Performance Analysis**: Gain insights into investment performance and profit margins.
-   **Real-Time Notifications**: Stay updated with real-time alerts using SignalR.
-   **Responsive Interface**: Experience a seamless interaction on any device with a fully responsive design.

## Built With

-   **.NET 8**: Utilizing the latest advancements for robust back-end services.
-   **Entity Framework Core**: Managing database operations with Microsoft's modern ORM.
-   **Blazor**: Crafting interactive UIs using Blazor Server and WebAssembly.
-   **Clean Architecture**: Implementing a maintainable and scalable architecture with clear separation of concerns.
-   **Azure Services**: Hosting on Azure for a high-availability cloud environment.

## Getting Started

### Prerequisites

-   .NET 8 SDK
-   SQL Server (LocalDB, SQL Express, or Azure SQL Database)
-   Azure account for hosting services (optional for local development)

### Local Setup

1. **Clone the repository:**

    ```sh
    git clone https://github.com/JanSzczepan/Invest.git
    ```

2. **Navigate to the project directory:**

    ```sh
    cd Invest
    ```

3. **Restore dependencies:**

    ```sh
    dotnet restore
    ```

4. **Update connection strings in `appsettings.json`:**

    Replace with your local or Azure SQL Database connection details.

5. **Run the application:**

    ```sh
    dotnet run --project Invest/Invest.csproj
    ```

    Navigate to `http://localhost:7014` in your browser.

## Deployment

InvestApp leverages GitHub Actions for CI/CD, automating the build and deployment process to Azure Web Apps upon commits to the `develop` branch.
