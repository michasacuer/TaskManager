namespace TaskManager.Domain.Entity.Base
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
