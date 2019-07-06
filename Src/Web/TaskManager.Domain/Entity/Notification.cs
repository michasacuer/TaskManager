namespace TaskManager.Domain.Entity
{
    using TaskManager.Domain.Entity.Base;

    public class Notification : BaseEntity<int>
    {
        public string Message { get; set; }
    }
}
