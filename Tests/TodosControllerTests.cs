using Xunit;
using Moq;
using todo_web_app_dotnet.Controllers;
using todo_web_app_dotnet.Data;
using todo_web_app_dotnet.Models;
using todo_web_app_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace todo_web_app_dotnet.Tests
{
    public class TodosControllerTests
    {
        [Fact]
        public void Index_ReturnsView()
        {
            var controller = new TodosController(
                Mock.Of<ITodoService>(),
                Mock.Of<ITodotaskService>(),
                Mock.Of<IAllTodosViewModelBuilder>()
            );
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllTodos_CallsServiceAndBuilder()
        {
            var todos = new List<Todo>();
            var vm = new AllTodosViewModel();

            var todoService = new Mock<ITodoService>();
            todoService.Setup(s => s.GetAllTodos()).Returns(todos);

            var builder = new Mock<IAllTodosViewModelBuilder>();
            builder.Setup(b => b.Build(todos)).Returns(vm);

            var controller = new TodosController(
                todoService.Object,
                Mock.Of<ITodotaskService>(),
                builder.Object
            );

            var result = controller.AllTodos() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(vm, result.Model);
            todoService.Verify(s => s.GetAllTodos(), Times.Once);
            builder.Verify(b => b.Build(todos), Times.Once);
        }

        [Fact]
        public void AddTask_EmptyTitle_RedirectsWithoutCalling()
        {
            var taskService = new Mock<ITodotaskService>();
            var controller = new TodosController(
                Mock.Of<ITodoService>(),
                taskService.Object,
                Mock.Of<IAllTodosViewModelBuilder>()
            );
            controller.TempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
                new Microsoft.AspNetCore.Http.DefaultHttpContext(),
                Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>()
            );

            var result = controller.AddTask(new Todotask { Title = "" }, 1) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("AllTodos", result.ActionName);
            taskService.Verify(s => s.AddTask(It.IsAny<int>(), It.IsAny<Todotask>()), Times.Never);
        }

        [Fact]
        public void AddTask_ValidTitle_CallsServiceAndRedirects()
        {
            var taskService = new Mock<ITodotaskService>();
            var controller = new TodosController(
                Mock.Of<ITodoService>(),
                taskService.Object,
                Mock.Of<IAllTodosViewModelBuilder>()
            );

            var result = controller.AddTask(new Todotask { Title = "NewTask" }, 5) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("AllTodos", result.ActionName);
            taskService.Verify(s => s.AddTask(5, It.Is<Todotask>(t => t.Title == "NewTask")), Times.Once);
        }

        [Fact]
        public void AddTodo_EmptyTitle_RedirectsWithoutCalling()
        {
            var todoService = new Mock<ITodoService>();
            var controller = new TodosController(
                todoService.Object,
                Mock.Of<ITodotaskService>(),
                Mock.Of<IAllTodosViewModelBuilder>()
            );
            controller.TempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
                new Microsoft.AspNetCore.Http.DefaultHttpContext(),
                Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>()
            );

            var result = controller.AddTodo(new Todo { Title = "" }) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("AllTodos", result.ActionName);
            todoService.Verify(s => s.AddTodo(It.IsAny<Todo>()), Times.Never);
        }

        [Fact]
        public void AddTodo_ValidTitle_CallsServiceAndRedirects()
        {
            var todoService = new Mock<ITodoService>();
            var controller = new TodosController(
                todoService.Object,
                Mock.Of<ITodotaskService>(),
                Mock.Of<IAllTodosViewModelBuilder>()
            );

            var result = controller.AddTodo(new Todo { Title = "NewGroup" }) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("AllTodos", result.ActionName);
            todoService.Verify(s => s.AddTodo(It.Is<Todo>(t => t.Title == "NewGroup")), Times.Once);
        }

        [Fact]
        public void ToggleStatus_CallsServiceAndRedirects()
        {
            var taskService = new Mock<ITodotaskService>();
            var controller = new TodosController(
                Mock.Of<ITodoService>(),
                taskService.Object,
                Mock.Of<IAllTodosViewModelBuilder>()
            );

            var result = controller.ToggleStatus(3, 7) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("AllTodos", result.ActionName);
            taskService.Verify(s => s.ToggleTaskStatus(3, 7), Times.Once);
        }

        [Fact]
        public void DeleteTask_CallsServiceAndRedirects()
        {
            var taskService = new Mock<ITodotaskService>();
            var controller = new TodosController(
                Mock.Of<ITodoService>(),
                taskService.Object,
                Mock.Of<IAllTodosViewModelBuilder>()
            );

            var result = controller.DeleteTask(2, 4) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("AllTodos", result.ActionName);
            taskService.Verify(s => s.DeleteTask(2, 4), Times.Once);
        }

        [Fact]
        public void DeleteTodo_CallsServiceAndRedirects()
        {
            var todoService = new Mock<ITodoService>();
            var controller = new TodosController(
                todoService.Object,
                Mock.Of<ITodotaskService>(),
                Mock.Of<IAllTodosViewModelBuilder>()
            );

            var result = controller.DeleteTodo(6) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("AllTodos", result.ActionName);
            todoService.Verify(s => s.DeleteTodo(6), Times.Once);
        }
    }
}
