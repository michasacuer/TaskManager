namespace TaskManager.Validation
{
    using TaskManager.BindingModel;
    using TaskManager.Validation.Models;
    
    public class AddProjectForm
    {
        public ValidationResult IsValid(AddNewProjectBindingModel model, bool isManager)
        {
            var result = new ValidationResult();

            if (!isManager)
            {
                result.Message = "Brak uprawnień! Zgłoś się do administratora!";
                result.IsValid = false;

                return result;
            }

            if (model.ProjectName == null)
            {
                result.Message = "Wypełnij wszystkie pola";
                result.IsValid = false;

                return result;
            }

            result.IsValid = true;

            return result;
        }
    }
}
