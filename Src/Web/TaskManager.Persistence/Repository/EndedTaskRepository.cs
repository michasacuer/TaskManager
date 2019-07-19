namespace TaskManager.Persistence.Repository
{
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class EndedTaskRepository : Repository<EndedTask>, IEndedTaskRepository
    {
        public EndedTaskRepository(TaskManagerDbContext context)
            : base(context)
        {
        }
    }
}
