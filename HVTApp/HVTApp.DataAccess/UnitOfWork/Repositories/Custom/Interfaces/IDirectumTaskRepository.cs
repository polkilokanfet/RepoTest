using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial interface IDirectumTaskRepository
    {
        /// <summary>
        /// Получить все задачи текущего пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<DirectumTask> GetAllOfCurrentUser();
    }
}