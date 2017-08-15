using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ActivityFildsViewModel : BaseListViewModel<ActivityFieldWrapper, ActivityFildDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActivityFildsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.ActivityFields.GetAll().ForEach(Items.Add);
        }
    }
}
