# ASP.NET Core Backend

Hosted live on MonsterASP.NET

# HR Section

## Project Summary

This project is a .NET BaaS (Backend-as-a-Service) backend supporting a data-driven React frontend. Its primary purpose is to provide fast and reliable access to game item data through centralized APIs, reducing direct load on third-party data sources. The system ensures data is consistently available, improves performance, and simplifies frontend data consumption.

<<<<<<< HEAD
Tech Stack: ASP.NET Core (C#, .NET 8), REST API + Swagger, MongoDB, Third-Party GraphQL Integration

Core Competencies: API Architectures (GraphQL to REST), Data Transformation (DTO / Adapter), NoSQL Persistence, Service-Oriented Development, Performance & Scalability, Reliability & Error Handling, Clean Code & Design Principles (Separation of Concerns)

### Next Steps:

Quartz: Data remodeling, database structure updates, and configuring scheduled jobs
=======
Tech stack: ASP.NET, MongoDB, REST API, GraphQL, SQL Server (MSSQL) In progress

### Next Steps:

Introduce MSSQL database for user management, enabling authentication and personalized features.

### Next:
>>>>>>> 031b67f4d1f8fdaa2af56d94f9064ec4e97ed6f2

Introduction to Redis: Accelerating front-end API requests with cached database queries.

Optimize data loading and caching for even faster frontend responses.

<<<<<<< HEAD
Introduce MSSQL database for user management, enabling authentication and personalized features.

=======
>>>>>>> 031b67f4d1f8fdaa2af56d94f9064ec4e97ed6f2
### Done:

Implemented backend services delivering item data to the existing frontend, following clear and maintainable architecture principles.

### Impact:

Reduces direct load on external APIs

Improves frontend performance and reliability

Provides a scalable foundation for further feature development

---

# Technical section

## Project Goal

This backend layer reads data from a 3rd party **GraphQL API** (Tarkov.dev), stores it in a **Mongo DB NoSQL** database, and then serves it through a **REST API** to the frontend [Tarkony-react-frontend](https://github.com/Sz-Daniel/Tarkony-react-frontend). In later phases, user management and a “shopping list” feature will be implemented.

---

## Architecture Overview

- **Backend:** ASP.NET Core Web API
- **Data Source:** 3rd party Origin GraphQL endpoint
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

- Redis
- Shopping list feature with MSSQL – user saves the price/barter favourites

---

## Project Structure

```
/src
├── Controllers/
│   ├── FrontendController.cs //Queries from database for Frontend
│   ├── FetchController.cs //Query from Thirdparty API
│   └── MongoController.cs //Query and mutation from Database
├── Services/
│   ├── GraphQLService.cs // integration with Tarkov.dev GraphQL API
│   └── MongoDBServices.cs // NoSQL, bulk data for frontend rendering only
├── Models/
│   ├── Adapters/
│   │   └── ItemsAdapter.cs
│   └── DataModels/ //External and Domain models
│       ├── Contracts.cs //Model for data select queries
│       └── ItemsModel.cs
└── Program.cs
```

---

## Quick Setup and Run

1. Clone the repo:

```powershell
git clone https://github.com/Sz-Daniel/Tarkony-aspdotnet-backend/
cd Tarkony-aspdotnet-backend/Tarkony-aspdotnet-backend

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

---
