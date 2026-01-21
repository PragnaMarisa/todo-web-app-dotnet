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
        private  List<Todo> _todos = new List<Todo>();

        public List<Todo> GetAllTodos()
        {
            return _todos;
        }

        public Todo? GetTodoById(int id)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }

        public void AddTodo(Todo todo)
        {
            todo.Id = _todos.Any() ? _todos.Max(t => t.Id) + 1 : 1;
            todo.Tasks = todo.Tasks ?? new Todotask[] { };
            _todos.Add(todo);
        }

        public void DeleteTodo(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                _todos.Remove(todo);
            }
        }

    }
}
