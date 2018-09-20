using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TenderViewModel : TenderDetailsViewModel
    {
        public TenderViewModel(IUnityContainer container, Project project) : base(container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();
            Load(new TenderWrapper(new Tender()), unitOfWork);
            project = UnitOfWork.Repository<Project>().Find(x => x.Id == project.Id).First();
            Item.Project = new ProjectWrapper(project);
            Item.DateOpen = DateTime.Today;
            Item.DateClose = DateTime.Today.AddDays(7);
        }
        public TenderViewModel(IUnityContainer container, Tender tender) : base(container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();
            Load(new TenderWrapper(tender), unitOfWork);
        }
    }
}