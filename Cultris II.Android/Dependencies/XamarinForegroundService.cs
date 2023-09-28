using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using Cultris_II.Interfaces;
using Cultris_II.Models;
using Cultris_II.Droid.Dependencies;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(XamarinForegroundService))]
namespace Cultris_II.Droid.Dependencies
{
    [Service]
    public class XamarinForegroundService : Service, IXamarinForegroundService
    {
        private static readonly ConcurrentDictionary<string, PeriodicWork> _workDictionary = new ConcurrentDictionary<string, PeriodicWork>();
        private static readonly ConcurrentQueue<XamarinNotification> _showNotificationQueue = new ConcurrentQueue<XamarinNotification>();
        private static readonly ConcurrentQueue<int> _removeNotificationQueue = new ConcurrentQueue<int>();
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public void StartService(PeriodicWork work)
        {
            if (!running) { StartSelf(); }
            _workDictionary.GetOrAdd(work.Name, work);
        }

        public void StopService(PeriodicWork service)
        {
            _workDictionary.TryRemove(service.Name, out _);
            if (_workDictionary.IsEmpty && running) { StopSelfSafe(); }
        }
        public void ForceStop()
        {
            _workDictionary.Clear();
            StopSelfSafe();
        }
        private void StartSelf()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(XamarinForegroundService));
            Android.App.Application.Context.StartForegroundService(intent);
        }
        public void StopSelfSafe()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(XamarinForegroundService));
            Android.App.Application.Context.StopService(intent);
        }
        private void DoWork()
        {
            foreach (var work in _workDictionary.Values.Where(work => !work.Running))
            {
                work.RunAsync();
            }
        }

        #region Overrides
        private static bool running = false;
        public override void OnCreate()
        {
            base.OnCreate();
            running = true;
            CreateNotificationChannel();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            running = false;
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CancelAll();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            StartForeground(300, NotificationBuilder("XF Service", "Running in background").Build());
            Task.Run(() =>
            {
                while (running) { DoWork(); HandleNotifications(); }
            });
            return base.OnStartCommand(intent, flags, startId);
        }
        #endregion

        #region Notification
        private const string _notificationChannelId = "XF Service Notifications";

        private void CreateNotificationChannel()
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var notificationChannel = new NotificationChannel(
                _notificationChannelId,
                _notificationChannelId,
                NotificationImportance.High);
            notificationManager.CreateNotificationChannel(notificationChannel);
        }
        private NotificationCompat.Builder NotificationBuilder(string title, string message)
        {
            return new NotificationCompat.Builder(this, _notificationChannelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetChannelId(_notificationChannelId)
                .SetSmallIcon(Resource.Mipmap.players_circle_icon)
                .SetSilent(true)
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetOngoing(true)
                .SetAutoCancel(true);
        }

        private void HandleNotifications()
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            while (_showNotificationQueue.TryDequeue(out XamarinNotification n))
            {
                notificationManager.Notify(n.Id, NotificationBuilder(n.Title, n.Message).Build());
            }
            while (_removeNotificationQueue.TryDequeue(out int id))
            {
                notificationManager.Cancel(id);
            }
        }

        public void NotificationShow(XamarinNotification notification)
        {
            _showNotificationQueue.Enqueue(notification);
        }

        public void NotificationClear(int id)
        {
            _removeNotificationQueue.Enqueue(id);
        }
        #endregion
    }
}