using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// ��� ������ ����������� ���� � ���������� �������
        /// </summary>
        /// <param name="task">����������� ������</param>
        /// <param name="completedCallback">�������� ��� ������� ���������� ����������� ������</param>
        /// <param name="errorCallback">�������� ��� ������������� ������</param>
        /// <param name="lastAction">��������, ������� ��� ����� ���������</param>
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