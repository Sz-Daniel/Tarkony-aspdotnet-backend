# ASP.NET Core Backend – GraphQL → REST → NoSQL

## Project Goal

This backend layer reads data from a 3rd party **GraphQL API** (Tarkov.dev), stores it in a **Cosmos DB NoSQL** database, and then serves it through a **REST API** to the frontend [Tarkony-react-frontend](https://github.com/Sz-Daniel/Tarkony-react-frontend). In later phases, user management and a “shopping list” feature will be implemented.

---

## Next Task

- ItemDetails model - GraphQL - Mongo - ItemsController
- Layer: External Model - Adapter - Domain Model -> Mongo
- CORS setup for Frontend
- Deploy
- Frontend: GraphQL API fetching rework -> REST API from ItemsController

---

## Architecture Overview

- **Backend:** ASP.NET Core Web API
- **Data Source:** 3rd party GraphQL endpoint
- **Adapter Layer:** GraphQL → internal DTO → domain model
- **Persistence:** Mongo Atlas DB (NoSQL documents)
- **Schesuled Jobs** Quartz
- **Serving:** REST API endpoints

---

## Current Status

- ASP.NET Core project initialized
- Swagger/OpenAPI docs enabled in developer mode
- Middleware: logging and error handling
- GraphQLService: integration with Tarkov.dev GraphQL API
- MongoDB: NoSQL, bulk data for frontend rendering only
- MongoController: direct data upload and mutation
- ItemController: queries MongoDB for data

---

## Project Structure

```
/src
├── Program.cs
├── Controllers/
│   ├── ItemsController.cs
│   ├── MongoController.cs
│   └── QuartzController.cs
├── Services/
│   ├── GraphQLService.cs
│   ├── MongoDBServices.cs
│   └── QuartzServices.cs
├── Models/
│   ├── ItemSingle.cs
│   ├── ItemDetail.cs
│   ├── ItemBaseModel.cs
│   ├── CategoriesModel.cs
│   └── MongoModel.cs
└── Properties/launchSettings.json
```

---

## Quick Setup and Run

1. Clone the repo:

````powershell
git clone https://github.com/Sz-Daniel/Tarkony-aspdotnet-backend/
cd Tarkony-aspdotnet-backend


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
- NoSQL storage – Mongo Atlas DB integration, item documents
- REST API endpoints – search, prices, barter information
- CI/CD pipeline – GitHub Actions / Azure DevOps
- First deploy – Azure App Service (with free tier options)
- User management – ASP.NET Identity, JWT tokens, role-based access
- Shopping list feature – user saves with price/barter snapshots


---

## Notes, Design Decisions

- Pagination will only be introduced based on frontend requirements; initially, the backend provides simple, explicit endpoints.
- Strive for clean layering: GraphQL deserialization, adapter mapping, domain model, repository.
- The project currently has a junior focus: simple, easy-to-understand solutions; later expandable with advanced patterns.
````
