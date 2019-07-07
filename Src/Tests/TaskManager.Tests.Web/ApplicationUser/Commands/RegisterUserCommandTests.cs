namespace TaskManager.Tests.Web
{
    using Xunit;
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class RegisterUserCommandTests
    {
        public RegisterUserCommandTests(DatabaseFixture fixture)
        {
        }
    }
}
