using System;
using System.Threading.Tasks;

namespace HVTApp.UI.Services
{
    public interface IOfferToDoc
    {
        Task PrintOfferAsync(Guid offerId);
    }
}
