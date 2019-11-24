namespace TaskManager.Entity
{
    using System.Collections.Generic;
    using TaskManager.Entity.Base;

    public class Project : BaseEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ToDoTask> Tasks { get; set; }

        public List<EndedTask> EndedTasks { get; set; }
    }
}
