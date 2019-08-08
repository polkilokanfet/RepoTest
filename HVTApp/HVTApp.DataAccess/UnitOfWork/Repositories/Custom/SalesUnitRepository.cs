using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial interface ISalesUnitRepository : IRepository<SalesUnit>
    {
        IEnumerable<SalesUnit> GetUsersSalesUnits();
        Task<IEnumerable<SalesUnit>> GetUsersSalesUnitsAsync();
    }

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

        public IEnumerable<SalesUnit> GetUsersSalesUnits()
        {
            return this.Find(x => x.Project.Manager.Id == GlobalAppProperties.User.Id);
        }

        public async Task<IEnumerable<SalesUnit>> GetUsersSalesUnitsAsync()
        {
            var su = await this.GetAllAsync();
            return su.Where(x => x.Project.Manager.Id == GlobalAppProperties.User.Id);
        }
    }
}