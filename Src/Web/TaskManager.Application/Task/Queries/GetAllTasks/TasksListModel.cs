namespace TaskManager.Application.Task.Queries.GetAllTasks
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity;

    public class TasksListModel
    {
        public IEnumerable<ToDoTask> List { get; set; }
    }
}
