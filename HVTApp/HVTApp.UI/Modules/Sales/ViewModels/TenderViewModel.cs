using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class TenderViewModel : TenderDetailsViewModel
    {
        /// <summary>
        ///  онструктор дл€ создани€ нового конкурса
        /// </summary>
        /// <param name="container"></param>
        /// <param name="project"></param>
        public TenderViewModel(IUnityContainer container, Project project) : base(container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();
            Load(new TenderWrapper(new Tender()), unitOfWork);
            project = UnitOfWork.Repository<Project>().Find(x => x.Id == project.Id).First();
            Item.Project = new ProjectWrapper(project);
            Item.DateOpen = DateTime.Today;
            Item.DateClose = DateTime.Today.AddDays(7);
        }

        /// <summary>
        ///  онструктор дл€ редактировани€ конкурса
        /// </summary>
        /// <param name="container"></param>
        /// <param name="tender"></param>
        public TenderViewModel(IUnityContainer container, Tender tender) : base(container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();
            var tenderEdit = unitOfWork.Repository<Tender>().Find(x => x.Id == tender.Id).First();
            Load(new TenderWrapper(tenderEdit), unitOfWork);
        }
    }
}