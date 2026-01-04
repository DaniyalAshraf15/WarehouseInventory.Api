# Warehouse Inventory API

A modern Warehouse Inventory Management backend built using **ASP.NET Core Minimal APIs (.NET 8)**.  
This project demonstrates lightweight API design, clean separation of concerns, DTO-based contracts, and a phased development approach aligned with current .NET best practices.

---

## 📌 Project Overview

The Warehouse Inventory API manages products and categories within a warehouse system.  
It exposes RESTful endpoints using **Minimal APIs** instead of traditional MVC controllers, resulting in a cleaner and more performant architecture.

This project is designed for **code review, interview discussion, and production-ready API design demonstration**.

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core Minimal API (.NET 8)
- **ORM:** Entity Framework Core
- **Database:** SQLite (lightweight & cloud-friendly)
- **API Documentation:** Swagger / OpenAPI
- **Testing:** xUnit
- **Architecture Style:** Minimal API + Service + Repository
- **Language:** C#

---

## 🧱 Architecture Overview

The project follows a **clean and modular Minimal API architecture**:

```text
Presentation (Minimal API Endpoints)
        ↓
Application / Services Layer
        ↓
Repository / Data Access Layer
        ↓
Database (SQLite via EF Core)


### Key Architectural Decisions

- ❌ No MVC Controllers
- ✅ Minimal API endpoints defined in `Program.cs` / endpoint files
- ✅ Business logic isolated in services
- ✅ Data access handled via repositories
- ✅ DTOs used for request/response contracts
- ✅ Dependency Injection via built-in .NET container

This approach ensures:
- Better performance
- Reduced boilerplate
- Easier maintenance
- Modern .NET alignment

---

## ✨ Key Features

- CRUD operations using Minimal APIs
- Product & Category management
- DTO-based request/response handling
- Input validation at API boundary
- SQLite database integration
- Swagger UI for interactive API testing
- Phase-based development and testing structure
- Clean, readable, and extensible codebase

---

## 🚀 Getting Started (Run Locally)

### Prerequisites
- .NET SDK 8.0+
- Visual Studio / VS Code

### Steps

```bash
git clone https://github.com/<your-actual-username>/WarehouseInventory.Api.git
cd WarehouseInventory.Api
dotnet restore
dotnet run

The API will start locally and be accessible at:
https://localhost:5001/swagger
