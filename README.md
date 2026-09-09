# 📈 Employee Performance Management System (EPMS) API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-success?style=for-the-badge)

A robust, enterprise-grade Backend API for managing employee performance evaluations. Built with **ASP.NET Core 8**, this system employs **Clean Architecture** and **Domain-Driven Design (DDD)** principles, utilizing a decoupled Service Pattern to separate read/write operations without the overhead of external routing libraries.

## 🚀 Phase 1 (Architecture Setup)
- [x] Initialized blank solution and core projects.
- [x] Established strict dependency rules (Domain → Application → Infrastructure → API).
- [x] Configured NuGet packages per layer to ensure architectural boundaries.
- [x] Decided on custom Service Pattern for business logic execution.
- [x] Prepared ASP.NET Core Identity integration strategy within the Infrastructure layer.

## 🏗️ Architecture & Project Structure

The solution is divided into four strictly isolated layers:

*   **EPMS.Domain:** The core of the system. Contains Entities, Value Objects, Enums, and Repository Interfaces. *Zero external dependencies.*
*   **EPMS.Application:** Business logic layer. Contains Services (Command/Query separation), DTOs, AutoMapper profiles, and FluentValidation rules. *Depends only on Domain.*
*   **EPMS.Infrastructure:** Data access and external services. Implements EF Core, Unit of Work, ASP.NET Core Identity, and JWT generation. *Depends on Domain and Application.*
*   **EPMS.API:** The presentation layer. Contains Controllers, Global Exception Handling Middlewares, and Serilog configuration. *Depends on Application and Infrastructure.*

## 🛠️ Technology Stack
*   **Framework:** .NET 8 Web API
*   **Database:** SQL Server & Entity Framework Core
*   **Validation & Mapping:** FluentValidation, AutoMapper
*   **Authentication:** ASP.NET Core Identity, JWT Bearer
*   **Logging:** Serilog
  
## 🚀 Current Progress: Phase 2 (Domain Layer Completed)
- [x] Initialized blank solution and core projects.
- [x] Established strict architectural boundaries and dependencies.
- [x] Configured NuGet packages per layer (zero external packages in Domain).
- [x] Implemented Domain-Driven Design (DDD) building blocks:
  - **Enums:** `SystemRoles`, `EvaluationStatus`.
  - **Value Objects:** `DateRange` (immutable, self-validating period logic).
  - **Entities:** Rich POCO models with private setters, business methods, and invariant protections (`User`, `Employee`, `Department`, `EvaluationTemplate`, `Section`, `Criteria`, `Evaluation`, `EvaluationResponse`).
  - **Contracts:** Clean repository interfaces and `IUnitOfWork` without generic clutter.

## 📦 Domain Model Highlights
- **Domain-Driven Design (DDD):** Entities maintain invariants internally via parameterized constructors and business methods (e.g., `StartEvaluation`, `ChangeDepartment`).
- **Immutability & Safety:** Value objects are immutable records; collections are exposed as `IReadOnlyCollection` to prevent external modifications.
- **Decoupled Identity:** Authentication details are cleanly decoupled from core business entities to allow pluggable identity implementations.
