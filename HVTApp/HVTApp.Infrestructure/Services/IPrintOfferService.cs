using System;
using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintOfferService
    {
        Task PrintOfferAsync(Guid offerId);
    }
}
