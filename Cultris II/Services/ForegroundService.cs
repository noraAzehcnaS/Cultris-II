using System;
using Xamarin.Forms;

namespace Cultris_II.Services
{
    public interface IForegroundService
    {
        void StartService(int id, string name, TimeSpan timeSpan);
        void StopService(int id);
    }
    public static class ForegroundService
    {
        private static readonly IForegroundService _service = DependencyService.Get<IForegroundService>();
        public static void StartService(int id, string name, TimeSpan timeSpan) => _service.StartService(id,name,timeSpan);
        public static void StopService(int id, string name, TimeSpan timeSpan) => _service.StartService(id, name,timeSpan);
    }
}
