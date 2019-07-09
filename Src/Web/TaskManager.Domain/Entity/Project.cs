namespace TaskManager.Domain.Entity
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity.Base;

    public class Project : BaseEntity<int>
    {
        public Project()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<EndedTask> EndedTasks { get; set; }
    }
}
