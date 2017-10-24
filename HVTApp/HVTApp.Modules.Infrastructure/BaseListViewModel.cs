using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Infrastructure
{
    public class BaseListViewModel<TWrapper, TDelailsViewModel> : EditableSelectableBindableBase<TWrapper>, IBaseListViewModel<TWrapper> 
        where TWrapper : class, IWrapper<IBaseEntity>
        where TDelailsViewModel : IDetailsViewModel<TWrapper> 
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

        protected override void NewItemCommand_Execute()
        {
            //TWrapper item = _unitOfWork.GetWrapper<TWrapper>();
            TWrapper item = default(TWrapper);

            TDelailsViewModel delailsViewModel = Container.Resolve<TDelailsViewModel>(new ParameterOverride("item", item));
            bool? dialogResult = DialogService.ShowDialog(delailsViewModel);
            if (!dialogResult.HasValue || !dialogResult.Value) return;

            UnitOfWork.AddItem(delailsViewModel.Item);
            UnitOfWork.Complete();

            Items.Add(delailsViewModel.Item);
            SelectedItem = delailsViewModel.Item;
        }

        protected override void EditItemCommand_Execute()
        {
            TDelailsViewModel delailsViewModel = Container.Resolve<TDelailsViewModel>(new ParameterOverride("item", SelectedItem));

            var dialogResult = DialogService.ShowDialog(delailsViewModel);
            if (dialogResult.HasValue && dialogResult.Value)
            {
                SelectedItem.AcceptChanges();
                UnitOfWork.Complete();
            }
            else
            {
                if (SelectedItem.IsChanged)
                    SelectedItem.RejectChanges();
            }
        }

        protected override void RemoveItemCommand_Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}