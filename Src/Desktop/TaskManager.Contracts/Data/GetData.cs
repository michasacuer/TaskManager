namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Entity;

    public class GetData
    {
        private HttpClient httpClient;

        public GetData()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<List<Project>> GetProjects() => await this.httpClient.Get<Project>();
    }
}
