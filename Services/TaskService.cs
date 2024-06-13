using Bogus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks;

        public TaskService()
        {
            _tasks = GenerateFakeTasks(100);
        }

        private List<TaskItem> GenerateFakeTasks(int count)
        {
            var faker = new Faker<TaskItem>()
                .RuleFor(t => t.Id, f => f.IndexFaker + 1)
                .RuleFor(t => t.Title, f => f.Lorem.Sentence(3))
                .RuleFor(t => t.Description, f => f.Lorem.Sentence(6));

            return faker.Generate(count);
        }

        public Task<List<TaskItem>> GetTasksAsync(int pageNumber, int pageSize)
        {
            var tasks = _tasks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult(tasks);
        }

        public Task<int> GetTotalTasksCountAsync()
        {
            return Task.FromResult(_tasks.Count);
        }

        public Task AddTaskAsync(TaskItem task)
        {
            task.Id = _tasks.Max(t => t.Id) + 1;
            _tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task UpdateTaskAsync(TaskItem task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;
            }
            return Task.CompletedTask;
        }

        public Task DeleteTaskAsync(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _tasks.Remove(task);
            }
            return Task.CompletedTask;
        }
                public Task<TaskItem> GetTaskByIdAsync(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            return Task.FromResult(task);
        }
    }
}
