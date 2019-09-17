namespace TaskManager.Contracts.Exceptions
{
    using System;

    public class RegistrationException : Exception
    {
        public RegistrationException()
        {
        }

        public RegistrationException(string message)
            : base(message)
        {
        }
    }
}
