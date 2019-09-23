namespace TaskManager.Validation
{
    using System.Net.Mail;
    using TaskManager.BindingModel;
    using TaskManager.Entity.Enum;

    public class RegistrationForm
    {
        public Models.ValidationResult IsValid(
            RegistrationBindingModel accountForm,
            bool isManager,
            bool isDeveloper,
            bool isViewer)
        {
            var result = SetRole(accountForm, isManager, isDeveloper, isViewer);

            if (!result.IsValid)
            {
                return result;
            }

            if (accountForm.UserName == null || accountForm.FirstName == null ||
                accountForm.LastName == null || accountForm.Email == null)
            {
                result.IsValid = false;
                result.Message = "Wypełnij wszystkie pola!";

                return result;
            }
            else if (IsStringHaveSpaces(accountForm.UserName))
            {
                result.IsValid = false;
                result.Message = "Niedozwolone znaki w polu Login!";

                return result;
            }
            else if (!IsEmailValid(accountForm.Email))
            {
                result.IsValid = false;
                result.Message = "Błędny adres Email!";

                return result;
            }
            else if (IsStringHaveSpaces(accountForm.LastName))
            {
                result.IsValid = false;
                result.Message = "Niedozwolone znaki w polu Nazwisko!";

                return result;
            }

            result.Message = "Zarejestrowano pomyślnie!";

            return result;
        }

        private Models.ValidationResult SetRole(RegistrationBindingModel accountForm, bool isManager, bool isDeveloper, bool isViewer)
        {
            var result = new Models.ValidationResult();

            if (!isManager && !isDeveloper && !isViewer)
            {
                result.IsValid = false;
                result.Message = "Wybierz stanowisko!";

                return result;
            }

            accountForm.Role = isManager ?
                 Role.Manager : isDeveloper ?
                 Role.Developer : isViewer ?
                 Role.Viewer : Role.Viewer;

            result.IsValid = true;

            return result;
        }

        private bool IsStringHaveSpaces(string text) => text.Contains(" ");

        private bool IsEmailValid(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
