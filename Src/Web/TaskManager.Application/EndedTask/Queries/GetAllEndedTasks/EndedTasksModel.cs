namespace TaskManager.Application.EndedTask.Queries.GetAllEndedTasks
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity;

    public class EndedTasksModel
    {
        public IEnumerable<EndedTask> EndedTasks { get; set; }
    }
}
