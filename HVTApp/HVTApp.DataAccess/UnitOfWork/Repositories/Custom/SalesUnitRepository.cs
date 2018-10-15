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
        protected override IQueryable<SalesUnit> GetQuary()
        {
            return Context.Set<SalesUnit>().AsQueryable()
                .Include(x => x.Facility)
                .Include(x => x.Project.Manager)
                .Include(x => x.Product.ProductBlock.Parameters)
                .Include(x => x.Product.DependentProducts.Select(dp => dp.Product.ProductBlock.Parameters))
                .Include(x => x.ProductsIncluded.Select(dp => dp.Product.ProductBlock.Parameters))
                .Include(x => x.PaymentsActual)
                .Include(x => x.PaymentsPlanned)
                .Include(x => x.Order);
        }

        public override async Task<List<SalesUnit>> GetAllAsync()
        {
            return Manipulate(await base.GetAllAsync());
        }

        public override async Task<List<SalesUnit>> GetAllAsNoTrackingAsync()
        {
            return Manipulate(await base.GetAllAsNoTrackingAsync());
        }

        public override List<SalesUnit> Find(Func<SalesUnit, bool> predicate)
        {
            return Manipulate(base.Find(predicate));
        }

        private void Manipulate(SalesUnit unit)
        {           
            //срок доставки
            _container.Resolve<IShippingService>().DeliveryTerm(unit);
        }

        private List<SalesUnit> Manipulate(List<SalesUnit> units)
        {
            units.ForEach(Manipulate);
            return units;
        }
    }
}