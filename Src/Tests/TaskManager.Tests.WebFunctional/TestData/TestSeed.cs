using TaskManager.Persistence;
using TaskManager.Tests.Infrastructure;

namespace TaskManager.Tests.WebFunctional.TestData
{
    public class TestSeed : IBaseSeed
    {
        public void Run(ref TaskManagerDbContext context)
        {
            ContextDataSeeding.Run(ref context);
        }
    }
}