# 📈 Employee Performance Management System (EPMS) API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-success?style=for-the-badge)

A robust, enterprise-grade backend API for managing employee performance evaluations. Built with **ASP.NET Core 8**, the system employs **Clean Architecture** and **Domain-Driven Design (DDD)** principles, using a decoupled Service Pattern to separate read/write operations without the overhead of external routing libraries.

> 🚧 **Status:** Actively under development. Domain layer is complete; Application/Infrastructure/API layers are in progress. See [Progress Log](#-progress-log) below.

---

## Table of Contents

- [Architecture & Project Structure](#-architecture--project-structure)
- [Technology Stack](#-technology-stack)
- [Progress Log](#-progress-log)
- [Domain Model Highlights](#-domain-model-highlights)
- [Roles & Permissions (Planned)](#roles--permissions-planned)
- [Planned API Endpoints](#planned-api-endpoints)
- [Setup & Local Development](#setup--local-development)
- [Design Decisions](#design-decisions)

---

## 🏗️ Architecture & Project Structure

The solution is divided into four strictly isolated layers:

- **EPMS.Domain** — The core of the system. Contains Entities, Value Objects, Enums, and Repository Interfaces. *Zero external dependencies.*
- **EPMS.Application** — Business logic layer. Contains Services (Command/Query separation), DTOs, AutoMapper profiles, and FluentValidation rules. *Depends only on Domain.*
- **EPMS.Infrastructure** — Data access and external services. Implements EF Core, Unit of Work, ASP.NET Core Identity, and JWT generation. *Depends on Domain and Application.*
- **EPMS.API** — Presentation layer. Contains Controllers, global exception-handling middleware, and Serilog configuration. *Depends on Application and Infrastructure.*

```text
src/
├── EPMS.Domain/                 # Entities, Value Objects, Enums, Repository Interfaces
├── EPMS.Application/            # Command/Query Services, DTOs, Mapping profiles, FluentValidators
├── EPMS.Infrastructure/         # DbContext, Fluent Configurations, Repositories, Unit of Work, Identity Setup
└── EPMS.API/                    # Controllers, Middleware, Dependency Injection setup
```

---

## 🛠️ Technology Stack

| Category | Technology |
| --- | --- |
| Framework | .NET 8 Web API |
| Database & ORM | SQL Server, Entity Framework Core (Code-First) |
| Validation & Mapping | FluentValidation, AutoMapper |
| Authentication | ASP.NET Core Identity, JWT Bearer |
| Logging | Serilog |
| API Docs | Swagger / NSwag (OpenAPI Specification) |

---

## 🚀 Progress Log

### Phase 1 — Architecture Setup ✅
- [x] Initialized blank solution and core projects.
- [x] Established strict dependency rules (Domain → Application → Infrastructure → API).
- [x] Configured NuGet packages per layer to enforce architectural boundaries.
- [x] Decided on a custom Service Pattern for business logic execution (Command/Query separation).
- [x] Prepared ASP.NET Core Identity integration strategy within the Infrastructure layer.

### Phase 2 — Domain Layer ✅
- [x] Implemented DDD building blocks:
  - **Enums:** `SystemRoles`, `EvaluationStatus`.
  - **Value Objects:** `DateRange` (immutable, self-validating period logic).
  - **Entities:** Rich POCO models with private setters, business methods, and invariant protections — `User`, `Employee`, `Department`, `EvaluationTemplate`, `Section`, `Criteria`, `Evaluation`, `EvaluationResponse`.
  - **Contracts:** Clean repository interfaces and `IUnitOfWork` without generic clutter.

### Phase 3 — Application Layer (Planned)
- [ ] DTOs and AutoMapper profiles.
- [ ] Command/Query services per feature.
- [ ] FluentValidation rules.

### Phase 4 — Infrastructure Layer (Planned)
- [ ] EF Core `DbContext` and Fluent configurations.
- [ ] Repository & Unit of Work implementations.
- [ ] ASP.NET Core Identity setup and JWT token generation.

### Phase 5 — API Layer (Planned)
- [ ] Controllers and endpoint wiring.
- [ ] Global exception handling middleware.
- [ ] Serilog configuration and Swagger/OpenAPI docs.

---

## 📦 Domain Model Highlights

- **Domain-Driven Design (DDD):** Entities maintain invariants internally via parameterized constructors and business methods (e.g., `StartEvaluation`, `ChangeDepartment`).
- **Immutability & Safety:** Value objects are immutable records; collections are exposed as `IReadOnlyCollection` to prevent external modification.
- **Decoupled Identity:** Authentication details are cleanly decoupled from core business entities, allowing pluggable identity implementations.

### Core Entities
- **User** — credentials and basic profile information.
- **Employee** — extends `User` with department, position, and hire date.
- **Department** — lookup entity used to organize employees.
- **EvaluationTemplate** — defines the structure of a performance review (e.g., "Quarterly Review 2026").
- **Section** — a template contains multiple sections (e.g., "Technical Skills", "Teamwork").
- **Criteria** — each section has specific, measurable criteria (e.g., "Code Quality", "Communication").
- **Evaluation** — the main transactional entity, linking an employee, an evaluator, and a template.
- **EvaluationResponse** — stores the actual scores/feedback per criterion within an evaluation.

---

## Roles & Permissions (Planned)

| Role | Responsibilities |
| --- | --- |
| **Admin** | Full system governance, department management, employee accounts, template provisioning. |
| **HR** | Department operations, employee profile provisioning, evaluation lifecycle oversight. |
| **Manager** | Initiating evaluation cycles, scoring assigned team members, submitting performance metrics. |
| **Employee** | Viewing personal profile and tracking assigned evaluation summaries. |

---

## Planned API Endpoints

> Not yet implemented — reflects the target surface for Phase 5.

### Authentication (`/api/Auth`)
- `POST /api/auth/register-employee`
- `POST /api/auth/login`

### Departments (`/api/Departments`)
- `POST /api/departments` `[Admin]`
- `PUT /api/departments/{id}` `[Admin]`
- `GET /api/departments/{id}`
- `GET /api/departments`

### Employees (`/api/Employees`)
- `POST /api/employees` `[Admin, HR]`
- `PUT /api/employees/{id}/department` `[Admin, HR]`
- `PATCH /api/employees/{id}/deactivate` `[Admin, HR]`
- `GET /api/employees/{id}` `[Admin, HR, Manager]`
- `GET /api/employees` `[Admin, HR, Manager]`
- `GET /api/employees/department/{departmentId}` `[Admin, HR, Manager]`

### Evaluation Templates (`/api/EvaluationTemplates`)
- `POST /api/evaluationtemplates` `[Admin, HR]`
- `PATCH /api/evaluationtemplates/{id}/deactivate` `[Admin, HR]`
- `GET /api/evaluationtemplates/{id}` `[Admin, HR, Manager]`
- `GET /api/evaluationtemplates/active`

### Evaluations (`/api/Evaluations`)
- `POST /api/evaluations/initiate` `[Admin, HR, Manager]`
- `POST /api/evaluations/{id}/submit-scores` `[Admin, Manager]`
- `PATCH /api/evaluations/{id}/cancel` `[Admin, HR]`
- `GET /api/evaluations/{id}`
- `GET /api/evaluations/employee/{employeeId}`
- `GET /api/evaluations/evaluator/{evaluatorId}`

---

## Setup & Local Development

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EPMS
   ```

2. **Configure database & secrets**

   Update `EPMS.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EPMS-Db;Trusted_Connection=True;TrustServerCertificate=True;"
     },
     "JwtSettings": {
       "Secret": "YourSuperSecretKeyHereMakeSureItIsLongEnough32Bytes!",
       "Issuer": "EPMS-API",
       "Audience": "EPMS-Client",
       "DurationInMinutes": 60
     }
   }
   ```

3. **Apply EF Core migrations** *(once Infrastructure layer is implemented)*
   ```bash
   dotnet ef database update --project EPMS.Infrastructure --startup-project EPMS.API
   ```

4. **Run the application**
   ```bash
   dotnet run --project EPMS.API
   ```

5. Navigate to `https://localhost:<port>/swagger` to explore the API.

---

## Design Decisions

- **Repository Pattern** — decouples business logic from data access, improving testability.
- **CQRS** — separates read/write concerns, simplifying validation and optimizing query performance.
- **Clean Architecture** — keeps domain logic independent from frameworks and infrastructure, easing future changes (e.g., swapping SQL Server for another provider).
- **DTOs** — shape API responses to avoid over-fetching and prevent exposing internal domain models.

---

## 👥 Contributors

| Name | Role | LinkedIn |
|---|---|---|
| **Mostafa Ahmed Soudi** | Backend Developer | [linkedin.com/in/mostafa-ahmed-745497326](https://www.linkedin.com/in/mostafa-ahmed-745497326/) |

*Computer Software Engineering, Egyptian Chinese University (ECU)*

---

## 🙏 Mentorship

Special thanks to the following mentors for their guidance throughout this project:

| Name | LinkedIn |
|---|---|
| **AbdALlatif Hossni** | [linkedin.com/in/abdallatif-hossni](https://www.linkedin.com/in/abdallatif-hossni/) |

---

## 📜 License

This project is licensed under the MIT License.

---

**Developed by Mostafa Ahmed Soudi © 2026**
