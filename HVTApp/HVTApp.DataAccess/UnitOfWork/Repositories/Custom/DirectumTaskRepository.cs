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
                .Include(offer => offer.Group.Author);
        }

        public IEnumerable<DirectumTask> GetAllOfCurrentUser()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Where(directumTask => directumTask.Group.Author.Id == GlobalAppProperties.User.Id)
                .ToList();
        }
    }
}