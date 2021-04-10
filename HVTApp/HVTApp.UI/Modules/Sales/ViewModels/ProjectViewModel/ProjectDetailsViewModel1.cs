using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class ProjectDetailsViewModel1 : BaseDetailsViewModel<ProjectWrapper1, Project, AfterSaveProjectEvent>
    {
        public ICommand SelectProjectTypeCommand { get; }
        public ICommand ClearProjectTypeCommand { get; }

        public ProjectDetailsViewModel1(IUnityContainer container) : base(container)
        {

            SelectProjectTypeCommand = new DelegateCommand(
                () =>
                {
                    ProjectType[] projectTypes = UnitOfWork.Repository<ProjectType>().GetAll();

                    //выбор сущности
                    var selectedProjectType = Container.Resolve<ISelectService>().SelectItem(projectTypes, Item.ProjectType?.Model.Id);

                    //замена текущего значения новым
                    if (selectedProjectType != null && !Equals(selectedProjectType.Id, Item.ProjectType?.Model.Id))
                    {
                        selectedProjectType = UnitOfWork.Repository<ProjectType>().GetById(selectedProjectType.Id);
                        Item.ProjectType = new ProjectTypeSimpleWrapper(selectedProjectType);
                    }
                });

            ClearProjectTypeCommand = new DelegateCommand(() => { Item.ProjectType = null; });
        }
    }
}