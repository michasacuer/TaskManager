namespace TaskManager.Entity
{
    using TaskManager.Entity.Base;
    using TaskManager.Entity.Enum;

    public class ApplicationUser : BaseEntity<string>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public string Bearer { get; set; }
    }
}
