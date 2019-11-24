namespace TaskManager.Validation
{
    using TaskManager.Validation.Models;
    
    public class AddProjectForm
    {
        public ValidationResult IsValid(AddNewProjectForm model, bool isManager)
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
