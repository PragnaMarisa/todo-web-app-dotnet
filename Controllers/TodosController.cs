using Microsoft.AspNetCore.Mvc;

namespace todo_web_app_dotnet.Controllers
{
    public class TodosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
