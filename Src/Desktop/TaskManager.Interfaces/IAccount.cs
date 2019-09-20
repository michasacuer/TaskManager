namespace TaskManager.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Entity;

    public interface IAccount
    {
        Task RegisterAsync(RegistrationBindingModel newUserAccount);

        Task<ApplicationUser> LoginAsync(LoginBindingModel loginCredentials);
    }
}
