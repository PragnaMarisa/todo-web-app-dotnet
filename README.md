# Task Master - Todo Web Application

A modern, feature-rich task management web application built with ASP.NET Core MVC and PostgreSQL. Organize your tasks into groups, track progress, and boost productivity with an intuitive interface.

![ASP.NET Core](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Neon-4169E1?logo=postgresql)
![License](https://img.shields.io/badge/license-MIT-green)

## 📋 Table of Contents

- [About](#about)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Database](#database)
- [Deployment](#deployment)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [API Endpoints](#api-endpoints)

## 🎯 About

Task Master is a full-stack web application that helps users organize their tasks into logical groups (Todos) with multiple subtasks (Todotasks). Built as a pet project to demonstrate modern ASP.NET Core development practices, it features a clean MVC architecture, Entity Framework Core for data access, and a responsive UI with real-time progress tracking.

**Live Demo:** [Deployed on Render](https://render.com) *(if applicable)*

## ✨ Features

### Core Functionality
- 📚 **Task Groups**: Organize tasks into logical groups (e.g., "Backend Development", "Project Setup")
- ✅ **Task Management**: Create, complete, and delete individual tasks within groups
- 📊 **Progress Tracking**: Visual progress bars showing completion percentage for each group
- 🎨 **Modern UI**: Clean, intuitive interface with emoji icons and smooth animations
- 🔔 **Notifications**: Top-right toast notifications for all actions (create, delete, validate)
- 🗑️ **Confirmation Dialogs**: Safe delete operations with confirmation popups

### Advanced Features
- ✏️ **Validation**: Client-side and server-side validation ensuring titles are not empty
- 🔄 **Real-time Updates**: AJAX-powered task additions without full page reloads
- 📱 **Responsive Design**: Works seamlessly on desktop and mobile devices
- 🎯 **Task Sorting**: Automatic sorting with pending tasks first, completed tasks last
- 📅 **Timestamps**: Track when each task was created

## 🛠️ Tech Stack

### Backend
- **Framework**: ASP.NET Core 9.0 MVC
- **Language**: C# 11+ (with nullable reference types and required properties)
- **ORM**: Entity Framework Core 9.0.2
- **Database Driver**: Npgsql.EntityFrameworkCore.PostgreSQL 9.0.2

### Frontend
- **View Engine**: Razor (.cshtml)
- **Styling**: Custom CSS with modern animations
- **JavaScript**: Vanilla JS (ES6+) for AJAX and interactivity
- **Icons**: Emoji-based design system

### Database
- **Database**: PostgreSQL 15+
- **Hosting**: Neon (Serverless PostgreSQL)
- **Migrations**: EF Core Migrations

### Testing
- **Framework**: xUnit 2.7.0
- **Mocking**: Moq 4.20.72
- **Test Runner**: Microsoft.NET.Test.Sdk 17.12.0
- **Coverage**: Unit tests for Controllers, Services, and ViewModels

### DevOps
- **Containerization**: Docker
- **Deployment**: Render
- **Version Control**: Git

## 🏗️ Architecture

### MVC Pattern

```
┌─────────────┐      ┌─────────────┐      ┌─────────────┐
│   Browser   │─────▶│ Controller  │─────▶│   Service   │
│   (View)    │◀─────│ (MVC)       │◀─────│   (Logic)   │
└─────────────┘      └─────────────┘      └─────────────┘
                            │                     │
                            ▼                     ▼
                     ┌─────────────┐      ┌─────────────┐
                     │ ViewModels  │      │  DbContext  │
                     └─────────────┘      └──────┬──────┘
                                                  │
                                                  ▼
                                           ┌─────────────┐
                                           │ PostgreSQL  │
                                           └─────────────┘
```

### Key Components

#### Controllers
- **TodosController**: Handles all HTTP requests for Todos and Todotasks
  - `Index()`: Home page
  - `AllTodos()`: Display all todos with tasks
  - `AddTodo()`: Create new todo group
  - `AddTask()`: Add task to a group
  - `ToggleStatus()`: Mark task complete/incomplete
  - `DeleteTask()`: Remove a task
  - `DeleteTodo()`: Remove a todo group

#### Services
- **ITodoService / TodoService**: Business logic for Todo operations
- **ITodotaskService / TodotaskService**: Business logic for Todotask operations
- **IAllTodosViewModelBuilder**: Builds complex view models with computed properties

#### Models
- **Todo**: Represents a task group
  - `Id` (int)
  - `Title` (required string)
  - `Description` (string?)
  - `Tasks` (ICollection<Todotask>)

- **Todotask**: Represents an individual task
  - `Id` (int)
  - `Title` (required string)
  - `Description` (string?)
  - `IsCompleted` (bool)
  - `CreatedDate` (DateTime)
  - `TodoId` (int, foreign key)
  - `Todo` (Todo?, navigation property)

#### ViewModels
- **AllTodosViewModel**: Aggregates all todos with computed progress
- **TodoGroupViewModel**: Enhanced todo with progress statistics

## 💾 Database

### PostgreSQL on Neon

**Provider**: [Neon](https://neon.tech)  
**Type**: Serverless PostgreSQL  
**Location**: Cloud-hosted (managed service)

### Connection

The application connects to PostgreSQL using a connection string stored in:
- **Local Development**: `appsettings.Development.json`
- **Production**: Environment variable on Render

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=xxx.neon.tech;Database=xxx;Username=xxx;Password=xxx"
  }
}
```

### Schema

**Tables**:
1. **Todos**
   - Id (PK, int, auto-increment)
   - Title (string, NOT NULL, DEFAULT '')
   - Description (string, nullable)

2. **Todotasks**
   - Id (PK, int, auto-increment)
   - Title (string, NOT NULL, DEFAULT '')
   - Description (string, nullable)
   - IsCompleted (boolean, default false)
   - CreatedDate (timestamp)
   - TodoId (FK → Todos.Id, CASCADE DELETE)

**Relationships**:
- One Todo → Many Todotasks (1:N)
- Cascade delete: Deleting a Todo removes all its Todotasks

### Migrations

Current migrations:
1. `20260121104326_InitialCreate` - Initial schema
2. `20260122053302_MakeTitleRequired` - Made Title non-nullable

Apply migrations:
```bash
dotnet ef database update
```

## 🚀 Deployment

### Hosting Platform: Render

**Service Type**: Web Service  
**Container**: Docker  
**Build**: Automated from GitHub repository

### Deployment Process

1. **Dockerfile** builds the application:
   - Build stage: `mcr.microsoft.com/dotnet/sdk:9.0`
   - Runtime stage: `mcr.microsoft.com/dotnet/aspnet:9.0`

2. **Render** automatically:
   - Pulls latest code from GitHub
   - Builds Docker image
   - Deploys to production
   - Exposes on HTTPS

3. **Environment Variables** on Render:
   - `ConnectionStrings__DefaultConnection`: PostgreSQL connection string

### Continuous Deployment

- Push to GitHub → Auto-deploy on Render
- Zero-downtime deployments
- Automatic HTTPS/SSL

For detailed deployment instructions, see:
- [CreatePSQLOnNeon.md](docFiles/CreatePSQLOnNeon.md)
- [DeployToRender.md](docFiles/DeployToRender.md)

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (or use Neon cloud database)
- Git
- Your favorite code editor (VS Code, Visual Studio, Rider)

### Local Development Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/todo-web-app-dotnet.git
   cd todo-web-app-dotnet
   ```

2. **Quick start (using Makefile)**
   ```bash
   # View all available commands
   make help
   
   # Quick setup
   make quickstart
   ```

3. **Configure database connection**
   
   Update `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=todoapp;Username=postgres;Password=yourpassword"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   make migration-update
   # or
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   make run
   # or
   dotnet run
   ```

6. **Open in browser**
   ```
   https://localhost:5001
   ```

### Common Makefile Commands

```bash
# Development
make dev              # Run with hot reload
make build            # Build the project
make test             # Run all tests
make coverage         # Run tests with coverage

# Database
make migration-add NAME=YourMigration
make migration-update
make migration-list

# Docker
make docker-build
make docker-run

# Cleanup
make clean
make clean-all
```

For all available commands, run: `make help`

### Using Docker

```bash
# Build image
docker build -t todo-web-app .

# Run container
docker run -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="your-connection-string" \
  todo-web-app
```

## 📁 Project Structure

```
todo-web-app-dotnet/
├── Controllers/
│   └── TodosController.cs          # Main MVC controller
├── Data/
│   ├── MyDbContext.cs              # EF Core DbContext
│   ├── TodoService.cs              # Todo business logic
│   ├── TaskService.cs              # Task business logic
│   └── AllTodosViewModelBuilder.cs # ViewModel builder
├── Models/
│   ├── Todo.cs                     # Todo entity
│   └── Todotask.cs                 # Task entity
├── ViewModels/
│   ├── AllTodosViewModel.cs        # Aggregated view data
│   └── TodoGroupViewModel.cs       # Enhanced todo data
├── Views/
│   └── Todos/
│       ├── Index.cshtml            # Home/landing page
│       └── AllTodos.cshtml         # Main task management page
├── wwwroot/
│   └── css/
│       ├── index.css               # Home page styles
│       └── alltodos.css            # Main app styles
├── Migrations/                     # EF Core migrations
├── Tests/                          # Unit tests
│   ├── TodosControllerTests.cs
│   ├── TodoServiceTests.cs
│   ├── TodotaskServiceTests.cs
│   └── AllTodosViewModelBuilderTests.cs
├── docFiles/                       # Documentation
│   ├── CreatePSQLOnNeon.md
│   └── DeployToRender.md
├── Program.cs                      # Application entry point
├── Dockerfile                      # Docker configuration
└── appsettings.json                # Configuration
```

## 🧪 Testing

The project includes comprehensive unit tests using xUnit and Moq.

### Run all tests
```bash
dotnet test
```

### Run specific test file
```bash
dotnet test --filter FullyQualifiedName~TodosControllerTests
```

### Test Coverage
- ✅ Controller actions
- ✅ Service layer methods
- ✅ ViewModel builders
- ✅ Edge cases and validation

### Test Structure
```csharp
// Example: Testing AddTodo with empty title
[Fact]
public void AddTodo_WithEmptyTitle_RedirectsWithError()
{
    // Arrange
    var mockService = new Mock<ITodoService>();
    var controller = new TodosController(mockService.Object, ...);
    
    // Act
    var result = controller.AddTodo(new Todo { Title = "" });
    
    // Assert
    Assert.IsType<RedirectToActionResult>(result);
    Assert.Equal("Title should not be empty", controller.TempData["ErrorMessage"]);
}
```

## 🔌 API Endpoints

> **Note**: This is an MVC application designed for browser usage, not a REST API. All endpoints return HTML views or redirects.

### Routes

| Method | Path | Description |
|--------|------|-------------|
| GET | `/` | Home page |
| GET | `/Todos/AllTodos` | View all todos and tasks |
| POST | `/Todos/AddTodo` | Create new todo group |
| POST | `/Todos/AddTask` | Add task to group |
| POST | `/Todos/ToggleStatus` | Toggle task completion |
| POST | `/Todos/DeleteTask` | Delete a task |
| POST | `/Todos/DeleteTodo` | Delete a todo group |

### Form Data Examples

**Add Todo:**
```
POST /Todos/AddTodo
Content-Type: application/x-www-form-urlencoded

title=Backend%20Development&description=API%20and%20database%20work
```

**Add Task:**
```
POST /Todos/AddTask
Content-Type: application/x-www-form-urlencoded

todoId=1&title=Create%20user%20model&description=Add%20user%20entity
```

## 📝 Key Features Deep Dive

### 1. Validation System

**Client-Side (JavaScript)**:
- Validates title before form submission
- Shows notification popup: "Title should not be empty"
- Prevents empty form submissions

**Server-Side (C#)**:
- Uses `required string Title` for compile-time safety
- Runtime validation with `IsNullOrWhiteSpace()`
- Passes error via `TempData` to UI

### 2. Notification System

All notifications appear at **top-right corner** with:
- ✅ Success messages (green background)
- ❌ Error messages (red background)
- ⏰ Auto-dismiss after 2 seconds
- 🎨 Smooth fade-out animation

### 3. AJAX Task Addition

Tasks are added via AJAX to avoid full page reloads:
- Submit form asynchronously
- Clear inputs on success
- Reload page to sync IDs from server
- Show notification on error

### 4. Delete Confirmation

Safe deletion with cursor-positioned confirmation dialogs:
- Click delete → Popup appears near cursor
- "Yes" button → Proceed with deletion
- "No" button / Outside click → Cancel
- Success → Show notification

## 🤝 Contributing

This is a personal learning project, but feedback and suggestions are welcome!

## 📄 License

This project is open source and available under the MIT License.

## 👤 Author

**Pragna Marisa**  
Pet Project - Learning ASP.NET Core MVC

## 🙏 Acknowledgments

- ASP.NET Core documentation
- Entity Framework Core team
- Neon for serverless PostgreSQL
- Render for easy deployment

---

**Built with ❤️ using ASP.NET Core 9.0**
