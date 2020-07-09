using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class ProjectViewModel : UnitsContainer<Model.POCOs.Project, ProjectWrapper, ProjectDetailsViewModel, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
        }

        public override void Load(Model.POCOs.Project model, bool isNew, object parameter = null)
        {
            base.Load(model, isNew, parameter);
            //назначаем менеджера
            if (DetailsViewModel.Item.Manager == null)
            {
                var user = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                DetailsViewModel.Item.Manager = new UserWrapper(user);
            }
        }

        protected override IEnumerable<SalesUnit> GetUnits(Model.POCOs.Project project, object parameter = null)
        {
            return UnitOfWork.Repository<SalesUnit>().GetAll().Where(x => x.Project.Id == project.Id);
        }
    }
}