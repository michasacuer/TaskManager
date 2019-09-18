namespace TaskManager.Contracts.Extensions
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class HttpClientDataExtension
    {
        public static async Task<List<TObject>> Get<TObject>(this HttpClient httpClient)
        {
            string controllerName = typeof(TObject).Name;

            var response = await httpClient.GetAsync(UrlBuilder.BuildEndpoint(controllerName));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<TObject>>();
            }
            else
            {
                return default;
            }
        }

        public static async Task<TObject> Get<TObject>(this HttpClient httpClient, int id)
        {
            string controllerName = typeof(TObject).Name;

            var response = await httpClient.GetAsync(UrlBuilder.BuildEndpoint(controllerName, id));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TObject>();
            }
            else
            {
                return default;
            }
        }
    }
}
