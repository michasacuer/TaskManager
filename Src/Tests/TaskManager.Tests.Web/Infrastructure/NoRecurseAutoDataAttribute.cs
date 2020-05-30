namespace TaskManager.Tests.Infrastructure
{
    using AutoFixture.Xunit2;
    
    public sealed class NoRecurseAutoDataAttribute : AutoDataAttribute
    {
        public NoRecurseAutoDataAttribute()
            : base(MoqFixture.Create)
        {
        }
    }
}