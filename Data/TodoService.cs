using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Data
{
    public interface ITodoService
    {
        List<Todo> GetAllTodos();
        void AddTodo(Todo todo);
        void DeleteTodo(int id);
        Todo GetTodoById(int id);
    }

    public class TodoService : ITodoService
    {
        private static List<Todo> todos = new List<Todo>
        {
            new Todo 
            { 
                Id = 1, 
                Title = "Project Setup", 
                Description = "Initialize and configure the project",
                Tasks = new Models.Todotask[]
                {
                    new Models.Todotask
                    {
                        Id = 1,
                        Title = "Complete Project Setup",
                        Description = "Set up the initial ASP.NET Core project structure",
                        IsCompleted = true,
                        CreatedDate = DateTime.UtcNow.AddDays(-5)
                    },
                    new Models.Todotask
                    {
                        Id = 2,
                        Title = "Install Dependencies",
                        Description = "Install required NuGet packages",
                        IsCompleted = true,
                        CreatedDate = DateTime.UtcNow.AddDays(-4)
                    }
                }
            },
            new Todo 
            { 
                Id = 2, 
                Title = "Backend Development", 
                Description = "Build the API and data layer",
                Tasks = new Models.Todotask[]
                {
                    new Models.Todotask
                    {
                        Id = 3,
                        Title = "Create Todo Model",
                        Description = "Create the Todo model class with required properties",
                        IsCompleted = true,
                        CreatedDate = DateTime.UtcNow.AddDays(-4)
                    },
                    new Models.Todotask
                    {
                        Id = 4,
                        Title = "Build Todo API Endpoint",
                        Description = "Implement the API endpoint for fetching todos",
                        IsCompleted = false,
                        CreatedDate = DateTime.UtcNow.AddDays(-2)
                    },
                    new Models.Todotask
                    {
                        Id = 5,
                        Title = "Add Database Integration",
                        Description = "Integrate Entity Framework Core with the application",
                        IsCompleted = false,
                        CreatedDate = DateTime.UtcNow.AddHours(-6)
                    },
                }
            },
            new Todo 
            { 
                Id = 3, 
                Title = "Frontend Development", 
                Description = "Design and implement UI pages",
                Tasks = new Models.Todotask[]
                {
                    new Models.Todotask
                    {
                        Id = 6,
                        Title = "Design UI Pages",
                        Description = "Design the NoTodos and AllTodos Razor views",
                        IsCompleted = false,
                        CreatedDate = DateTime.UtcNow.AddHours(-12)
                    },
                    new Models.Todotask
                    {
                        Id = 7,
                        Title = "Add Styling",
                        Description = "Apply CSS styling to the views",
                        IsCompleted = false,
                        CreatedDate = DateTime.UtcNow.AddHours(-10)
                    }
                }
            }
        };

        public List<Todo> GetAllTodos()
        {
            return todos;
        }

        public Todo? GetTodoById(int id)
        {
            return todos.FirstOrDefault(t => t.Id == id);
        }

        public void AddTodo(Todo todo)
        {
            todo.Id = todos.Count + 1;
            todo.Tasks = todo.Tasks ?? new Todotask[] { };
            todos.Add(todo);
        }

        public void DeleteTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todos.Remove(todo);
            }
        }
    }
}
