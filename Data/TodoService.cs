using todo_web_app_dotnet.Models;

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
        private static List<Todo> todos = new List<Todo>();

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
            todo.Id = todos.Any() ? todos.Max(t => t.Id) + 1 : 1;
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
