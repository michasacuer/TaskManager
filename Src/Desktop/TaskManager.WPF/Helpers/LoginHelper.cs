namespace TaskManager.WPF.Helpers
{
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Account;
    using TaskManager.Validation;
    using TaskManager.Validation.Models;
    using TaskManager.WPF.Models;

    public class LoginHelper
    {
        public async Task<ValidationResult> ExternlLoginAsync(string login, string password)
        {
            var loginForm = new LoginBindingModel
            {
                UserName = login,
                Password = password
            };

            var validationResult = new LoginForm().IsValid(loginForm);

            if (validationResult.IsValid)
            {
                var accountContract = new AccountContract();
                var user = await accountContract.LoginAsync(loginForm);
                
                LoggedUser.Instance.LoginUserToApp(user);

                await LoggedUser.Instance.GetUserTask().IsUserHaveActiveTask(user.Id);
            }

            return validationResult;
        }
    }
}
