using todo_web_app_dotnet.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodotaskService, TodotaskService>();
builder.Services.AddScoped<IAllTodosViewModelBuilder, AllTodosViewModelBuilder>();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "main",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
