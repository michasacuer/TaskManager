namespace TaskManager.Contracts.Account
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Taskmanager.BindingModel;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity;

    public class Account
    {
        private HttpClient client;

        public Account()
        {
            this.client = new HttpClient();
        }

        public async Task Register(RegistrationBindingModel newUserAccount)
        {
            var response = await this.client.PostAsJsonAsync(UrlBuilder.BuildEndpoint("Account", "Register"), newUserAccount);

            if (!response.IsSuccessStatusCode)
            {
                throw new RegistrationException("Błąd serwera, sprawdź formularz!");
            }
        }

        public async Task<ApplicationUser> Login(LoginBindingModel loginData)
        {
            var response = await this.client.PostAsJsonAsync(UrlBuilder.BuildEndpoint("Account", "Login"), loginData);

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
