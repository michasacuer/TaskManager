    namespace TaskManager.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Persistence.Infrastructure;

    public class SSParkDbContextFactory : DesignTimeDbContextFactoryBase<TaskManagerDbContext>
    {
        protected override TaskManagerDbContext CreateNewInstance(DbContextOptions<TaskManagerDbContext> options)
        {
            return new TaskManagerDbContext(options);
        }
    }
}
