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
    public partial class SalesUnitRepository
    {
        public override async Task<List<SalesUnit>> GetAllAsync()
        {
            var units = await base.GetAllAsync();
            Manipulate(units);
            return units;
        }

        public override async Task<List<SalesUnit>> GetAllAsNoTrackingAsync()
        {
            var units = await base.GetAllAsNoTrackingAsync();
            Manipulate(units);
            return units;
        }

        public override List<SalesUnit> Find(Func<SalesUnit, bool> predicate)
        {
            var units = Context.Set<SalesUnit>().Include(nameof(SalesUnit.Product))
                                                .Include(nameof(SalesUnit.PaymentsActual))
                                                .Include(nameof(SalesUnit.PaymentsPlanned)).Where(predicate).ToList();
            Manipulate(units);
            return units;
        }

        private void Manipulate(SalesUnit unit)
        {
            unit.DesignateProduct(_container.Resolve<IProductDesignationService>());
            _container.Resolve<IShippingService>().SetShippingTerm(unit);
        }

        private void Manipulate(List<SalesUnit> units)
        {
            units.ForEach(Manipulate);
        }
    }
}