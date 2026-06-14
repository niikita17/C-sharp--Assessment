# C#--Assessment
RESTful Product Management API built with .NET 8, ASP.NET Core Web API, Entity Framework Core, SQL Server, JWT Authentication, Repository Pattern, Unit of Work, API Versioning, FluentValidation, Swagger, Docker.

# Product Management API

A production-ready RESTful API built using .NET 8 and ASP.NET Core Web API following Clean Architecture principles and industry best practices.

## Features

* ASP.NET Core Web API (.NET 8)
* Entity Framework Core
* SQL Server
* JWT Authentication
* Refresh Token Support
* API Versioning
* Repository Pattern
* Unit Of Work Pattern
* FluentValidation
* Global Exception Handling Middleware
* Swagger/OpenAPI Documentation
* Structured Logging
* Docker Support
* xUnit Unit Testing
* Pagination Support
* Role-Based Authorization

---

## Architecture

```text
Solution
│
├── src
│   ├── API
│   ├── Application
│   ├── Domain
│   └── Infrastructure
│
├── tests
│   ├── API.Tests
│   ├── Application.Tests
│   └── Infrastructure.Tests
│
└── docker-compose.yml
```

---

## Technologies Used

| Technology            | Version |
| --------------------- | ------- |
| .NET                  | 8       |
| ASP.NET Core Web API  | 8       |
| SQL Server            | Latest  |
| Entity Framework Core | 8       |
| JWT Authentication    | Yes     |
| FluentValidation      | Latest  |
| Swagger               | Latest  |
| Docker                | Latest  |
| xUnit                 | Latest  |
| Moq                   | Latest  |

---

## Database Schema

### Product

| Column      | Type          |
| ----------- | ------------- |
| Id          | INT           |
| ProductName | NVARCHAR(255) |
| CreatedBy   | NVARCHAR(100) |
| CreatedOn   | DATETIME      |
| ModifiedBy  | NVARCHAR(100) |
| ModifiedOn  | DATETIME      |

### Item

| Column    | Type |
| --------- | ---- |
| Id        | INT  |
| ProductId | INT  |
| Quantity  | INT  |

---

## Authentication

The API uses JWT Authentication.

### Login Flow

1. User authenticates.
2. Access Token generated.
3. Refresh Token generated.
4. Access Token used for secured endpoints.
5. Refresh Token used to obtain new Access Token.

---

## API Endpoints

### Products

| Method | Endpoint              | Description       |
| ------ | --------------------- | ----------------- |
| GET    | /api/v1/products      | Get All Products  |
| GET    | /api/v1/products/{id} | Get Product By Id |
| POST   | /api/v1/products      | Create Product    |
| PUT    | /api/v1/products/{id} | Update Product    |
| DELETE | /api/v1/products/{id} | Delete Product    |

### Authentication

| Method | Endpoint                   |
| ------ | -------------------------- |
| POST   | /api/v1/auth/login         |
| POST   | /api/v1/auth/register      |
| POST   | /api/v1/auth/refresh-token |

---

## Running Locally

### Clone Repository

```bash
git clone https://github.com/<yourusername>/ProductManagementAPI.git
```

### Navigate

```bash
cd ProductManagementAPI
```

### Update Connection String

Update:

```json
appsettings.json
```

### Apply Migration

```bash
dotnet ef database update
```

### Run Application

```bash
dotnet run
```

Swagger:

```text
https://localhost:5001/swagger
```

---

## Docker

### Build

```bash
docker-compose build
```

### Run

```bash
docker-compose up -d
```

---

## Testing

Run Unit Tests

```bash
dotnet test
```

Coverage Includes:

* Service Layer
* Repository Layer
* Authentication Logic
* Controller Testing
* Integration Testing

---

## Security

* JWT Authentication
* Refresh Token Rotation
* Role-Based Authorization
* FluentValidation
* Global Exception Handling
* HTTPS Enforcement
* CORS Policy

---

## Performance Optimizations

* AsNoTracking()
* Async/Await
* Pagination
* Optimized EF Queries
* Dependency Injection
* Repository Pattern

---

## Future Improvements

* Redis Caching
* Azure Deployment
* CQRS Pattern
* Distributed Logging
* Microservices Architecture

---

## Author

Nikita Mankape
