namespace TaskManager.Contracts.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Entity;

    public interface IEndedTaskContract
    {
        Task<List<EndedTask>> GetAllAsync();

        Task<EndedTask> GetAsync(int id);
    }
}
