using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitRepository
    {
        private IQueryable<SalesUnit> Query => Context.Set<SalesUnit>()
                                                      .AsQueryable()
                                                      .Include(x => x.Product.ProductBlock.Parameters)
                                                      .Include(x => x.PaymentsActual)
                                                      .Include(x => x.PaymentsPlanned);

        public override async Task<List<SalesUnit>> GetAllAsync()
        {
            var units = await Query.ToListAsync();
            Manipulate(units);
            return units;
        }

        public override async Task<List<SalesUnit>> GetAllAsNoTrackingAsync()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var units = await Query.AsNoTracking().ToListAsync();
            Manipulate(units);
            return units;
        }

        public override List<SalesUnit> Find(Func<SalesUnit, bool> predicate)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var units = Query.Where(predicate).ToList();
            Manipulate(units);
            return units;
        }

        private void Manipulate(SalesUnit unit)
        {
            //обозначение продукта
            unit.DesignateProduct(_container.Resolve<IProductDesignationService>());
            
            //срок доставки
            _container.Resolve<IShippingService>().SetShippingTerm(unit);
        }

        private void Manipulate(List<SalesUnit> units)
        {
            units.ForEach(Manipulate);
        }
    }
}