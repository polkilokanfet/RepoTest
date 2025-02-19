using System.Collections.Generic;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectDetailsViewModel1 : BaseDetailsViewModel<ProjectWrapper1, Project, AfterSaveProjectEvent>
    {
        public ICommand SelectProjectTypeCommand { get; }
        public ICommand ClearProjectTypeCommand { get; }

        public ProjectDetailsViewModel1(IUnityContainer container) : base(container)
        {
            SelectProjectTypeCommand = new DelegateLogCommand(
                () =>
                {
                    var projectTypes = UnitOfWork.Repository<ProjectType>().GetAll();

                    //выбор сущности
                    var selectedProjectType = Container.Resolve<ISelectService>().SelectItem(projectTypes, Item.ProjectType?.Model.Id);

                    //замена текущего значения новым
                    if (selectedProjectType != null && !Equals(selectedProjectType.Id, Item.ProjectType?.Model.Id))
                    {
                        selectedProjectType = UnitOfWork.Repository<ProjectType>().GetById(selectedProjectType.Id);
                        Item.ProjectType = new ProjectTypeSimpleWrapper(selectedProjectType);
                    }
                });

            ClearProjectTypeCommand = new DelegateLogCommand(() => { Item.ProjectType = null; });
        }
    }
}