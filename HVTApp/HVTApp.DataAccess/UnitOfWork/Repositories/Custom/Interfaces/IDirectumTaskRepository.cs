using System;
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

        /// <summary>
        /// Получить все задачи группы
        /// </summary>
        /// <returns></returns>
        IEnumerable<DirectumTask> GetAllByGroup(Guid groupId);


        /// <summary>
        /// Получить все задачи после этой
        /// </summary>
        /// <returns></returns>
        IEnumerable<DirectumTask> GetNextTasks(Guid taskId);

        /// <summary>
        /// Получить все дочерние задачи
        /// </summary>
        /// <returns></returns>
        IEnumerable<DirectumTask> GetChildTasks(Guid taskId);

    }
}