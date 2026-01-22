namespace todo_web_app_dotnet.Models
{
    public class Todotask
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TodoId { get; set; }
    }
}
