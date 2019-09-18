namespace TaskManager.Contracts.Extensions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity;

    public static class HttpClientAccountExtension
    {
        public static async Task Register(this HttpClient httpClient, RegistrationBindingModel newUserAccount)
        {
            var response = await httpClient.PostAsJsonAsync(UrlBuilder.BuildEndpoint("Account", "Register"), newUserAccount);

            if (!response.IsSuccessStatusCode)
            {
                throw new RegistrationException("Błąd serwera, sprawdź formularz!");
            }
        }

        public static async Task<ApplicationUser> Login(this HttpClient httpClient, LoginBindingModel loginCredentials)
        {
            var response = await httpClient.PostAsJsonAsync(UrlBuilder.BuildEndpoint("Account", "Login"), loginCredentials);

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
