namespace TaskManager.Contracts.Extensions
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity.Base;

    public static class HttpClientDataExtension
    {
        public static async Task<List<TObject>> GetAsync<TObject>(this HttpClient httpClient)
            where TObject : BaseEntity<int>
        {
            string controllerName = typeof(TObject).Name;
            var response = await httpClient.GetAsync(UrlBuilder.BuildEndpoint(controllerName));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<TObject>>();
            }
            else
            {
                throw new NotFoundServerException();
            }
        }

        public static async Task<TObject> GetAsync<TObject>(this HttpClient httpClient, int id)
            where TObject : BaseEntity<int>
        {
            string controllerName = typeof(TObject).Name;
            var response = await httpClient.GetAsync(UrlBuilder.BuildEndpoint(controllerName, id));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TObject>();
            }
            else
            {
                throw new NotFoundServerException();
            }
        }

        public static async Task<TObject> GetAsync<TObject>(this HttpClient httpClient, string id)
            where TObject : BaseEntity<int>
        {
            string controllerName = typeof(TObject).Name;
            var response = await httpClient.GetAsync(UrlBuilder.BuildEndpoint(controllerName, id));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TObject>();
            }
            else
            {
                throw new NotFoundServerException();
            }
        }

        public static async Task<TObject> PostAsync<TObject>(this HttpClient httpClient, TObject data)
            where TObject : BaseEntity<int>
        {
            string controlleName = typeof(TObject).Name;
            var response = await httpClient.PostAsJsonAsync(UrlBuilder.BuildEndpoint(controlleName), data);

            return await response.Content.ReadAsAsync<TObject>();
        }

        public static async Task<TObject> PutAsync<TObject>(this HttpClient httpClient, TObject data, int id)
            where TObject : BaseEntity<int>
        {
            string controllerName = typeof(TObject).Name;
            var response = await httpClient.PutAsJsonAsync(UrlBuilder.BuildEndpoint(controllerName, id), data);

            return await response.Content.ReadAsAsync<TObject>();
        }

        public static async Task<TObject> PutAsync<TObject>(this HttpClient httpClient, TObject data, params string[] routes)
            where TObject : BaseEntity<int>
        {
            string controllerName = typeof(TObject).Name;
            var response = await httpClient.PutAsJsonAsync(UrlBuilder.BuildEndpoint(controllerName, routes), data);

            return await response.Content.ReadAsAsync<TObject>();
        }

        public static async Task DeleteAsync<TObject>(this HttpClient httpClient, int id)
            where TObject : BaseEntity<int>
        {
            string controllerName = typeof(TObject).Name;
            var response = await httpClient.DeleteAsync(UrlBuilder.BuildEndpoint(controllerName, id));

            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundServerException();
            }
        }
    }
}
