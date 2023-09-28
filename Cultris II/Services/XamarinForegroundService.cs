using Cultris_II.Interfaces;
using Cultris_II.Models;
using System;
using Xamarin.Forms;

namespace Cultris_II.Services
{
    public class XamarinForegroundService
    {
        private static readonly IXamarinForegroundService _service = DependencyService.Get<IXamarinForegroundService>(DependencyFetchTarget.GlobalInstance);

        readonly XamarinNotification notification;
        public readonly PeriodicWork work;
        public XamarinForegroundService(string uniqueName, TimeSpan timeout, Action callback)
        {
            work = new PeriodicWork(uniqueName, timeout, callback);
            notification = new XamarinNotification(uniqueName, string.Empty);
        }

        public void Start() => _service.StartService(work);
        public void Stop() => _service.StopService(work);

        public void DoNotification(string message)
        {
            notification.Message = message;
            _service.NotificationShow(notification);
        }

        public void ClearNotification()
        {
            _service.NotificationClear(notification.Id);
        }
    }
}
