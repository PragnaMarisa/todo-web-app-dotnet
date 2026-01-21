using System;
using System.Linq;
using Xunit;
using todo_web_app_dotnet.Data;
using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Tests
{
    public class TodoServiceTests
    {
        private ITodoService CreateService() => new TodoService();

        [Fact]
        public void GetAllTodos_StartsEmpty()
        {
            var service = CreateService();
            Assert.Empty(service.GetAllTodos());
        }

        [Fact]
        public void AddTodo_AssignsIdAndAdds()
        {
            var service = CreateService();
            var todo = new Todo { Title = "Test", Description = "Desc" };
            service.AddTodo(todo);

            var all = service.GetAllTodos();
            Assert.Single(all);
            Assert.True(all[0].Id > 0);
            Assert.Equal("Test", all[0].Title);
        }

        [Fact]
        public void GetTodoById_ReturnsNullWhenNotFound()
        {
            var service = CreateService();
            Assert.Null(service.GetTodoById(999));
        }

        [Fact]
        public void GetTodoById_ReturnsTodoWhenFound()
        {
            var service = CreateService();
            var todo = new Todo { Title = "Find Me" };
            service.AddTodo(todo);
            var added = service.GetAllTodos().First();

            var found = service.GetTodoById(added.Id);
            Assert.NotNull(found);
            Assert.Equal("Find Me", found.Title);
        }

        [Fact]
        public void DeleteTodo_RemovesItem()
        {
            var service = CreateService();
            var todo = new Todo { Title = "ToDelete" };
            service.AddTodo(todo);
            var addedId = service.GetAllTodos().First().Id;

            service.DeleteTodo(addedId);

            Assert.Empty(service.GetAllTodos());
        }

        [Fact]
        public void AddTodo_MultipleItems_AssignsUniqueIds()
        {
            var service = CreateService();
            service.AddTodo(new Todo { Title = "T1" });
            service.AddTodo(new Todo { Title = "T2" });
            service.AddTodo(new Todo { Title = "T3" });

            var all = service.GetAllTodos();
            Assert.Equal(3, all.Count);
            Assert.NotEqual(all[0].Id, all[1].Id);
            Assert.NotEqual(all[1].Id, all[2].Id);
        }
    }
}
