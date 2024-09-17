# Critter Stacking

A playground for the [Critter Stack](https://github.com/JasperFx/).

- [Marten](https://martendb.io/)
- [Wolverine](https://wolverine.netlify.app/)

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/andhernand/critterstacking.git
cd critterstacking
```

### Start PostgreSQL and RabbitMQ

Make sure Docker Desktop is running, then execute the following command to start the [PostgreSQL](https://www.postgresql.org/) container:

```bash
docker-compose up -d
```

### Restore and Build

```bash
dotnet restore CritterStacking.sln
dotnet build CritterStacking.sln
```

### Integration Testing with Testcontainers

[Testcontainers for .NET](https://dotnet.testcontainers.org/) is being used to create [PostgreSQL](https://www.postgresql.org/) container for all integration tests. To run the tests, use the following command:

```bash
dotnet test CritterStacking.sln
```

- [Testcontainers PostgreSQL Module](https://testcontainers.com/modules/postgresql/)

## Contributing

Contributions are welcome! Please fork this repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

