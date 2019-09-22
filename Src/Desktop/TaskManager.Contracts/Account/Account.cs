namespace TaskManager.Contracts.Account
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Entity;
    using TaskManager.Contracts.Interfaces;

    public class Account : IAccount
    {
        private HttpClient httpClient;

        public Account()
        {
            this.httpClient = new HttpClient();
        }

        public async Task RegisterAsync(RegistrationBindingModel newUserAccount) 
            => await this.httpClient.RegisterAsync(newUserAccount);

        public async Task<ApplicationUser> LoginAsync(LoginBindingModel loginCredentials) 
            => await this.httpClient.LoginAsync(loginCredentials);
    }
}
