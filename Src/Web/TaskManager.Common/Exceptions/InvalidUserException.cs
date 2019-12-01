namespace TaskManager.Common.Exceptions
{
    using System;

    public class InvalidUserException : Exception
    {
        public InvalidUserException()
            : base()
        {
        }

        public InvalidUserException(string message)
            : base(message)
        {
        }
    }
}
