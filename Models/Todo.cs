namespace todo_web_app_dotnet.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Todotask> Tasks { get; set; } = new List<Todotask>();
    }
}
