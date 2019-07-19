namespace TaskManager.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;

    public class Repository<T> : IRepository<T> 
        where T : class
    {
        protected readonly TaskManagerDbContext Context;

        public Repository(TaskManagerDbContext context)
        {
            this.Context = context;
        }

        public async Task Add(T item)
        {
            await this.Context.Set<T>().AddAsync(item);
            await this.Context.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            this.Context.Set<T>().Remove(item);
            await this.Context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await this.Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await this.Context.Set<T>().FindAsync(id);
        }

        public async Task Update(T item)
        {
            this.Context.Entry(item).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();
        }
    }
}
