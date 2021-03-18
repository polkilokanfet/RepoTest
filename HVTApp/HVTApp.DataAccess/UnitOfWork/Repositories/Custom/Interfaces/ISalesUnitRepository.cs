using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial interface ISalesUnitRepository : IRepository<SalesUnit>
    {
        /// <summary>
        /// Получить все юниты текущего пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetAllOfCurrentUser();

        /// <summary>
        /// Получить все неудаленные юниты текущего пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetAllOfCurrentUserForMarketView();

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

        /// <summary>
        /// Получить все не удаленные и не проигранные юниты
        /// </summary>
        /// <returns></returns>
        IEnumerable<SalesUnit> GetAllNotRemovedNotLoosen();
    }
}