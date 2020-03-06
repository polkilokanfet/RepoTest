using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintOfferService
    {
        void PrintOffer(Guid offerId, string path = "");
    }
}
