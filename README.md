# ASP.NET Core Backend – GraphQL → REST → NoSQL

## Project Goal

This backend layer reads data from a 3rd party **GraphQL API** (Tarkov.dev), stores it in a **Cosmos DB NoSQL** database, and then serves it through a **REST API** to the frontend (Tarkony-react-frontend). In later phases, user management and a “shopping list” feature will be implemented.

---

## Architecture Overview

- **Backend:** ASP.NET Core Web API
- **Data Source:** 3rd party GraphQL endpoint
- **Adapter Layer:** GraphQL → internal DTO → domain model
- **Persistence:** Cosmos DB (NoSQL documents)
- **Serving:** REST API endpoints

---

## Current Status

- ASP.NET Core project initialized
- Swagger/OpenAPI documentation in developer mode
- Middleware: logging, error handling
- `GraphQLService` → `HttpClient` integration with Tarkov.dev GraphQL API
- `ItemsController` → `/api/items/bulk` endpoint fetching data from GraphQL

---

## Project Structure

```
/src
├── Program.cs           # ASP.NET Core entry point, middleware, DI
├── Controllers/
│   └── ItemsController.cs  # REST endpoints (GraphQL fetch and serving results)
├── Services/
│   ├── GraphQLService.cs    # GraphQL adapter layer + HttpClient wrapper
│   └── Adapters.cs          # Mapping: query DTO -> domain DTO
├── Models/
│   ├── ExternalModels.cs    # DTOs for GraphQL response
│   └── DomainModel.cs       # Internal domain types
└── Properties/launchSettings.json
```

---

## Quick Setup and Run

1. Clone the repo:

````powershell
git clone https://github.com/Sz-Daniel/Tarkony-aspdotnet-backend/
cd Tarkony-aspdotnet-backend


```powershell
git clone https://github.com/Sz-Daniel/Tarkony-aspdotnet-backend/
cd Tarkony-aspdotnet-backend
````

2. Restore NuGet packages and build the project:

```powershell
dotnet restore
dotnet build
```

3. Run in developer mode:

```powershell
dotnet run --project Tarkony-aspdotnet-backend/Tarkony-aspdotnet-backend.csproj
```

4. Open Swagger UI (in developer environment):

```
https://localhost:5001/swagger
```

---

## Roadmap / Priorities

- Backend setup – ASP.NET Core API skeleton, Swagger documentation
- GraphQL integration – adapter layer, validation, DTOs
- NoSQL storage – Cosmos DB integration, item documents
- REST API endpoints – search, prices, barter information
- CI/CD pipeline – GitHub Actions / Azure DevOps
- First deploy – Azure App Service (with free tier options)
- User management – ASP.NET Identity, JWT tokens, role-based access
- Shopping list feature – user saves with price/barter snapshots
- Cache & Queue – Redis cache, Azure Storage Queue
- Blob Storage integration – images and static content
- Security – HTTPS, OWASP basics, secret management
- Advanced patterns – CQRS, Mediator, AutoMapper, unit/integration tests
- Monitoring – Application Insights, performance tuning

---

## Notes, Design Decisions

- Pagination will only be introduced based on frontend requirements; initially, the backend provides simple, explicit endpoints.
- Strive for clean layering: GraphQL deserialization, adapter mapping, domain model, repository.
- The project currently has a junior focus: simple, easy-to-understand solutions; later expandable with advanced patterns.
