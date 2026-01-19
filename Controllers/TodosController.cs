using Microsoft.AspNetCore.Mvc;
using todo_web_app_dotnet.Data;
using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Controllers
{
    public class TodosController : Controller
    {
        private readonly ITodoService _todoService;
        private readonly ITodotaskService _todotaskService;

        public TodosController(ITodoService todoService, ITodotaskService todotaskService)
        {
            _todoService = todoService;
            _todotaskService = todotaskService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllTodos()
        {
            var todos = _todoService.GetAllTodos();
            return View(todos);
        }

        [HttpPost]
        public IActionResult AddTask(Todotask task, int todoId)
        {
            if (string.IsNullOrEmpty(task.Title))
            {
                ModelState.AddModelError("Title", "Title is required");
                return View(task);
            }

            _todotaskService.AddTask(todoId, task);
            return RedirectToAction("AllTodos");
        }
        [HttpPost]
        public IActionResult AddTodo(Todo todo)
        {
            if (string.IsNullOrEmpty(todo.Title))
            {
                ModelState.AddModelError("Title", "Title is required");
                return View(todo);
            }

            _todoService.AddTodo(todo);
            return RedirectToAction("AllTodos");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id, int taskId)
        {
            _todotaskService.ToggleTaskStatus(id, taskId);
            return RedirectToAction("AllTodos");
        }

        [HttpPost]
        public IActionResult DeleteTask(int id, int taskId)
        {
            _todotaskService.DeleteTask(id, taskId);
            return RedirectToAction("AllTodos");
        }   
        [HttpPost]
        public IActionResult DeleteTodo(int id)
        {
            _todoService.DeleteTodo(id);
            return RedirectToAction("AllTodos");
        }   
    }
}
