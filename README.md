# SmartApp – Restaurant Table Booking System 🍽️

A clean architecture–based ASP.NET Core 9.0 application for managing restaurant table bookings.

## 🚀 Features
- List available restaurants
- Select restaurant branches
- View available time slots for a branch
- Display tables available in a time slot
- Book a table with customer details
- Send booking confirmation email (via SendGrid)
- Contact support: customers can send queries to the restaurant’s email (handled via SendGrid)
- Search bookings by customer email to view booked tables, with the option to check in upon arrival


## ⚙️ Tech Stack
- **.NET 9.0 (ASP.NET Core Web API)**
- **Entity Framework Core** (SQL Server / PostgreSQL)
- **SendGrid** (Email notifications)
- **Clean Architecture** principles
  - `Domain` → Entities & core logic
  - `Application` → Interfaces & Use Cases
  - `Infrastructure` → Implementations (EF Core, SendGrid)
  - `API` → Entry point & Dependency Injection

