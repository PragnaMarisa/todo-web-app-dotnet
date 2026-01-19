using todo_web_app_dotnet.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ITodoService, TodoService>();
builder.Services.AddSingleton<ITodotaskService, TodotaskService>();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "main",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
