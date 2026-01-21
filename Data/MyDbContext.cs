using Microsoft.EntityFrameworkCore;
using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; } = null!;
    }
}
