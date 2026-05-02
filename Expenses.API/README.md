````markdown
# 🚀 1. Creating an ASP.NET Core Web API Project in VS Code

ASP.NET Core Web API is one of the most powerful frameworks for building RESTful services. In this guide, we’ll walk through setting up a new Web API project, configuring Swagger for testing, and running it locally in **Visual Studio Code**.

---

## 🛠 Step 1: Create an Empty Web API Project

1. Open your terminal in VS Code.
2. Run the following command to create a new project:

   ```bash
   dotnet new webapi -n Expenses.API
````

* `webapi` → template for ASP.NET Core Web API
* `-n Expenses.API` → project name

3. Navigate into the project folder:

   ```bash
   cd Expenses.API
   ```

---

## 📂 Step 2: Explore the Project Structure

Once created, you’ll see a default structure:

* **Controllers/** → contains API controllers (e.g., `WeatherForecastController.cs`)
* **Program.cs** → entry point of the application
* **appsettings.json** → configuration file (e.g., database connection strings)
* **Properties/** → includes `launchSettings.json` for profiles (HTTP/HTTPS)
* **Dependencies/** → NuGet packages

👉 By default, you’ll see a sample `WeatherForecast` controller and model.

---

## ⚙️ Step 3: Run the Project

Run the app with:

```bash
dotnet run
```

By default, it will start on:

```
https://localhost:7145
```

Open this in your browser, and you’ll see the API running.

---

## 📖 Step 4: Configure Swagger for API Testing

Swagger UI is not enabled by default in .NET 9.0. Let’s add it:

### 1. Install Swashbuckle

```bash
dotnet add package Swashbuckle.AspNetCore
```

---

### 2. Update `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

### 3. Update `launchSettings.json`

```json
"profiles": {
  "https": {
    "commandName": "Project",
    "dotnetRunMessages": true,
    "launchBrowser": true,
    "applicationUrl": "https://localhost:7145",
    "launchUrl": "swagger"
  }
}
```

---

## 🌐 Step 5: Test Your API

Now, run the project again:

```bash
dotnet run
```

Your browser will open at:

```
https://localhost:7145/swagger
```

Here you’ll see the Swagger UI with the default `WeatherForecast` endpoint.
This interface will be your testing ground for all future API endpoints.

---


# 🏗 2. Defining Application Models in ASP.NET Core Web API

In ASP.NET Core, **models** are C# classes that represent database tables. Each property in a model maps to a column in the table. Models are the foundation of your application’s data layer, and they work hand-in-hand with **Entity Framework Core** to generate and manage database tables.

In this guide, we’ll define models for our **ExpensesApp** project.

---

## 📂 Step 1: Create a Models Folder

To keep things organized:

1. In your project root (`Expenses.API`), create a new folder named **Models**  
2. This folder will contain all your entity classes  

---

## 👤 Step 2: Define the User Model

Create a new file: `Models/User.cs`

```csharp
namespace Expenses.API.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
````

### Explanation:

* `Id` will come from a base class (we’ll create it soon)
* `Email` → user’s email address
* `Password` → user’s password
* `CreatedAt` and `UpdatedAt` will also come from the base class

---

## 💰 Step 3: Define the Transaction Model

Create a new file: `Models/Transaction.cs`

```csharp
namespace Expenses.API.Models
{
    public class Transaction : BaseEntity
    {
        public string Type { get; set; }      // "Income" or "Expense"
        public double Amount { get; set; }    // transaction value
        public string Category { get; set; }  // e.g., Food, Salary
    }
}
```

### Explanation:

* `Type` → distinguishes between income and expense
* `Amount` → transaction amount
* `Category` → classification (food, rent, salary, etc.)
* `CreatedAt` and `UpdatedAt` → inherited from base class

👉 Later, when authentication is added, we’ll include a `UserId` property to link transactions to users.

---

## 🏛 Step 4: Create a Base Entity Class

Since both models share common properties (`Id`, `CreatedAt`, `UpdatedAt`), let’s avoid duplication by creating a **BaseEntity** class.

Create a new folder: **Base**
Add a file: `Base/BaseEntity.cs`

```csharp
namespace Expenses.API.Models.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
```

Now both `User` and `Transaction` inherit from `BaseEntity`.

---

## ✅ Step 5: Verify Your Models

At this point, your project structure should look like this:

```
Expenses.API/
 ├── Models/
 │    ├── User.cs
 │    ├── Transaction.cs
 ├── Base/
 │    ├── BaseEntity.cs
 ├── Program.cs
 ├── appsettings.json
```

---



