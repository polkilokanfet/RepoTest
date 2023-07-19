using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class SalesUnitDetailsViewModel
    {
        protected override void InitSpecialCommands()
        {
            SelectProductCommand = new DelegateLogCommand(SelectProductCommand_Execute);
        }

        protected override void SaveItem()
        {
            if (AllowSave() == false) return;

            //сохраняем
            if (UnitOfWork.SaveEntity(Item.Model).OperationCompletedSuccessfully)
            {
                ////Принимаем изменения
                //Item.AcceptChanges();

                //Сигнализируем о сохранении сущности
                EventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(Item.Model);

                if (SaveCommand is DelegateLogCommand delegateCommand)
                    delegateCommand.RaiseCanExecuteChanged();

                //запрашиваем закрытие окна
                OnCloseRequested(new DialogRequestCloseEventArgs(true));
            }
        }


        private void SelectProductCommand_Execute()
        {
            var product = Container.Resolve<IGetProductService>().GetProduct(Item.Model.Product);
            if (product != null)
            {
                product = UnitOfWork.Repository<Product>().GetById(product.Id);
                Item.Product = new ProductWrapper(product);
            }
        }
    }
}