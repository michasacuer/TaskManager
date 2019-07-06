namespace TaskManager.Tests.Infrastructure
{
    using Xunit;
    using TaskManager.Persistence;

    public class DatabaseFixture
    {
        public TaskManagerDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            Context = DatabaseContextFactory.Create();
        }
    }

    [CollectionDefinition("DatabaseTestCollection")]
    public class QueryCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}
