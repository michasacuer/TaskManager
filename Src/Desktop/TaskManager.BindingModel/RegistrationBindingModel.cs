namespace TaskManager.BindingModel
{
    using TaskManager.Entity.Enum;

    public class RegistrationBindingModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role? Role { get; set; }
    }
}
