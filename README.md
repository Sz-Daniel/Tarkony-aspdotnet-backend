# ASP.NET Core Backend

~~[Azure](https://tarkony-asp-aqa9axgghrdmb0cx.westeurope-01.azurewebsites.net/health)~~

Online

# HR Section

## Project Summary

This project is a .NET BaaS (Backend-as-a-Service) backend supporting a data-driven React frontend. Its primary purpose is to provide fast and reliable access to game item data through centralized APIs, reducing direct load on third-party data sources. The system ensures data is consistently available, improves performance, and simplifies frontend data consumption.

Tech stack: ASP.NET, MongoDB, REST API, GraphQL, SQL Server (MSSQL) In progress

### Next Steps:

Introduce MSSQL database for user management, enabling authentication and personalized features.

### Next:

Introduction to Redis: Accelerating front-end API requests with cached database queries.

Optimize data loading and caching for even faster frontend responses.

### Done:

Implemented backend services delivering item data to the existing frontend, following clear and maintainable architecture principles.

Ensured backend handles data retrieval from a third-party source efficiently, providing fallback mechanisms to maintain uninterrupted service.

### Impact:

Reduces direct load on external APIs

Improves frontend performance and reliability

Provides a scalable foundation for further feature development

---

# Technical section

## Project Goal

This backend layer reads data from a 3rd party **GraphQL API** (Tarkov.dev), stores it in a **Mongo DB NoSQL** database, and then serves it through a **REST API** to the frontend [Tarkony-react-frontend](https://github.com/Sz-Daniel/Tarkony-react-frontend). In later phases, user management and a “shopping list” feature will be implemented.

---

## Next Task

- Deploy
- Further expansion of Error
- Further expansion of Logging
- Adapter fixing + extend it by transferring the frontend-rendered data
- Documentation
- ItemsPrice: Create a Quary and DTO, Upload into Items - Refresh the item's price
- Log db collection
- Quartz timing for frefres the Item Price and Item data into database
- GraphQL.FetchAPIStatusAsync: Generate DTO from APIStatusQuery, Create API Controller to GET the API status.
- ItemSingle: Create DTO from Query and From Frontend Typescript, Adapter, Upload
- Batching upload insteand bulk
- Status page for backend
- Refactor (+ Domain Pacalcase with JsonPropertyName)
- Deploy End of Stage 2

---

## Architecture Overview

- **Backend:** ASP.NET Core Web API
- **Data Source:** 3rd party GraphQL endpoint
- **Adapter Layer:** GraphQL + external model -> adapter → domain model
- **Persistence:** Mongo Atlas DB (NoSQL documents)
- **Schesuled Jobs** Quartz
- **Serving:** REST API endpoints

---

## Roadmap / Priorities

#### Backend setup – ASP.NET Core API skeleton, Swagger documentation

#### GraphQL integration – adapter layer, validation, DTOs

#### NoSQL storage – Mongo Atlas DB integration, item documents

#### REST API endpoints – search, prices, barter information

####

- User management – MSSQL
- Redis
- Shopping list feature – user saves with price/barter snapshots

---

## Project Structure

```
/src
├── Program.cs
├── Controllers/
│   ├── FrontendController.cs //Queries from database for Frontend
│   ├── FetchController.cs //Query from Thirdparty API
│   ├── MongoController.cs //Query and mutation from Database
│   └── QuartzController.cs
├── Services/
│   ├── GraphQLService.cs // integration with Tarkov.dev GraphQL API
│   ├── MongoDBServices.cs // NoSQL, bulk data for frontend rendering only
│   └── QuartzServices.cs //Schedules for updating item prices and item data in the database.
├── Models/
│   ├── Adapters/
│   │   └── ItemsAdapter.cs
│   └── DataModels/ //External and Domain models
│       ├── Contracts.cs //Model for data select queries
│       └── ItemsModel.cs
└── Properties/launchSettings.json
```

---

## Quick Setup and Run

1. Clone the repo:

```powershell
git clone https://github.com/Sz-Daniel/Tarkony-aspdotnet-backend/
cd Tarkony-aspdotnet-backend

```

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
.../swagger/
```
