namespace TaskManager.WPF.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.Entity;
    using TaskManager.Validation;
    using TaskManager.Validation.Models;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels;

    public class AddNewTaskHelper
    {
        private ProjectContract projectContract;

        private TaskContract taskContract;

        public AddNewTaskHelper()
        {
            string bearer = LoggedUser.Instance.User.Bearer;

            this.projectContract = new ProjectContract(bearer);
            this.taskContract = new TaskContract(bearer);
        }

        public async Task<ValidationResult> AddTaskToDatabase(AddNewTaskViewModel vm)
        {
            bool isManager = LoggedUser.Instance.IsManager();
            var model = new AddNewTaskForm
            {
                TaskName = vm.TaskNameTextBox,
                SelectedProject = vm.SelectedProject,
                Description = vm.DescriptionTextBox,
                Priority = vm.Priority,
                LowPriorityButton = vm.LowPriorityButton,
                MediumPriorityButton = vm.MediumPriorityButton,
                HighPriorityButton = vm.HighPriorityButton
            };

            var validationResult = new AddTaskForm().IsValid(model, isManager);
            if (validationResult.IsValid)
            {
                int? storyPoints = null;
        
                try
                {
                    if (vm.TaskNameTextBox.Contains(","))
                    {
                        var substring = vm.TaskNameTextBox.Substring(vm.TaskNameTextBox.IndexOf(",") + 1);
                        storyPoints = Convert.ToInt32(substring.Replace(" ", string.Empty));
                        vm.TaskNameTextBox = vm.TaskNameTextBox.Substring(0, vm.TaskNameTextBox.IndexOf(","));
                    }
                }
                catch (FormatException)
                {
                    validationResult.Message = "SP podajemy po przecinku jako liczba!";
                    validationResult.IsValid = false;
        
                    return validationResult;
                }

                var newTask = new ToDoTask
                {
                    Name = model.TaskName,
                    Description = model.Description,
                    Priority = model.Priority,
                    ProjectId = model.SelectedProject.Id,
                    StoryPoints = storyPoints
                };

                await this.taskContract.AddAsync(newTask);
                validationResult.Message = "Task dodano pomyślnie!";
            }
        
            return validationResult;
        }

        public async Task<List<Project>> GetAllProjectsFromDatabase() => await this.projectContract.GetAllAsync();
    }
}
