namespace TaskManager.Contracts.Account
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Entity;

    public class Account
    {
        private HttpClient httpClient;

        public Account()
        {
            this.httpClient = new HttpClient();
        }

        public async Task Register(RegistrationBindingModel newUserAccount) 
            => await this.httpClient.Register(newUserAccount);

        public async Task<ApplicationUser> Login(LoginBindingModel loginCredentials) 
            => await this.httpClient.Login(loginCredentials);
    }
}
