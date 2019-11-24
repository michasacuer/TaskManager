namespace TaskManager.Contracts.Data
{
    using System.Net.Http;
    using TaskManager.Contracts.Extensions;

    public abstract class BaseContract
    {
        protected readonly HttpClient httpClient;

        public BaseContract(string bearer)
        {
            this.httpClient = new HttpClient();
            this.httpClient.Authorize(bearer);
        }
    }
}
