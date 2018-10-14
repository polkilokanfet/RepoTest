using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectViewModel : UnitsContainer<Project, ProjectWrapper, ProjectDetailsViewModel, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
        }

        public override async Task LoadAsync(Project model, bool isNew, object parameter = null)
        {
            await base.LoadAsync(model, isNew, parameter);
            //назначаем менеджера
            if (DetailsViewModel.Item.Manager == null)
            {
                var user = await UnitOfWork.Repository<User>().GetByIdAsync(GlobalAppProperties.User.Id);
                DetailsViewModel.Item.Manager = new UserWrapper(user);
            }

        }

        protected override async Task<IEnumerable<SalesUnit>> GetUnits(Project project, object parameter = null)
        {
            return UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id);
        }
    }
}