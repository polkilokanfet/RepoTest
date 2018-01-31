using System;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketTenderListViewModel : TenderListViewModel
    {
        public MarketTenderListViewModel(IUnityContainer container) : base(container)
        {
            container.Resolve<IEventAggregator>().GetEvent<AfterSelectProjectEvent>().Subscribe(OnSelectProjectEvent);
        }

        private async void OnSelectProjectEvent(PubSubEventArgs<Project> e)
        {
            if (!Equals(e.Sender.GetType(), typeof(MarketProjectListViewModel))) return;

            var tenders = UnitOfWork.GetRepository<Tender>().Find(x => Equals(e.Entity.Id, x.ProjectId));
            await InjectItems(tenders);
        }
    }
}