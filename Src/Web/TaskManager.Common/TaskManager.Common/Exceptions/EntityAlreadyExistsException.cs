namespace TaskManager.Common.Exceptions
{
    using System;

    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException()
            : base()
        {
        }

        public EntityAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
