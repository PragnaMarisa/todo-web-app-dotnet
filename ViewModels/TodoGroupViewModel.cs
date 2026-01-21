using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.ViewModels
{
    public class TodoGroupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }

        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int ProgressPercentage { get; set; }

        public List<Todotask> Tasks { get; set; } = new();
    }
}
