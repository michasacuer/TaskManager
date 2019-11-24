namespace TaskManager.WPF.Helpers
{
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.Entity;
    using TaskManager.Validation;
    using TaskManager.Validation.Models;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels;

    public class AddNewProjectHelper
    {
        public async Task<ValidationResult> AddProjectToDatabase(AddNewProjectViewModel vm)
        {
            bool isManager = LoggedUser.Instance.IsManager();
            var model = new AddNewProjectForm 
            { 
                ProjectName = vm.ProjectNameTextBox,
                Description = vm.DescriptionTextBox
            };

            var validationResult = new AddProjectForm().IsValid(model, isManager);

            if (validationResult.IsValid)
            {
                var newProject = new Project
                {
                    Name = model.ProjectName,
                    Description = model.Description
                };

                var projectContract = new ProjectContract(LoggedUser.Instance.User.Bearer);
                await projectContract.AddAsync(newProject);
                validationResult.Message = "Projekt dodano pomyślnie!";
            }

            return validationResult;
        }
    }
}
