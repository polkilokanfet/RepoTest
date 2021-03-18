using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial interface IOfferRepository
    {
        /// <summary>
        /// Получить все ТКП текущего пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<Offer> GetAllOfCurrentUser();
    }
}