namespace TaskManager.Application.Queries
{
    using MediatR;

    public class LoginQuery : IRequest<LoginModel>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
