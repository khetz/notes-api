# 📝 Notes App — .NET Backend

A clean-architecture REST API built with **.NET Minimal APIs**, powering a Notes App Angular client. Uses **EF Core** with **SQL Server** for data access and **JWT** authentication with refresh token rotation.

## Tech Stack

- **.NET 9** with Minimal APIs
- **EF Core** (SQL Server)
- **JWT** authentication with refresh tokens
- **Clean Architecture**
- **RAG pipeline** with Voyage AI embeddings (direct HTTP)
- **AI-powered note summarisation** with auto-tagging

## Project Structure

```
src/
├── Application/       # Use cases, request/response DTOs, service interfaces
│   ├── Inputs/
│   ├── Interfaces/
│   ├── Outputs/
│   └── Services/
│
├── Domain/            # Entities
│   ├── Entities/
│
|
├── Infrastructure/    # EF Core, repository implementations, external services
│   ├── configuration/
│   ├── Mappers
│   ├── Migrations
│   ├── Persistence
│   ├── Security
│   ├── Services
│   ├── Repositories/
│   └── Services/
│
└── NotesAPI/               # Minimal API endpoints, middleware, DI registration
    ├── Endpoints/
    ├── Extensions/
    │   Properties
    └── Program.cs
```

## Architecture

The solution follows **Clean Architecture** with a strict dependency flow:

```
Api → Application → Domain
         ↑
   Infrastructure
```

- **Domain** — Core entities. No external dependencies.
- **Application** — Business logic, request/response DTOs, and service contracts. Depends only on Domain.
- **Infrastructure** — EF Core DbContext, repository implementations, and external integrations. Implements interfaces defined in Application.
- **Api** — Minimal API endpoint definitions, middleware, and dependency injection wiring. The composition root.

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server (local or containerised)

### Setup

```bash
# Restore packages
dotnet restore

# Apply migrations
dotnet ef database update --project src/NotesApp.Infrastructure --startup-project src/NotesApp.Api

# Run the API
dotnet run --project src/NotesApp.Api
```

The API runs at `https://localhost:7013/api/` by default.

### Configuration

Update `appsettings.Development.json` in the Api project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NotesAppv2;Trusted_Connection=true;TrustServerCertificate=true"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "NotesApp",
    "Audience": "NotesApp",
    "ExpiryMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }
}
```

## Authentication

JWT access tokens are issued on login alongside a refresh token. API endpoints are secured with `[Authorize]` via `RequireAuthorization()` on endpoint groups. Token refresh is handled through a dedicated endpoint — expired access tokens are swapped silently using a valid refresh token.

## AI Features

**Semantic Search** — Notes are embedded using **Voyage AI** via direct HTTP calls. Queries are embedded at search time and matched against stored note vectors, enabling AI-powered retrieval across the user's notes.

**Summarisation & Auto-Tagging** — Notes are summarised on creation or update, with tags automatically generated from the content. This powers quick browsing and organisation without manual effort.

## Error Handling

Service and repository operations are wrapped in try-catch blocks. Exceptions are caught at the application layer and mapped to appropriate HTTP responses at the API layer.

## Roadmap

- [ ] Structured logging
- [ ] Global exception handling middleware
- [ ] Unit and integration tests

## Related

- **Frontend** — Angular 21 client ([frontend repo](!https://github.com/khetz/notes-app-v2))

## License

MIT