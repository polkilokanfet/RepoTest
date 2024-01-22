using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Extensions
{
    public static class TaskExtensions
    {
        public static async void Await(this Task task, Action completedCallback = null, Action<Exception> errorCallback = null)
        {
            try
            {
                await task;
                completedCallback?.Invoke();
            }
            catch (Exception e)
            {
                errorCallback?.Invoke(e);
            }
        }
    }
}