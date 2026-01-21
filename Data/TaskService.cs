using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Data
{
    public interface ITodotaskService
    {
        void ToggleTaskStatus(int todoId, int taskId);
        void DeleteTask(int todoId, int taskId);
        void AddTask(int todoId, Todotask task);
    }

    public class TodotaskService : ITodotaskService
    {
        private readonly MyDbContext _context;

        public TodotaskService(MyDbContext context)
        {
            _context = context;
        }

        public void ToggleTaskStatus(int todoId, int taskId)
        {
            var task = _context.Todotasks.FirstOrDefault(t => t.Id == taskId && t.TodoId == todoId);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _context.SaveChanges();
            }
        }

        public void DeleteTask(int todoId, int taskId)
        {
            var task = _context.Todotasks.FirstOrDefault(t => t.Id == taskId && t.TodoId == todoId);
            if (task != null)
            {
                _context.Todotasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public void AddTask(int todoId, Todotask task)
        {
            task.TodoId = todoId;
            task.CreatedDate = DateTime.UtcNow;
            _context.Todotasks.Add(task);
            _context.SaveChanges();
        }
    }
}
