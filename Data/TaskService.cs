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
        private readonly ITodoService _todoService;

        public TodotaskService(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public void ToggleTaskStatus(int todoId, int taskId)
        {
            var todo = _todoService.GetTodoById(todoId);
            if (todo?.Tasks != null)
            {
                var task = todo.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.IsCompleted = !task.IsCompleted;
                }
            }
        }

        public void DeleteTask(int todoId, int taskId)
        {
            var todo = _todoService.GetTodoById(todoId);
            if (todo?.Tasks != null)
            {
                var taskToDelete = todo.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (taskToDelete != null)
                {
                    var taskList = todo.Tasks.ToList();
                    taskList.Remove(taskToDelete);
                    todo.Tasks = taskList.ToArray();
                }
            }
        }

        public void AddTask(int todoId, Todotask task)
        {
            var todo = _todoService.GetTodoById(todoId);
            if (todo?.Tasks != null)
            {
                task.Id = todo.Tasks.Length > 0 ? todo.Tasks.Max(t => t.Id) + 1 : 1;
                var taskList = todo.Tasks.ToList();
                taskList.Add(task);
                todo.Tasks = taskList.ToArray();
            }
        }
    }
}
