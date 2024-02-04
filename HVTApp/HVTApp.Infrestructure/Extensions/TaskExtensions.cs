using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Для работы асинхронных таск в синхронных задачах
        /// </summary>
        /// <param name="task">Асинхронная задача</param>
        /// <param name="completedCallback">Действие при удачном завершении асинхронной задачи</param>
        /// <param name="errorCallback">Действие при возникновении ошибки</param>
        /// <param name="lastAction">Действие, которое идёт самым последним</param>
        public static async void Await(this Task task, Action completedCallback = null, Action<Exception> errorCallback = null, Action lastAction = null)
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

            lastAction?.Invoke();
        }
    }
}