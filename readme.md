# GreenFlux Smart Charging API

This application is designed for designed and developed for GreenFlux.

## Getting Started

These instructions will guide you on how to run and use the application on your local machine.

### Prerequisites

You will need the following prerequisites to run the application:

- Docker: [Docker Download Page](https://www.docker.com/products/docker-desktop)
- .NET 7.0.308 SDK: [.NET 7.0.308 SDK Download Page](https://dotnet.microsoft.com/download/dotnet/7.0.308)

### Installation

1. Clone the project to your local machine:

   ```bash
   git clone https://github.com/serdarakay/charge-station.git

2. Navigate to the project directory:

    ```bash
    cd your-repo

3. Build the Docker image:

    ```bash
    docker build -t green-flux-api .

4. Run the Docker container:

    ```bash
    docker run -d -p 8080:80 green-flux-api

5. Open your web browser and access the application at [http://localhost:8080](http://localhost:8080).

6. Go to Swagger Url like [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html).

7. Youı can test all endpoints into this url.


## Usage

You can use swagger guidance to test api end points.