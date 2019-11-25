namespace TaskManager.WPF.Helpers
{
    using TaskManager.Contracts.Data;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity;
    using TaskManager.Entity.Base;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class InfoBoxHelper
    {
        public void EditTask(ToDoTask task) => this.EditItemInDatabase(task, task.Id);

        public void EditProject(Project project) => this.EditItemInDatabase(project, project.Id);

        private async void EditItemInDatabase<TObject>(TObject item, int itemId)
            where TObject : BaseEntity<int>
        {
            try
            {
                if (item is Project)
                {
                    var projectContract = new ProjectContract(LoggedUser.Instance.User.Bearer);


                }
                //var httpDataService = new HttpDataService();
                //await httpDataService.Put(item, itemId);
            }
            catch (NotFoundServerException exception)
            {
                ApplicationWindows.ShowErrorBox(exception.Message);
            }
        }
    }
}
