using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DirectumTaskRepositoryTest
    {
        public IEnumerable<DirectumTask> GetAllOfCurrentUser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectumTask> GetAllByGroup(Guid groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectumTask> GetAllParallelTasks(DirectumTask task)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectumTask> GetAllSerialTasks(DirectumTask task)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectumTask> GetNextTasks(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectumTask> GetChildTasks(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}