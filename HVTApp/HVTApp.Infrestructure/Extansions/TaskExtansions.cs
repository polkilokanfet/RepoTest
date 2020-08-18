using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Extansions
{
    public static class TaskExtansions
    {
        public static async void Await(this Task task, Action complitedCallback = null, Action<Exception> errorCallback = null)
        {
            try
            {
                await task;
                complitedCallback?.Invoke();
            }
            catch (Exception e)
            {
                errorCallback?.Invoke(e);
            }
        }
    }
}