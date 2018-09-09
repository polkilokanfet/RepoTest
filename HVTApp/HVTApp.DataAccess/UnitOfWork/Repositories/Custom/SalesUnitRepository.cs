using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitRepository
    {
        public override async Task<List<SalesUnit>> GetAllAsync()
        {
            var units = await base.GetAllAsync();
            units.DesignateProducts(_container.Resolve<IProductDesignationService>());
            return units;
        }

        public override async Task<List<SalesUnit>> GetAllAsNoTrackingAsync()
        {
            var units = await base.GetAllAsNoTrackingAsync();
            units.DesignateProducts(_container.Resolve<IProductDesignationService>());
            return units;
        }

        public override List<SalesUnit> Find(Func<SalesUnit, bool> predicate)
        {
            var units = Context.Set<SalesUnit>().Include(nameof(SalesUnit.Product)).Where(predicate).ToList();
            units.DesignateProducts(_container.Resolve<IProductDesignationService>());
            return units;
        }
    }
}