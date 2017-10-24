using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Infrastructure
{
    public class BaseListViewModel<TWrapper, TEntity, TDelailsViewModel> : EditableSelectableBindableBase<TWrapper>, IBaseListViewModel<TWrapper> 
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TDelailsViewModel : IDetailsViewModel<TWrapper, TEntity> 
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IUnityContainer Container;
        protected readonly IDialogService DialogService;

        public BaseListViewModel(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            DialogService = Container.Resolve<IDialogService>();
        }

        protected override async void NewItemCommand_ExecuteAsync()
        {
            var viewModel = Container.Resolve<TDelailsViewModel>();
            await viewModel.LoadAsync(SelectedItem.Model.Id);
            DialogService.ShowDialog(viewModel);
        }

        protected override async void EditItemCommand_ExecuteAsync()
        {
            var viewModel = Container.Resolve<TDelailsViewModel>();
            await viewModel.LoadAsync(SelectedItem.Model.Id);
            DialogService.ShowDialog(viewModel);
        }

        protected override void RemoveItemCommand_Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}