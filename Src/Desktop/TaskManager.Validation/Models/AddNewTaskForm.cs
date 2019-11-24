using TaskManager.Entity.Enum;

namespace TaskManager.Validation.Models
{
    public class AddNewTaskForm
    {
        public string TaskName { get; set; }

        public int SelectedProject { get; set; }

        public Priority? Priority { get; set; }

        public bool LowPriorityButton { get; set; }

        public bool MediumPriorityButton { get; set; }

        public bool HighPriorityButton { get; set; }
    }
}
