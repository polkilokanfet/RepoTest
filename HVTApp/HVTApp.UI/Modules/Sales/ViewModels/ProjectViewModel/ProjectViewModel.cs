using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class ProjectViewModel : UnitsContainer<Project, ProjectWrapper, ProjectDetailsViewModel, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
        }

        public override async Task LoadAsync(Project model, bool isNew, object parameter = null)
        {
            await base.LoadAsync(model, isNew, parameter);
            //��������� ���������
            if (DetailsViewModel.Item.Manager == null)
            {
                var user = await UnitOfWork.Repository<User>().GetByIdAsync(GlobalAppProperties.User.Id);
                DetailsViewModel.Item.Manager = new UserWrapper(user);
            }
        }

        protected override async Task<IEnumerable<SalesUnit>> GetUnits(Project project, object parameter = null)
        {
            return (await UnitOfWork.Repository<SalesUnit>().GetAllAsync()).Where(x => x.Project.Id == project.Id);
        }
    }
}