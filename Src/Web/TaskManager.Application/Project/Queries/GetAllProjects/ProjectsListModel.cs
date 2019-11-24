namespace TaskManager.Application.Project.Queries.GetAllProjects
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity;

    public class ProjectsListModel
    {
        public IEnumerable<Project> List { get; set; }
    }
}
