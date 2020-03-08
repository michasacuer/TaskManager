namespace TaskManager.Domain.Entity
{
    using System;
    using TaskManager.Domain.Entity.Base;
    
    public class Comment : BaseEntity<int>
    {
        public int ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int ToDoTaskId { get; set; }

        public ToDoTask ToDoTask { get; set; }

        public string Content { get; set; }

        public DateTime AddTime { get; set; }
    }
}
