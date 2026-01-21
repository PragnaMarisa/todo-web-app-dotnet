namespace todo_web_app_dotnet.ViewModels
{
    public class AllTodosViewModel
    {
        public int TotalGroups { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }

        public List<TodoGroupViewModel> Groups { get; set; } = new();
    }
}