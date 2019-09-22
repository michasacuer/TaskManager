namespace TaskManager.Contracts.Extensions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity;

    public static class HttpClientAccountExtension
    {
        public static async Task RegisterAsync(this HttpClient httpClient, string baseUrl, RegistrationBindingModel newUserAccount)
        {
            var response = await httpClient.PostAsJsonAsync(UrlBuilder.BuildEndpoint(baseUrl, "Account", "Register"), newUserAccount);

            if (!response.IsSuccessStatusCode)
            {
                throw new RegistrationException("Błąd serwera, sprawdź formularz!");
            }
        }

        public static async Task<ApplicationUser> LoginAsync(this HttpClient httpClient, string baseUrl, LoginBindingModel loginCredentials)
        {
            var response = await httpClient.PostAsJsonAsync(UrlBuilder.BuildEndpoint(baseUrl, "Account", "Login"), loginCredentials);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ApplicationUser>();
            }
            else
            {
                throw new LoginException("Błędne dane logowania!");
            }
        }
    }
}
