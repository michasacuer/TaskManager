namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Entity;

    public class GetData
    {
        private HttpClient client;

        public GetData()
        {
            this.client = new HttpClient();
        }

        public async Task<List<Project>> GetProjects()
        {
            var response = await this.client.GetAsync(UrlBuilder.BuildEndpoint("Project"));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Project>>();
            }
            else
            {
                return default;
            }
        }
    }
}
