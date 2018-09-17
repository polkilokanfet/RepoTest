using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class OfferUnitRepository
    {
        public override async Task<List<OfferUnit>> GetAllAsync()
        {
            var units = await base.GetAllAsync();
            units.DesignateProducts(_container.Resolve<IProductDesignationService>());
            return units;
        }

        public override async Task<List<OfferUnit>> GetAllAsNoTrackingAsync()
        {
            var units = await base.GetAllAsNoTrackingAsync();
            units.DesignateProducts(_container.Resolve<IProductDesignationService>());
            return units;
        }
        public override List<OfferUnit> Find(Func<OfferUnit, bool> predicate)
        {
            var units = Context.Set<OfferUnit>().Include(nameof(SalesUnit.Product)).Where(predicate).ToList();
            units.DesignateProducts(_container.Resolve<IProductDesignationService>());
            return units;
        }
    }
}