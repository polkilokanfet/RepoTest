using System;
using System.Threading.Tasks;

namespace HVTApp.Services.OfferToDocService
{
    public interface IOfferToDoc
    {
        Task GenerateOfferDocAsync(Guid offerId);
    }
}
