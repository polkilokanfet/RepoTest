using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.Directum
{
    public static class DirectumExt
    {
        #region Inject

        /// <summary>
        /// �������� � ������ ��� ����������� ������
        /// </summary>
        /// <param name="directumTask"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="injectPreviousTask">������������� ��������� ���������� �����</param>
        /// <returns></returns>
        public static DirectumTaskWrapper InjectTasks(this DirectumTaskWrapper directumTask, IUnitOfWork unitOfWork, bool injectPreviousTask = true)
        {
            //�������� ���������� ������
            if (injectPreviousTask)
            {
                directumTask.PreviousTask?.InjectTasks(unitOfWork);
            }

            ////�������� ����������� ������
            //var nextTasks = ((IDirectumTaskRepository)unitOfWork.Repository<Model.POCOs.DirectumTask>()).GetNextTasks(directumTask.Id);
            //foreach (var nextTask in nextTasks)
            //{
            //    directumTask.NextTasks.Add(new DirectumTaskWrapper(nextTask).InjectTasks(unitOfWork, false));
            //}

            //�������� �������� ������
            return directumTask.InjectChildTasks(unitOfWork);
        }

        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        /// <param name="directumTask"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static DirectumTaskWrapper InjectChildTasks(this DirectumTaskWrapper directumTask, IUnitOfWork unitOfWork)
        {
            //�������� �������� ������
            var childTasks = ((IDirectumTaskRepository) unitOfWork.Repository<Model.POCOs.DirectumTask>())
                .GetChildTasks(directumTask.Id)
                .OrderByDescending(task => task)
                .Select(task => new DirectumTaskWrapper(task).InjectTasks(unitOfWork));
            directumTask.ChildTasks.AddRange(childTasks);

            directumTask.ChildTasks.ForEach(directumTaskWrapper => directumTaskWrapper.ShowPreviousTask = false);
            directumTask.ChildTasks.ForEach(directumTaskWrapper => directumTaskWrapper.ShowNextTask = false);

            return directumTask;
        }

        /// <summary>
        /// ��������� ������������ �����
        /// </summary>
        /// <param name="directumTask"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static DirectumTaskWrapper InjectParallelTasks(this DirectumTaskWrapper directumTask, IUnitOfWork unitOfWork)
        {
            if (directumTask.PreviousTask != null)
                return directumTask;

            var allParallelTasks = ((IDirectumTaskRepository)unitOfWork.Repository<Model.POCOs.DirectumTask>()).GetAllParallelTasks(directumTask.Model).ToList();
            allParallelTasks.Sort();
            var parallelTasks = allParallelTasks
                .Where(task => !Equals(task.Id, directumTask.Id))
                .Select(model => new DirectumTaskWrapper(model))
                .ToList();

            //���� ���� ���������� ������ � ������������, ������ ��� ������ �� ������������
            if (parallelTasks.Any(directumTaskWrapper => directumTaskWrapper.PreviousTask != null))
                return directumTask;

            if (parallelTasks.Any(directumTaskWrapper => directumTaskWrapper.Model.Parallel.Any()))
                return directumTask;

            directumTask.ParallelTasks.AddRange(parallelTasks);
            directumTask.Model.Parallel.AddRange(parallelTasks.Select(directumTaskWrapper => directumTaskWrapper.Model));
            parallelTasks.ForEach(directumTaskWrapper => InjectTasks(directumTaskWrapper, unitOfWork));

            return directumTask;
        }

        #endregion
    }
}