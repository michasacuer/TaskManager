namespace TaskManager.Persistence.Repository
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;

    public class Repository<T> : IRepository<T> 
        where T : class
    {
        protected readonly ITaskManagerDbContext context;

        public Repository(ITaskManagerDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T item)
        {
            await this.context.Set<T>().AddAsync(item);
        }

        public void Delete(T item)
        {
            this.context.Set<T>().Remove(item);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }

        public void Update(T item)
        {
            this.context.Entry(item).State = EntityState.Modified;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
