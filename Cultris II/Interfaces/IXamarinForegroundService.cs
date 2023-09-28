using Cultris_II.Models;

namespace Cultris_II.Interfaces
{
   public interface IXamarinForegroundService
    {
        void StartService(PeriodicWork work);
        void StopService(PeriodicWork work);
        void NotificationShow(XamarinNotification notification);
        void NotificationClear(int id);
        void ForceStop();
    }
}
