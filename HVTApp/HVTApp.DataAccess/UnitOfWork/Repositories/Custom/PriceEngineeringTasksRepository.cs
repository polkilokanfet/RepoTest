using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTasksRepository
    {

        public PriceEngineeringTasks GetForPriceEngineering(Guid id)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return this.GetQuery()
                .Include(priceEngineeringTasks => priceEngineeringTasks.ChildPriceEngineeringTasks.Select(x => x.SalesUnits))
                .Include(priceEngineeringTasks => priceEngineeringTasks.UserManager)
                .SingleOrDefault(priceEngineeringTasks => priceEngineeringTasks.Id == id);
        }

        protected override IQueryable<PriceEngineeringTasks> GetQuery()
        {
            return Context.Set<PriceEngineeringTasks>().AsQueryable()
                .Include(x => x.ChildPriceEngineeringTasks)
                .Include(x => x.UserManager);
        }
    }
}