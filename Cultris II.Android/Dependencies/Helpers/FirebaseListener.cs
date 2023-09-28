
using Android.Gms.Tasks;
using System.Threading.Tasks;

namespace Cultris_II.Droid.Dependencies
{
    public class FirebaseListener<TResult> : Java.Lang.Object, IOnCompleteListener
        where TResult : class
    {
        readonly TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
        public Task<TResult> Task => taskCompletionSource.Task;
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if(task.IsSuccessful)
            {
                taskCompletionSource.SetResult(task.Result as TResult);
            }
        }
    }
}

