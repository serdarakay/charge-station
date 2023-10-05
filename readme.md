# GreenFlux Smart Charging API

This application is designed for designed and developed for GreenFlux.

## Getting Started

These instructions will guide you on how to run and use the application on your local machine.

### Prerequisites

You will need the following prerequisites to run the application:

- .NET 7.0.308 SDK: [.NET 7.0.308 SDK Download Page](https://dotnet.microsoft.com/download/dotnet/7.0.308)

### Installation

1. Clone the project to your local machine:

   ```bash
   git clone https://github.com/serdarakay/charge-station.git

2. Navigate to the project directory:

    ```bash
    cd your-repo/GreenFluxSmartChargingAPI

3. Run this command:

    ```bash
    dotnet tool --global dotnet-ef
    dotnet ef migrations add initial
    dotnet ef database update

4. Navigate to the Test project directory:

    ```bash
    cd your-repo/XUnitTest

    dotnet add package Microsoft.AspNetCore.Mvc.Testing
    dotnet add package Microsoft.EntityFrameworkCore.InMemory

5. Run Project and then you can test all endpoints at swagger.

7. You can test all endpoints into this url.


## Usage

You can use swagger guidance to test api end points.