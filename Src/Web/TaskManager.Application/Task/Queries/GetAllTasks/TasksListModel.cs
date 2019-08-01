namespace TaskManager.Application.Task.Queries
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity;

    public class TasksListModel
    {
        public IEnumerable<ToDoTask> ToDoTasks { get; set; }
    }
}
