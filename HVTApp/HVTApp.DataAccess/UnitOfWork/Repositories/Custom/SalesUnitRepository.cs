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
        /// <summary>
        /// Получить все юниты авторизованного пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetUsersSalesUnits();

        ///// <summary>
        ///// Получить все юниты авторизованного пользователя асинхронно
        ///// </summary>
        ///// <returns></returns>
        //Task<IEnumerable<SalesUnit>> GetUsersSalesUnitsAsync();
    }

    public partial class SalesUnitRepository
    {
        protected override IQueryable<SalesUnit> GetQuary()
        {
            return Context.Set<SalesUnit>().AsQueryable()
                .Include(x => x.Facility.Type)
                .Include(x => x.Product.ProductBlock.Parameters)
                .Include(x => x.Product.DependentProducts.Select(dp => dp.Product.ProductBlock.Parameters))
                .Include(x => x.ProductsIncluded.Select(dp => dp.Product.ProductBlock.Parameters))
                .Include(x => x.PaymentConditionSet.PaymentConditions.Select(pc => pc.PaymentConditionPoint));
        }

        //protected override IQueryable<SalesUnit> GetQuary()
        //{
        //    return Context.Set<SalesUnit>().AsQueryable()
        //        .Include(x => x.Facility)
        //        .Include(x => x.Facility.Type)
        //        .Include(x => x.Facility.Address.Locality.Region.District.Country)
        //        .Include(x => x.Facility.OwnerCompany)
        //        .Include(x => x.Facility.OwnerCompany.AddressLegal.Locality.Region.District.Country)
        //        .Include(x => x.Project)
        //        .Include(x => x.Project.Manager)
        //        .Include(x => x.Product.ProductBlock.Parameters)
        //        .Include(x => x.Product.DependentProducts.Select(dp => dp.Product.ProductBlock.Parameters))
        //        .Include(x => x.ProductsIncluded.Select(dp => dp.Product.ProductBlock.Parameters))
        //        .Include(x => x.PaymentsActual)
        //        .Include(x => x.PaymentsPlanned)
        //        .Include(x => x.PaymentConditionSet)
        //        .Include(x => x.PaymentConditionSet.PaymentConditions)
        //        .Include(x => x.Order)
        //        .Include(x => x.Penalty)
        //        .Include(x => x.Producer)
        //        .Include(x => x.AddressDelivery);
        //}

        public IEnumerable<SalesUnit> GetUsersSalesUnits()
        {
            return this.Find(x => x.Project.Manager.IsAppCurrentUser());
        }

        //public async Task<IEnumerable<SalesUnit>> GetUsersSalesUnitsAsync()
        //{
        //    var su = await this.GetAllAsync();
        //    return su.Where(x => x.Project.Manager.IsAppCurrentUser());
        //}
    }
}