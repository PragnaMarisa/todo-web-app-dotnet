using System;
using System.Linq;
using Xunit;
using todo_web_app_dotnet.Data;
using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Tests
{
    public class TodotaskServiceTests
    {
        [Fact]
        public void AddTask_CreatesTaskWithValidTodoId()
        {
            var todoService = new TodoService();
            var taskService = new TodotaskService(todoService);

            var todo = new Todo { Title = "Group" };
            todoService.AddTodo(todo);
            var addedTodo = todoService.GetAllTodos().First();

            var newTask = new Todotask { Title = "Task1", Description = "D" };
            taskService.AddTask(addedTodo.Id, newTask);

            var afterAdd = todoService.GetTodoById(addedTodo.Id);
            Assert.NotNull(afterAdd);
            Assert.NotNull(afterAdd.Tasks);
            Assert.Single(afterAdd.Tasks);
            Assert.Equal("Task1", afterAdd.Tasks![0].Title);
            Assert.False(afterAdd.Tasks![0].IsCompleted);
        }

        [Fact]
        public void ToggleTaskStatus_TogglesBetweenCompleteAndIncomplete()
        {
            var todoService = new TodoService();
            var taskService = new TodotaskService(todoService);

            var todo = new Todo { Title = "Group" };
            todoService.AddTodo(todo);
            var addedTodo = todoService.GetAllTodos().First();

            var newTask = new Todotask { Title = "Task1" };
            taskService.AddTask(addedTodo.Id, newTask);
            var taskId = todoService.GetTodoById(addedTodo.Id)!.Tasks![0].Id;

            taskService.ToggleTaskStatus(addedTodo.Id, taskId);
            Assert.True(todoService.GetTodoById(addedTodo.Id)!.Tasks![0].IsCompleted);

            taskService.ToggleTaskStatus(addedTodo.Id, taskId);
            Assert.False(todoService.GetTodoById(addedTodo.Id)!.Tasks![0].IsCompleted);
        }

        [Fact]
        public void DeleteTask_RemovesTaskFromTodo()
        {
            var todoService = new TodoService();
            var taskService = new TodotaskService(todoService);

            var todo = new Todo { Title = "Group" };
            todoService.AddTodo(todo);
            var addedTodo = todoService.GetAllTodos().First();

            var newTask = new Todotask { Title = "Task1" };
            taskService.AddTask(addedTodo.Id, newTask);
            var taskId = todoService.GetTodoById(addedTodo.Id)!.Tasks![0].Id;

            taskService.DeleteTask(addedTodo.Id, taskId);
            Assert.Empty(todoService.GetTodoById(addedTodo.Id)!.Tasks!);
        }

        [Fact]
        public void AddTask_DoesNothingIfTodoDoesNotExist()
        {
            var todoService = new TodoService();
            var taskService = new TodotaskService(todoService);

            var task = new Todotask { Title = "Orphan" };
            taskService.AddTask(999, task);

            Assert.Empty(todoService.GetAllTodos());
        }
    }
}
