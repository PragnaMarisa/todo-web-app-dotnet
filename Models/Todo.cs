namespace todo_web_app_dotnet.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Todotask[]? Tasks { get; set; }
    }
}
