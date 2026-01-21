using System.Collections.Generic;
using Xunit;
using todo_web_app_dotnet.Data;
using todo_web_app_dotnet.Models;

namespace todo_web_app_dotnet.Tests
{
    public class AllTodosViewModelBuilderTests
    {
        [Fact]
        public void Build_WithEmptyList_ProducesZeroCounts()
        {
            var builder = new AllTodosViewModelBuilder();
            var vm = builder.Build(new List<Todo>());
            
            Assert.Equal(0, vm.TotalGroups);
            Assert.Equal(0, vm.TotalTasks);
            Assert.Equal(0, vm.CompletedTasks);
            Assert.Empty(vm.Groups);
        }

        [Fact]
        public void Build_WithNullList_ProducesZeroCounts()
        {
            var builder = new AllTodosViewModelBuilder();
            var vm = builder.Build(null!);
            
            Assert.Equal(0, vm.TotalGroups);
            Assert.Equal(0, vm.TotalTasks);
        }

        [Fact]
        public void Build_ComputesProgressPercentageCorrectly()
        {
            var todos = new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Title = "G1",
                    Tasks = new[]
                    {
                        new Todotask{ Id=1, Title="A", IsCompleted=true },
                        new Todotask{ Id=2, Title="B", IsCompleted=false }
                    }
                }
            };
            var builder = new AllTodosViewModelBuilder();
            var vm = builder.Build(todos);

            Assert.Equal(1, vm.TotalGroups);
            Assert.Equal(2, vm.TotalTasks);
            Assert.Equal(1, vm.CompletedTasks);
            Assert.Single(vm.Groups);
            Assert.Equal(50, vm.Groups[0].ProgressPercentage);
        }

        [Fact]
        public void Build_WithMultipleGroups_AggregatesStats()
        {
            var todos = new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Title = "G1",
                    Tasks = new[]
                    {
                        new Todotask{ Id=1, Title="A", IsCompleted=true },
                        new Todotask{ Id=2, Title="B", IsCompleted=true }
                    }
                },
                new Todo
                {
                    Id = 2,
                    Title = "G2",
                    Tasks = new[]
                    {
                        new Todotask{ Id=3, Title="C", IsCompleted=false }
                    }
                }
            };
            var builder = new AllTodosViewModelBuilder();
            var vm = builder.Build(todos);

            Assert.Equal(2, vm.TotalGroups);
            Assert.Equal(3, vm.TotalTasks);
            Assert.Equal(2, vm.CompletedTasks);
        }

        [Fact]
        public void Build_WithNoTasks_SetProgressToZero()
        {
            var todos = new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Title = "G1",
                    Tasks = new Todotask[] { }
                }
            };
            var builder = new AllTodosViewModelBuilder();
            var vm = builder.Build(todos);

            Assert.Equal(0, vm.Groups[0].ProgressPercentage);
        }
    }
}
