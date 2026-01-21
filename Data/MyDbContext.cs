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
        public DbSet<Todotask> Todotasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>()
                .HasMany(t => t.Tasks)
                .WithOne(tt => tt.Todo)
                .HasForeignKey(tt => tt.TodoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
