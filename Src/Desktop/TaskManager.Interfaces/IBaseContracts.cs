namespace TaskManager.Contracts.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Entity.Base;

    public interface IBaseContracts<T>
        where T : BaseEntity<int>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);
    }
}
