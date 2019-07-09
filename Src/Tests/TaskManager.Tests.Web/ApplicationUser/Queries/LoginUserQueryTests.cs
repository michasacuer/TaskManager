namespace TaskManager.Tests.Web
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;
    using TaskManager.Application.Queries;
    using TaskManager.Tests.Infrastructure;

    public class LoginUserQueryTests
    {
        private ServiceCollection Services { get; set; }

        public LoginUserQueryTests()
        {
            this.Services = ServicesFactory.Create();
        }
    }
}
