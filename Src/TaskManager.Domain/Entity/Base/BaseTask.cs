namespace TaskManager.Domain.Entity.Base
{
    using System;
    using TaskManager.Domain.Enum;

    public class BaseTask : BaseEntity<int>
    {
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public int? StoryPoints { get; set; }

        public DateTime? StartTime { get; set; }
    }
}
