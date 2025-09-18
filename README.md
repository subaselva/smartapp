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
- **Entity Framework Core** (SQL Server )
- **SendGrid** (Email notifications)
- **Clean Architecture** principles
  - `Domain` → Entities & core logic
  - `Application` → Interfaces & Use Cases
  - `Infrastructure` → Implementations (EF Core, SendGrid)
  - `API` → Entry point & Dependency Injection

## 🏗️ Database & Class Model

The following diagram shows the entities, primary keys, and foreign key relationships used in the application:

<img width="1684" height="604" alt="image" src="https://github.com/user-attachments/assets/5df7281a-4f59-43d5-9759-8e19de48d9fb" />


## 📖 API Documentation

The API is documented using **Swagger UI**:
<img width="1901" height="882" alt="image" src="https://github.com/user-attachments/assets/81cf31e9-9144-4298-94d5-204091b44283" />

