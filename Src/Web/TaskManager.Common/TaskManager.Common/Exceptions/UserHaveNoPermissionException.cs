namespace TaskManager.Common.Exceptions
{
    using System;

    public class UserHaveNoPermissionException : Exception
    {
        public UserHaveNoPermissionException() 
            : base()
        {
        }

        public UserHaveNoPermissionException(string message)
            : base(message)
        {
        }
    }
}
