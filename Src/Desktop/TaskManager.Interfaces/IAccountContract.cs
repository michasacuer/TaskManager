namespace TaskManager.Contracts.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Entity;

    public interface IAccountContract
    {
        Task RegisterAsync(RegistrationBindingModel newUserAccount);

        Task<ApplicationUser> LoginAsync(LoginBindingModel loginCredentials);
    }
}
