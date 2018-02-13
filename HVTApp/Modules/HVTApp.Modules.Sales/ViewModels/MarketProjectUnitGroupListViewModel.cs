using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Extantions;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketProjectUnitGroupListViewModel : ProjectUnitGroupListViewModel
    {
        //public MarketProjectUnitGroupListViewModel(IUnityContainer container) : base(container)
        //{
        //    //Container.Resolve<IEventAggregator>().GetEvent<AfterSelectProjectEvent>().Subscribe(OnProjectSelect);
        //}

        //private Project _project;

        //private async void OnProjectSelect(PubSubEventArgs<Project> e)
        //{
        //    if (!(e.Sender.GetType() == typeof(MarketProjectListViewModel))) return;
        //    _project = e.Entity;
        //    await LoadAsync();
        //}

        //protected override async Task<IEnumerable<ProjectUnitGroup>> GetItems()
        //{
        //    var projectUnits = await UnitOfWork.GetRepository<ProjectUnit>().GetAllAsNoTrackingAsync();
        //    projectUnits = projectUnits.FindAll(x => Equals(x.ProjectId, _project.Id));
        //    return projectUnits.ConvertToGroup();
        //}
    }
}