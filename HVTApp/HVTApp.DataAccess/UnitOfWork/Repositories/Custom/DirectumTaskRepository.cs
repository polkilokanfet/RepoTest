using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DirectumTaskRepository
    {
        protected override IQueryable<DirectumTask> GetQuery()
        {
            return Context.Set<DirectumTask>().AsQueryable()
                .Include(directumTask => directumTask.Group.Author)
                .Include(directumTask => directumTask.Performer.Employee);
        }

        public IEnumerable<DirectumTask> GetAllOfCurrentUser()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuery()
                .Where(directumTask => directumTask.Group.Author.Id == GlobalAppProperties.User.Id)
                .ToList();
        }

        public IEnumerable<DirectumTask> GetAllByGroup(Guid groupId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuery()
                .Where(directumTask => directumTask.Group.Id == groupId)
                .ToList();
        }

        public IEnumerable<DirectumTask> GetAllParallelTasks(DirectumTask task)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var tasksOfGroup = GetAllByGroup(task.Group.Id);
            return tasksOfGroup.Where(directumTask => directumTask.PreviousTask == null);
        }

        public IEnumerable<DirectumTask> GetAllSerialTasks(DirectumTask task)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var result = new List<DirectumTask> { task };

            //предыдущие задачи
            var prevTask = task.PreviousTask;
            while (prevTask != null)
            {
                result.Add(prevTask);
                prevTask = prevTask.PreviousTask;
            }

            //следующие задачи
            var nextTask = task;
            do
            {
                nextTask = GetNextTasks(nextTask.Id).SingleOrDefault();
                if (nextTask != null) 
                    result.Add(nextTask);
            } while (nextTask != null);

            result.Sort();

            return result;
        }

        public IEnumerable<DirectumTask> GetNextTasks(Guid taskId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuery()
                .Include(directumTask => directumTask.PreviousTask)
                .Where(directumTask => directumTask.PreviousTask != null)
                .Where(directumTask => directumTask.PreviousTask.Id == taskId)
                .ToList();
        }

        public IEnumerable<DirectumTask> GetChildTasks(Guid taskId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var tasks = this.GetQuery()
                .Include(directumTask => directumTask.PreviousTask)
                .Include(directumTask => directumTask.ParentTask)
                .Where(directumTask => directumTask.ParentTask != null)
                .Where(directumTask => directumTask.ParentTask.Id == taskId)
                .ToList();

            if (tasks.Count > 1)
            {
                tasks.Sort();
            }

            return tasks;
        }
    }
}