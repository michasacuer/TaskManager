namespace TaskManager.Domain.Entity.Base
{
    using System.Collections.Generic;

    public class Project
    {
        public Project()
        {
        }

        public int Id { get; set; }

        public string Tag { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<EndedTask> EndedTasks { get; set; }
    }
}
