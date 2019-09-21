namespace TaskManager.BindingModel.Commands
{
    public class EndTaskByUserBindingModel : Command
    {
        public string ApplicationUserId { get; set; }

        public int TaskId { get; set; }
    }
}
