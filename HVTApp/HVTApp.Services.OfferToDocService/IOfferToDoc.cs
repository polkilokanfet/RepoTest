using System.Threading.Tasks;
using HVTApp.UI.Wrapper;

namespace HVTApp.Services.OfferToDocService
{
    public interface IOfferToDoc
    {
        Task GenerateOfferDoc(OfferWrapper offer);
    }
}
