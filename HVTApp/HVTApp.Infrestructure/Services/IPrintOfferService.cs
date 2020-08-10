using System;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintOfferService
    {
        void PrintOffer(Guid offerId, string path = "");
    }
}
