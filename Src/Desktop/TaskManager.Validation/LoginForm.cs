namespace TaskManager.Validation
{
    using TaskManager.BindingModel;
    using TaskManager.Validation.Models;

    public class LoginForm
    {
        public ValidationResult IsValid(LoginBindingModel loginForm)
        {
            var result = new ValidationResult();
            result.IsValid = true;

            result.Message = "Zalogowano pomyślnie!";

            if (loginForm.UserName == null || loginForm.Password == null)
            {
                result.IsValid = false;
                result.Message = "Wypełnij wszystkie pola!";
            }
            else if (loginForm.UserName.Contains(" ") || loginForm.Password.Contains(" "))
            {
                result.IsValid = false;
                result.Message = "Niedozwolone znaki w polu Nazwisko!";
            }

            return result;
        }
    }
}
