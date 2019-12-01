namespace TaskManager.Common.Exceptions
{
    using System;

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base()
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }
    }
}
