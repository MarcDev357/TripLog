# My Trip Log — ASP.NET Core MVC

A full-featured travel planning web application built with ASP.NET Core MVC.

## Features
- Full CRUD functionality for trip records
- Multi-page Add Trip and Edit Trip workflows
- Post-Redirect-Get (PRG) pattern to prevent duplicate submissions
- ViewModels for clean data transfer between controllers and views
- TempData and ViewBag for dynamic UI messaging
- Entity Framework Core with SQL Server LocalDB
- Store destinations, accommodations, travel dates, and activities
- Bootstrap responsive UI

## Tech Stack
- ASP.NET Core MVC / C#
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap
- Jinja2

## How to Run
1. Clone the repo
2. Open in Visual Studio
3. Run migrations: `dotnet ef database update`
4. Launch with IIS Express
