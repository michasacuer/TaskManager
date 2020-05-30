namespace TaskManager.Tests.Infrastructure
{
    using System.Linq;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    
    public static class MoqFixture
    {
        public static IFixture Create()
        {
            var fixture = new Fixture
            {
                RepeatCount = 5
            };

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(x => fixture.Behaviors.Remove(x));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = false });

            return fixture;
        }
    }
}