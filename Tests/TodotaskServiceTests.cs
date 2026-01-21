using System;
using System.Linq;
using Xunit;
using todo_web_app_dotnet.Data;
using todo_web_app_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace todo_web_app_dotnet.Tests
{
    public class TodotaskServiceTests
    {
        [Fact]
        public void AddTask_AddsTaskToTodo()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new MyDbContext(options);
            var todoService = new TodoService(context);
            var taskService = new TodotaskService(context);

            var todo = new Todo { Title = "Group" };
            todoService.AddTodo(todo);
            var addedTodo = todoService.GetAllTodos().First();

            var newTask = new Todotask { Title = "Task1", Description = "D" };
            taskService.AddTask(addedTodo.Id, newTask);

            var afterAdd = todoService.GetTodoById(addedTodo.Id);
            Assert.NotNull(afterAdd);
            Assert.NotNull(afterAdd.Tasks);
            Assert.Single(afterAdd.Tasks);
            Assert.Equal("Task1", afterAdd.Tasks!.First().Title);
            Assert.False(afterAdd.Tasks!.First().IsCompleted);
        }

        [Fact]
        public void ToggleTaskStatus_TogglesBetweenCompleteAndIncomplete()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new MyDbContext(options);
            var todoService = new TodoService(context);
            var taskService = new TodotaskService(context);

            var todo = new Todo { Title = "Group" };
            todoService.AddTodo(todo);
            var addedTodo = todoService.GetAllTodos().First();

            var newTask = new Todotask { Title = "Task1" };
            taskService.AddTask(addedTodo.Id, newTask);
            var taskId = todoService.GetTodoById(addedTodo.Id)!.Tasks!.First().Id;

            taskService.ToggleTaskStatus(addedTodo.Id, taskId);
            Assert.True(todoService.GetTodoById(addedTodo.Id)!.Tasks!.First().IsCompleted);

            taskService.ToggleTaskStatus(addedTodo.Id, taskId);
            Assert.False(todoService.GetTodoById(addedTodo.Id)!.Tasks!.First().IsCompleted);
        }

        [Fact]
        public void DeleteTask_RemovesTaskFromTodo()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new MyDbContext(options);
            var todoService = new TodoService(context);
            var taskService = new TodotaskService(context);

            var todo = new Todo { Title = "Group" };
            todoService.AddTodo(todo);
            var addedTodo = todoService.GetAllTodos().First();

            var newTask = new Todotask { Title = "Task1" };
            taskService.AddTask(addedTodo.Id, newTask);
            var taskId = todoService.GetTodoById(addedTodo.Id)!.Tasks!.First().Id;

            taskService.DeleteTask(addedTodo.Id, taskId);
            Assert.Empty(todoService.GetTodoById(addedTodo.Id)!.Tasks!);
        }

        [Fact]
        public void AddTask_DoesNothingIfTodoDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new MyDbContext(options);
            var todoService = new TodoService(context);
            var taskService = new TodotaskService(context);

            var task = new Todotask { Title = "Orphan" };
            taskService.AddTask(999, task);

            Assert.Empty(todoService.GetAllTodos());
        }
    }
}
