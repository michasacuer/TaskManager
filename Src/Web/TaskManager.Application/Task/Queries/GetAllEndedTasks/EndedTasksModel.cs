using System.Collections.Generic;
using TaskManager.Domain.Entity;

namespace TaskManager.Application.Task.Queries.GetAllEndedTasks
{
    public class EndedTasksModel
    {
        public IEnumerable<ToDoTask> List { get; set; }
    }
}
