namespace TaskManager.Contracts.Exceptions
{
    using System;

    public class LoginException : Exception
    {
        public LoginException()
        {
        }

        public LoginException(string message)
            : base(message)
        {
        }
    }
}
