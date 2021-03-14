using System;
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
        /// Получить все юниты текущего пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetCurrentUserSalesUnits();

        ///// <summary>
        ///// Получить все юниты авторизованного пользователя асинхронно
        ///// </summary>
        ///// <returns></returns>
        //Task<IEnumerable<SalesUnit>> GetUsersSalesUnitsAsync();

        /// <summary>
        /// Получить все юниты из проекта
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetByProject(Guid projectId);

        /// <summary>
        /// Получить все юниты из заказа
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetByOrder(Guid orderId);

        /// <summary>
        /// Получить все юниты из спецификации
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetBySpecification(Guid specificationId);

        /// <summary>
        /// Получить все юниты с какими-либо актуальными платежами
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetAllWithActualPayments();

        /// <summary>
        /// Получить все юниты определенного пользователя с какими-либо актуальными платежами
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetAllWithActualPaymentsOfCurrentUser();

        /// <summary>
        /// Получить все юниты, подгрузив актуальные платежи
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetAllIncludeActualPayments();
    }

    public partial class SalesUnitRepository : ISalesUnitRepository
    {
        protected override IQueryable<SalesUnit> GetQuary()
        {
            return Context.Set<SalesUnit>().AsQueryable()
                .Include(salesUnit => salesUnit.Facility.Type)
                .Include(salesUnit => salesUnit.Product.ProductBlock.Parameters)
                .Include(salesUnit => salesUnit.Product.DependentProducts.Select(productDependent => productDependent.Product.ProductBlock.Parameters))
                .Include(salesUnit => salesUnit.ProductsIncluded.Select(productIncluded => productIncluded.Product.ProductBlock.Parameters))
                .Include(salesUnit => salesUnit.PaymentConditionSet.PaymentConditions.Select(paymentCondition => paymentCondition.PaymentConditionPoint));
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

        public IEnumerable<SalesUnit> GetCurrentUserSalesUnits()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return this.Find(salesUnit => salesUnit.Project.Manager.Id == GlobalAppProperties.User.Id);
        }

        public IEnumerable<SalesUnit> GetByProject(Guid projectId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var salesUnitsIds = this.Context.Set<SalesUnit>().AsNoTracking()
                .Where(salesUnit => salesUnit.Project.Id == projectId)
                .Select(salesUnit => salesUnit.Id)
                .ToList();

            foreach (var id in salesUnitsIds)
            {
                yield return this.GetById(id);
            }
        }

        public IEnumerable<SalesUnit> GetByOrder(Guid orderId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var salesUnitsIds = this.Context.Set<SalesUnit>().AsNoTracking()
                .Where(salesUnit => salesUnit.Order != null)
                .Where(salesUnit => salesUnit.Order.Id == orderId)
                .Select(salesUnit => salesUnit.Id)
                .ToList();

            foreach (var id in salesUnitsIds)
            {
                yield return this.GetById(id);
            }
        }

        public IEnumerable<SalesUnit> GetBySpecification(Guid specificationId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var salesUnitsIds = this.Context.Set<SalesUnit>().AsNoTracking()
                .Where(salesUnit => salesUnit.Specification != null)
                .Where(salesUnit => salesUnit.Specification.Id == specificationId)
                .Select(salesUnit => salesUnit.Id)
                .ToList();

            foreach (var id in salesUnitsIds)
            {
                yield return this.GetById(id);
            }
        }

        public IEnumerable<SalesUnit> GetAllWithActualPayments()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Include(salesUnit => salesUnit.PaymentsActual)
                .Include(salesUnit => salesUnit.Order)
                .Where(salesUnit => salesUnit.PaymentsActual.Any())
                .ToList();
        }

        public IEnumerable<SalesUnit> GetAllWithActualPaymentsOfCurrentUser()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Include(salesUnit => salesUnit.PaymentsActual)
                .Include(salesUnit => salesUnit.Order)
                .Include(salesUnit => salesUnit.Project.Manager)
                .Where(salesUnit => salesUnit.Project.Manager.Id == GlobalAppProperties.User.Id)
                .Where(salesUnit => salesUnit.PaymentsActual.Any())
                .ToList();
        }

        public IEnumerable<SalesUnit> GetAllIncludeActualPayments()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuary()
                .Include(salesUnit => salesUnit.PaymentsActual)
                .ToList();
        }

        //public async Task<IEnumerable<SalesUnit>> GetUsersSalesUnitsAsync()
        //{
        //    var su = await this.GetAllAsync();
        //    return su.Where(x => x.Project.Manager.IsAppCurrentUser());
        //}
    }
}