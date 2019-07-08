namespace TaskManager.Application.Exceptions
{
    using System;

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
            : base()
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}
