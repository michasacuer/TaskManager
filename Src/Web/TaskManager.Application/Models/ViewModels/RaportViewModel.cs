namespace TaskManager.Application.Models.ViewModels
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity;

    public class RaportViewModel
    {
        public Project Project { get; set; }

        public List<ToDoTask> ProjectTasks { get; set; }

        public int SpentStoryPoints { get; set; }

        public int RemainingStoryPoints { get; set; }
    }
}
