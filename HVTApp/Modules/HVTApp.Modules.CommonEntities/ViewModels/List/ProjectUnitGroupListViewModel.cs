namespace HVTApp.UI.ViewModels
{
    public partial class ProjectUnitGroupListViewModel //: BaseListViewModel<>
    {
        //protected override async Task<IEnumerable<UnitGroup>> GetItems()
        //{
        //    var units = await UnitOfWork.GetRepository<ProjectUnit>().GetAllAsNoTrackingAsync();
        //    return units.ConvertToGroup();
        //}

        //protected override void SubscribesToEvents()
        //{
        //    _container.Resolve<IEventAggregator>().GetEvent<AfterSaveProjectUnitEvent>().Subscribe(OnAfterSaveProjectUnitEvent);
        //}

        //private async void OnAfterSaveProjectUnitEvent(ProjectUnit projectUnit)
        //{
        //    await LoadAsync();
        //}
    }
}