using Bira.App.SchoolManager.Service.Notifications;

namespace Bira.App.SchoolManager.Service.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}