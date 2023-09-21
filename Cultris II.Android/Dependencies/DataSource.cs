using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cultris_II.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace Cultris_II.Droid.Dependencies
{
    [Service]
    public class DataSource : Service
    {

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public const int ServiceRunningNotifID = 9000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Notification notif = DependencyService.Get<INotification>().ReturnNotif();
            StartForeground(ServiceRunningNotifID, notif);
            Task.Run(() => 
            { 
                while(true) 
                {
                    Task.Delay(1000);
                    DataService.AddSubscription(Subscription.PROFESSIONAL_BATTLEFIELD);
                    Task.Delay(1000);
                    DataService.AddSubscription(Subscription.VETERAN_LOUNGE);
                    Task.Delay(1000);
                    DataService.AddSubscription(Subscription.BEGINNER_PARTY);
                }
            });
            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }
    }
}