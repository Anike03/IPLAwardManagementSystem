# 🏏 IPL Award Management System

A full-featured ASP.NET Core MVC web application designed to manage IPL player awards, votes, and results efficiently across seasons.

## 📌 Overview

The **IPL Award Management System** allows administrators to manage players, awards, voters, and conduct fair voting for various IPL awards. The system automatically calculates winners based on votes and also supports manual entry for historical records.

---

## 🚀 Features

- ✅ Player CRUD operations
- 🏆 Award category management
- 🗳️ Role-based voter system with verification and activation status
- 📅 Nomination and voting per season (year-wise)
- 📈 Real-time vote counting and winner calculation
- 📝 Manual winner management for past years
- 📄 Display of player name, award, year, vote count, and winner status

---

## 🛠️ Technologies Used

- **Backend:** ASP.NET Core MVC (C#)
- **Frontend:** Razor Pages, Bootstrap, HTML5/CSS3
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Architecture:** Layered structure with DTOs, Services, and Controllers

---
## 🧑‍💼 User Roles

- **Admin:** Full access to manage all entities and results
- **President / Secretary / Joint Secretary:** Voting privileges
- **User / Guest:** Limited or no voting rights (based on verification)

---

## 📊 Winner Calculation Logic

- The system determines the **Winner** per award and year by **highest vote count**.
- If multiple players have the same vote count, the first created nomination is prioritized.
- Admins can manually add winners for previous years.

---

## ✅ How to Run the Project

1. **Clone the repository:**

   git clone https://github.com/yourusername/IPLAwardManagementSystem.git
   cd IPLAwardManagementSystem
   
2.**Setup the database:**
   
   Update appsettings.json with your SQL Server connection string.

3.**Run the migrations:**

  dotnet ef database update
  Run the application:

## 📧 Contact
Developer: Aniketkumar Ramnarayan Sharma
Email: aniketsharma9426@gmail.com
College: Humber College, Ontario
Student ID: N01667327
