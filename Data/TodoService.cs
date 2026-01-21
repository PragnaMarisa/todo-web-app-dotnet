using todo_web_app_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace todo_web_app_dotnet.Data
{
    public interface ITodoService
    {
        List<Todo> GetAllTodos();
        void AddTodo(Todo todo);
        void DeleteTodo(int id);
        Todo? GetTodoById(int id);
    }

    public class TodoService : ITodoService
    {
        private readonly MyDbContext _context;

        public TodoService(MyDbContext context)
        {
            _context = context;
        }

        public List<Todo> GetAllTodos()
        {
            return _context.Todos
                .Include(t => t.Tasks)
                .ToList();
        }

        public Todo? GetTodoById(int id)
        {
            return _context.Todos
                .Include(t => t.Tasks)
                .FirstOrDefault(t => t.Id == id);
        }

        public void AddTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public void DeleteTodo(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
        }

    }
}
