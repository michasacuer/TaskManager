namespace TaskManager.Domain.Entity
{
    using System;
    using TaskManager.Domain.Enum;
    using TaskManager.Domain.Entity.Base;

    public class Task : BaseEntity<int>
    {
        public Task()
        {
        }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public int? StoryPoints { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
