namespace TaskManager.Contracts.Extensions
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using TaskManager.BindingModel.Commands;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity.Base;
    using TaskManager.Entity.JSONMapper;

    public static class HttpClientDataExtension
    {
        public static void Authorize(this HttpClient httpClient, string bearer) 
            => httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

        public static async Task<List<TObject>> GetAsync<TResponse, TObject>(this HttpClient httpClient)
            where TObject : BaseEntity<int>
            where TResponse : JSONResponse<TObject>
        {
            string controllerName = ControllerNameValidator(typeof(TObject).Name);
            var response = await httpClient.GetAsync(UrlBuilder.BuildEndpoint(controllerName));

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<TResponse>();
                return data.List;
            }
            else
            {
                throw new NotFoundServerException();
            }
        }

        public static async Task<TObject> GetAsync<TObject>(this HttpClient httpClient, int id)
            where TObject : BaseEntity<int>
        {
            string controllerName = ControllerNameValidator(typeof(TObject).Name);
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
            string controllerName = ControllerNameValidator(typeof(TObject).Name);
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
            string controlleName = ControllerNameValidator(typeof(TObject).Name);
            var response = await httpClient.PostAsJsonAsync(UrlBuilder.BuildEndpoint(controlleName), data);
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundServerException();
            }

            return await response.Content.ReadAsAsync<TObject>();
        }

        public static async Task<TObject> PostAsync<TObject>(this HttpClient httpClient, TObject data, Command command, params string[] routes)
            where TObject : BaseEntity<int>
        {
            string controlleName = ControllerNameValidator(typeof(TObject).Name);
            var response = await httpClient.PostAsJsonAsync(UrlBuilder.BuildEndpoint(controlleName, routes), command);
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundServerException();
            }

            return await response.Content.ReadAsAsync<TObject>();
        }

        public static async Task<TObject> PutAsync<TObject>(this HttpClient httpClient, TObject data, int id)
            where TObject : BaseEntity<int>
        {
            string controllerName = ControllerNameValidator(typeof(TObject).Name);
            var response = await httpClient.PutAsJsonAsync(UrlBuilder.BuildEndpoint(controllerName, id), data);
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundServerException();
            }

            return await response.Content.ReadAsAsync<TObject>();
        }

        public static async Task<TObject> PutAsync<TObject>(this HttpClient httpClient, TObject data, params string[] routes)
            where TObject : BaseEntity<int>
        {
            string controllerName = ControllerNameValidator(typeof(TObject).Name);
            var response = await httpClient.PutAsJsonAsync(UrlBuilder.BuildEndpoint(controllerName, routes), data);
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundServerException();
            }

            return await response.Content.ReadAsAsync<TObject>();
        }

        public static async Task DeleteAsync<TObject>(this HttpClient httpClient, int id)
            where TObject : BaseEntity<int>
        {
            string controllerName = ControllerNameValidator(typeof(TObject).Name);
            var response = await httpClient.DeleteAsync(UrlBuilder.BuildEndpoint(controllerName, id));

            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundServerException();
            }
        }

        public static async Task<string> GetPdfAsync(this HttpClient httpClient, int projectId)
        {
            var response = await httpClient.GetAsync(UrlBuilder.BuildEndpoint("Raport", projectId));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new NotFoundServerException();
            }
        }

        private static string ControllerNameValidator(string controllerName) => controllerName == "ToDoTask" ? "Task" : controllerName;
    }
}
