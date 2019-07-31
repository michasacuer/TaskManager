namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;

    public interface INotificationService
    {
        Task SendMessageToAll(string message);
    }
}
