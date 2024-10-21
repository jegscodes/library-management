# Library Management System

## Overview

The Library Management System is a web application designed to efficiently manage books and authors. It utilizes a .NET 8 Web API for the backend, following Clean Architecture principles, and Angular for the frontend, ensuring a robust and scalable solution for library management tasks such as adding, updating, and deleting books and authors.

## Features

- **CRUD Operations** for Books
  - Add, edit, delete, and view book details.
  
- **CRUD Operations** for Authors
  - Add, edit, delete, and view author details.
  
- **User-Friendly Interface**
  - Responsive and intuitive design built with Angular.
  
- **RESTful API**
  - Consumes a .NET 8 Web API, following REST principles and Clean Architecture.

## Tech Stack

- **Backend**: .NET 8, ASP.NET Core Web API
- **Frontend**: Angular (v.18)
- **Database**: Microsoft SQL Server

## Clean Architecture Overview

The backend is structured using Clean Architecture, which promotes separation of concerns and maintainability. The key layers include:

1. **Entities**: Core business logic encapsulated in domain models (e.g., `Book`, `Author`).
2. **Use Cases**: Application-specific business rules encapsulated in use case classes (e.g., `AddBook`, `GetBooks`).
3. **Interface Adapters**: Repositories and services that interact with external systems (e.g., database access).
4. **Frameworks and Drivers**: ASP.NET Core Web API and any external frameworks or tools.

### Benefits of Clean Architecture

- **Maintainability**: Changes in one layer do not affect others.
- **Testability**: Each component can be tested in isolat
- **Flexibility**: Easy to swap out frameworks or libraries without altering business logic.

#### Library API Configuration

## Connection String Setup

To configure the database connection string, locate the `appsettings.Development.json` file in the root of the project and update the `DefaultConnection` entry.

### Example Connection String

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_dbname;User Id=your_username;Password=your_password;Trusted_Connection=True;MultipleActiveResultSets=true;   TrustServerCertificate=true;"
}
```

#### Backend Setup
1. Clone the repository
  ```bash
    git clone https://github.com/jegscodes/library-management.git
    cd library-management/Libray.Api
  ```
2. Restore Packages
  ```bash
    dotnet restore
  ```
3. Start the API
  ```bash
    dotnet run
  ```
4. Access the API at https://localhost:7023
