namespace TaskManager.Contracts.Exceptions
{
    using System;

    public class NotFoundServerException : Exception
    {
        public NotFoundServerException()
        {
        }

        public NotFoundServerException(string message)
            : base(message)
        {
        }
    }
}
