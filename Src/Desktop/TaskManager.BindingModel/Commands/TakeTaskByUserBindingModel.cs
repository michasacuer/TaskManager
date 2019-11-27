namespace TaskManager.BindingModel.Commands
{
    public class TakeTaskByUserBindingModel : Command
    {
        public string ApplicationUserId { get; set; }

        public int TaskId { get; set; }
    }
}
