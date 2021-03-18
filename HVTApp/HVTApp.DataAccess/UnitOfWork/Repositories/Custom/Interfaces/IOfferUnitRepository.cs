using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial interface IOfferUnitRepository
    {
        /// <summary>
        /// Получить все ТКП текущего пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<OfferUnit> GetAllOfCurrentUser();

        IEnumerable<OfferUnit> GetByOffer(Offer offer);
    }
}