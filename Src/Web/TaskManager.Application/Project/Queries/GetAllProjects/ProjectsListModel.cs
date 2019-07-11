namespace TaskManager.Application.Project.Queries.GetAllProjects
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity;

    public class ProjectsListViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}
