namespace TaskManager.Entity
{
    using TaskManager.Entity.Base;

    public class Notification : BaseEntity<int>
    {
        public string Message { get; set; }
    }
}
