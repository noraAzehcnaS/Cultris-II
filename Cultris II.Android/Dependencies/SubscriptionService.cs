using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Annotations;
using AndroidX.Core.App;
using Cultris_II.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly:Dependency(typeof(Cultris_II.Droid.Dependencies.SubscriptionService))]
namespace Cultris_II.Droid.Dependencies
{
    [Service]
    public class SubscriptionService : Service, ISubscriptionsService
    {
        private readonly List<int> idsTriggered = new List<int>();
        private bool isStarted = false;
        private readonly string _notificationChannelId = "C2Service";
        private readonly string _notificationId = "SubscriptionService";
        private readonly int _serviceId = 1001;
        private readonly TimeSpan _timeout = new TimeSpan(0,0,45);
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            isStarted = true;
            MessagingCenter.Subscribe<Services.SubscriptionService, SubNotification>(this, "SubNotification", (_, n) => DoNotify(n));
            StartForeground(_serviceId, NotificationBuilder().Build());
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            isStarted = false;
            MessagingCenter.Unsubscribe<Services.SubscriptionService, SubNotification>(this,"SubNotification");
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (isStarted) { base.OnStartCommand(intent, flags, startId); }

            Task.Run(() =>
            {
                while (true)
                {
                    MessagingCenter.Send<ISubscriptionsService>(this, "SubscriptionService");
                    Thread.Sleep(_timeout);
                }
            });
            StartForeground(_serviceId, NotificationBuilder().Build());
            return base.OnStartCommand(intent, flags, startId);
        }

        public void StartService()
        {
            if (isStarted) { return; }
            var intent = new Intent(Android.App.Application.Context,typeof(SubscriptionService));
            Android.App.Application.Context.StartForegroundService(intent);
            isStarted = true;
        }

        public void StopService()
        {
            if (!isStarted) { return; }
            var intent = new Intent(Android.App.Application.Context, typeof(SubscriptionService));
            Android.App.Application.Context.StopService(intent);
        }

        private NotificationCompat.Builder NotificationBuilder()
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var notificationChannel = new NotificationChannel(
                _notificationChannelId, 
                _notificationChannelId, 
                NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(notificationChannel);

            return new NotificationCompat.Builder(this, _notificationChannelId)
                .SetContentTitle(_notificationId)
                .SetSmallIcon(Resource.Mipmap.info_icon)
                .SetContentText(_notificationId)
                .SetPriority(1)
                .SetOngoing(true)
                .SetChannelId(_notificationChannelId)
                .SetAutoCancel(true);
        }

        private void DoNotify(SubNotification subNotification)
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var notificationChannel = new NotificationChannel(
                "C2 Notifications",
                "C2 Notifications",
                NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(notificationChannel);
            if(string.IsNullOrEmpty(subNotification.Message) && subNotification.Id != _serviceId)
            {
                notificationManager.Cancel(subNotification.Id);
                idsTriggered.Remove(subNotification.Id);
                return;
            }
            var notification = new NotificationCompat.Builder(this, "C2 Notifications")
                .SetContentTitle(subNotification.Title)
                .SetSmallIcon(Resource.Drawable.players_circle_icon)
                .SetPriority(1)
                .SetContentText(subNotification.Message)
                .SetOngoing(true)
                .SetSilent(true)
                .SetChannelId("C2 Notifications")
                .SetAutoCancel(true);

            if (!idsTriggered.Contains(subNotification.Id))
            {
                notification.SetSilent(false);
                idsTriggered.Add(subNotification.Id);
            }
            notificationManager.Notify(subNotification.Id,notification.Build());
        }
    }
}