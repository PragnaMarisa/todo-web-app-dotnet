using todo_web_app_dotnet.Models;
using todo_web_app_dotnet.ViewModels;

namespace todo_web_app_dotnet.Data
{
    public interface IAllTodosViewModelBuilder
    {
        AllTodosViewModel Build(List<Todo> todos);
    }

    public class AllTodosViewModelBuilder : IAllTodosViewModelBuilder
    {
        public AllTodosViewModel Build(List<Todo> todos)
        {
            todos ??= new();

            return new AllTodosViewModel
            {
                TotalGroups = todos.Count,
                TotalTasks = todos.Sum(t => t.Tasks?.Length ?? 0),
                CompletedTasks = todos.Sum(t => t.Tasks?.Count(x => x.IsCompleted) ?? 0),
                Groups = todos.Select(todo =>
                {
                    var tasks = todo.Tasks?.ToList() ?? new();
                    var totalTasks = tasks.Count;
                    var completed = tasks.Count(t => t.IsCompleted);

                    return new TodoGroupViewModel
                    {
                        Id = todo.Id,
                        Title = todo.Title ?? "",
                        Description = todo.Description,
                        TotalTasks = totalTasks,
                        CompletedTasks = completed,
                        ProgressPercentage = totalTasks > 0 ? (completed * 100) / totalTasks : 0,
                        Tasks = tasks
                    };
                }).ToList()
            };
        }
    }
}

