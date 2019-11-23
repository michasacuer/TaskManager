namespace TaskManager.Contracts.Data
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Exceptions;

    public class TestConnection
    {
        private HttpClient httpClient;

        public TestConnection()
        {
            this.httpClient = new HttpClient();
        }

        public async Task CheckServerConnection()
        {
            var response = await this.httpClient.GetAsync(UrlBuilder.BuildEndpoint("Test"));

            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundServerException("Brak połączenia z serwerem!");
            }
        }
    }
}
