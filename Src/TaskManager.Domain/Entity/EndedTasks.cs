namespace TaskManager.Domain.Entity
{
    using System;

    public class EndedTask : Task
    {
        public EndedTask()
        {
        }

        public int? SpentStoryPoints { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
