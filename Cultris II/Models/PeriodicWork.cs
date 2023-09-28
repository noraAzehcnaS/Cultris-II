using System;
using System.Threading.Tasks;

namespace Cultris_II.Models
{
    public class PeriodicWork
    {
        public readonly string Name;
        public bool Running { get; private set; }
        readonly TimeSpan _timeout;
        readonly Action _callback;
        public PeriodicWork(string name, TimeSpan timeout, Action callback)
        {
            Name = name;
            _timeout = timeout;
            _callback = callback;
            Running = false;
        }
        public void RunAsync()
        {
            Running = true;
            Task.Run(() => _callback?.Invoke())
                .ContinueWith(async task =>
                {
                    await Task.Delay(_timeout);
                    Running = false;
                });
        }
    }
}
