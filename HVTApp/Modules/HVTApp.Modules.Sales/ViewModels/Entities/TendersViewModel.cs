using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TendersViewModel : BaseListViewModel<TenderWrapper, TenderDetailsViewModel>
    {
        public TendersViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            unitOfWork.Tenders.GetAll().Select(x => new TenderWrapper(x)).ForEach(Items.Add);
        }
    }
}
