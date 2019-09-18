namespace TaskManager.Contracts.Extensions
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity;

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
    }
}
