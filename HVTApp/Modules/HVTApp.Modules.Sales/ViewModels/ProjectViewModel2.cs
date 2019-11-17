using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectViewModel2 : ViewModelBase
    {
        public ProjectWrapper Project { get; set; }

        public ProjectViewModel2(IUnityContainer container, Guid id = default(Guid)) : base(container)
        {
            var project = UnitOfWork.Repository<Project>().Find(x => x.Id == id).First();
        }
    }
}