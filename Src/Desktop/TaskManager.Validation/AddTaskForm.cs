namespace TaskManager.Validation
{
    using System;
    using TaskManager.Entity.Enum;
    using TaskManager.Validation.Models;
    
    public class AddTaskForm
    {
        public ValidationResult IsValid(AddNewTaskForm model, bool isManager)
        {
            var result = new ValidationResult();

            if (!isManager)
            {
                result.Message = "Brak uprawnień! Zgłoś się do administratora!";
                result.IsValid = false;

                return result;
            }

            if (model.SelectedProject == null || model.TaskName == null)
            {
                result.Message = "Wypełnij wszystkie pola";
                result.IsValid = false;

                return result;
            }

            try
            {
                model.Priority = SetPriority(model.LowPriorityButton, model.MediumPriorityButton, model.HighPriorityButton);
            }
            catch (ArgumentNullException)
            {
                result.Message = "Wybierz Priorytet!";
                result.IsValid = false;

                return result;
            }

            result.IsValid = true;

            return result;
        }

        private static Priority SetPriority(bool isLow, bool isMedium, bool isHigh)
        {
            if (isLow)
            {
                return Priority.Low;
            }

            if (isMedium)
            {
                return Priority.Medium;
            }

            if (isHigh)
            {
                return Priority.High;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
