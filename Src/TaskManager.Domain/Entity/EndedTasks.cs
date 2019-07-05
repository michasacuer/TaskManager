﻿namespace TaskManager.Domain.Entity
{
    using System;
    using TaskManager.Domain.Entity.Base;

    public class EndedTask : BaseTask
    {
        public EndedTask()
        {
        }

        public int? SpentStoryPoints { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
