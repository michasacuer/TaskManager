namespace TaskManager.Tests.WebFunctional.TestData
{
    using TaskManager.Persistence;
    
    public interface IBaseSeed
    {
        void Run(ref TaskManagerDbContext context);
    }
}