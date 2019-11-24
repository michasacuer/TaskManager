namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;
    using TaskManager.Entity.JSONMapper;

    public class EndedTaskContract : BaseContract, IEndedTaskContract
    {
        public EndedTaskContract(string bearer)
            : base(bearer)
        {
        }

        public async Task<List<EndedTask>> GetAllAsync() => await base.httpClient.GetAsync<EndedTasks, EndedTask>();

        public async Task<EndedTask> GetAsync(int taskId) => await base.httpClient.GetAsync<EndedTask>(taskId);
    }
}
