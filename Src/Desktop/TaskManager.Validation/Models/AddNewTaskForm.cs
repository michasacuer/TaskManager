namespace TaskManager.Validation.Models
{
    using TaskManager.Entity;
    using TaskManager.Entity.Enum;
    
    public class AddNewTaskForm
    {
        public string TaskName { get; set; }

        public string Description { get; set; }

        public Project SelectedProject { get; set; }

        public Priority Priority { get; set; }

        public bool LowPriorityButton { get; set; }

        public bool MediumPriorityButton { get; set; }

        public bool HighPriorityButton { get; set; }
    }
}
