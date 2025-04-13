
namespace Bira.App.SchoolManager.Service.Notifications
{
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}