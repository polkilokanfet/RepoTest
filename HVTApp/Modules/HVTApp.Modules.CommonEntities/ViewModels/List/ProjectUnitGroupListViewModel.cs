using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Extantions;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectUnitGroupListViewModel
    {
        //protected override async Task<IEnumerable<ProjectUnitGroup>> GetItems()
        //{
        //    var units = await UnitOfWork.GetRepository<ProjectUnit>().GetAllAsNoTrackingAsync();
        //    return units.ConvertToGroup();
        //}

        //protected override void SubscribesToEvents()
        //{
        //    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProjectUnitEvent>().Subscribe(OnAfterSaveProjectUnitEvent);
        //}

        //private async void OnAfterSaveProjectUnitEvent(ProjectUnit projectUnit)
        //{
        //    await LoadAsync();
        //}
    }
}