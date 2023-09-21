using Android.Content;
using Cultris_II.Services;
using System;
using Xamarin.Forms;
using ForegroundService = Cultris_II.Droid.Dependencies.ForegroundService;

[assembly: Dependency(typeof(ForegroundService))]
namespace Cultris_II.Droid.Dependencies
{
    public class ForegroundService : IForegroundService
    {
        private static readonly Context context = Android.App.Application.Context;

        public void StartService(int i, string name, TimeSpan timeSpan)
        {
            var intent = new Intent(context, typeof(DataSource));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                context.StartForegroundService(intent);
            }
            else
            {
                context.StartService(intent);
            }
        }

        public void StopService(int i)
        {
            var intent = new Intent(context, typeof(DataSource));
            context.StopService(intent);
        }
    }
}