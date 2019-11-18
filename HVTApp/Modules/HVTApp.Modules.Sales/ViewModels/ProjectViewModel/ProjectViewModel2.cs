using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectViewModel2 : ViewModelBase
    {
        public ProjectWithGroupsWrapper Project { get; private set; }
        public object SelectedItem { get; set; }

        public ProjectViewModel2(IUnityContainer container) : base(container)
        {
        }

        public void Load(Guid id)
        {
            var project = UnitOfWork.Repository<Project>().GetById(id);
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == id);
            Project = new ProjectWithGroupsWrapper(project, salesUnits);
        }
    }
}