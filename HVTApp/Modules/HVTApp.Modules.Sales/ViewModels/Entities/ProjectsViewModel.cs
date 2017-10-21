using System.Linq;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : BaseListViewModel<ProjectWrapper, ProjectDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;

            unitOfWork.Projects.GetAll().Select(x => new ProjectWrapper(x)).ForEach(Items.Add);
        }
    }
}
