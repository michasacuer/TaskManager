namespace TaskManager.Entity
{
    using System;
    using TaskManager.Entity.Base;

    public class EndedTask : BaseTask
    {
        public int? SpentStoryPoints { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
