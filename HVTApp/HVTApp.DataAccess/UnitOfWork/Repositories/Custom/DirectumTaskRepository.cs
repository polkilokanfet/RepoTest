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
        protected override IQueryable<DirectumTask> GetQuary()
        {
            return Context.Set<DirectumTask>().AsQueryable()
                .Include(directumTask => directumTask.Group.Author)
                .Include(directumTask => directumTask.Performer.Employee);
        }

        public IEnumerable<DirectumTask> GetAllOfCurrentUser()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Where(directumTask => directumTask.Group.Author.Id == GlobalAppProperties.User.Id)
                .ToList();
        }

        public IEnumerable<DirectumTask> GetAllByGroup(Guid groupId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Where(directumTask => directumTask.Group.Id == groupId)
                .ToList();
        }

        public IEnumerable<DirectumTask> GetNextTasks(Guid taskId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Include(directumTask => directumTask.PreviousTask)
                .Where(directumTask => directumTask.PreviousTask != null)
                .Where(directumTask => directumTask.PreviousTask.Id == taskId)
                .ToList();
        }

        public IEnumerable<DirectumTask> GetChildTasks(Guid taskId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Include(directumTask => directumTask.ParentTask)
                .Where(directumTask => directumTask.ParentTask != null)
                .Where(directumTask => directumTask.ParentTask.Id == taskId)
                .ToList();
        }
    }
}