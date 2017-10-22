using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ParametersGroupsViewModel : BaseListViewModel<ParameterGroupWrapper, ParametersGroupDetailsViewModel>
    {
        public ParametersGroupsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            unitOfWork.ParametersGroups.GetAll().Select(x => new ParameterGroupWrapper(x)).ForEach(Items.Add);
        }
    }
}
