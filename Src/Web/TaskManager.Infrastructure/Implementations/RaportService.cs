namespace TaskManager.Infrastructure.Implementations
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Models.ViewModels;

    public class RaportService : IRaportService
    {
        private readonly IProjectRepository projectRepository;

        private readonly IRazorViewEngine razorViewEngine;

        private readonly ITempDataProvider tempDataProvider;

        private readonly IServiceProvider serviceProvider;

        public RaportService(
            IProjectRepository projectRepository,
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            this.projectRepository = projectRepository;
            this.razorViewEngine = razorViewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
        }

        public async Task<string> GenerateProjectRaport(int projectId)
        {
            var project = await this.projectRepository.GetProjectWithTasksAsync(projectId);

            var httpContext = new DefaultHttpContext { RequestServices = this.serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var stringWriter = new StringWriter())
            {
                string viewPath = "~/wwwroot/RaportTemplate.cshtml";

                var viewResult = this.razorViewEngine.GetView(viewPath, viewPath, false);

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = new RaportViewModel
                    {
                        Project = project,
                        ProjectTasks = project.Tasks.ToList(),
                        // ProjectEndedTasks = project.EndedTasks.ToList(),
                    }
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, this.tempDataProvider),
                    stringWriter,
                    new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }
    }
}
