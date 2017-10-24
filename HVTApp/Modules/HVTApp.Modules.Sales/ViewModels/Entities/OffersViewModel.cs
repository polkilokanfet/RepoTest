using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : BaseListViewModel<OfferWrapper, Offer, OfferDetailsWindowModel>
    {
        public OffersViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(container)
        {
            
        }
    }
}
