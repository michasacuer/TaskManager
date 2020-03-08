namespace TaskManager.Domain.Entity
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity.Base;

    public class Project : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ToDoTask> Tasks { get; set; }
    }
}
