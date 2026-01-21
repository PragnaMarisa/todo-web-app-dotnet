using Microsoft.AspNetCore.Mvc;
using todo_web_app_dotnet.Data;
using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Controllers
{
    public class TodosController : Controller
    {
        private readonly ITodoService _todoService;
        private readonly ITodotaskService _todotaskService;
        private readonly IAllTodosViewModelBuilder _viewModelBuilder;

        public TodosController(
            ITodoService todoService,
            ITodotaskService todotaskService,
            IAllTodosViewModelBuilder viewModelBuilder)
        {
            _todoService = todoService;
            _todotaskService = todotaskService;
            _viewModelBuilder = viewModelBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllTodos()
        {
            var todos = _todoService.GetAllTodos();
            var viewModel = _viewModelBuilder.Build(todos);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddTask(Todotask task, int todoId)
        {
            if (string.IsNullOrEmpty(task.Title))
            {
                ModelState.AddModelError("Title", "Title is required");
                return RedirectToAction("AllTodos");
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
                return RedirectToAction("AllTodos");
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
