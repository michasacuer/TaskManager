namespace TaskManager.Domain.Entity
{
    using System;
    using TaskManager.Domain.Enum;

    public class EndedTask
    {
        public EndedTask()
        {
        }

        public int Id { get; set; }

        public int TaskId { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public int? StoryPoints { get; set; }

        public int? SpentStoryPoints { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
