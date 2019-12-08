using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class CreateNewProductTasksViewModel : CreateNewProductTaskLookupListViewModel
    {
        public ICommand ChangeProductCommand { get; }

        public CreateNewProductTasksViewModel(IUnityContainer container) : base(container)
        {
            ChangeProductCommand = new DelegateCommand(async () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    //продукт на замену
                    var targetProduct = await Container.Resolve<IGetProductService>().GetProductAsync();

                    var task = await unitOfWork.Repository<CreateNewProductTask>().GetByIdAsync(SelectedItem.Id);

                    if (targetProduct == null || targetProduct.Id == task.Product.Id) return;

                    targetProduct = await unitOfWork.Repository<Product>().GetByIdAsync(targetProduct.Id);

                    //юниты с новым продуктом
                    var salesUnits = unitOfWork.Repository<SalesUnit>().Find(x => x.Product.Id == task.Product.Id);
                    var offerUnits = unitOfWork.Repository<OfferUnit>().Find(x => x.Product.Id == task.Product.Id);

                    //меняем продукт в юнитах
                    salesUnits.ForEach(x => x.Product = targetProduct);
                    offerUnits.ForEach(x => x.Product = targetProduct);

                    //удаление параметров
                    var parameters = task.Product.ProductBlock.Parameters
                        .Where(x => x.Id != GlobalAppProperties.Actual.NewProductParameter.Id).ToList();
                    unitOfWork.Repository<Parameter>().DeleteRange(parameters);

                    //удаление связей параметров
                    var relations = (await unitOfWork.Repository<ParameterRelation>().GetAllAsync())
                        .Where(x => parameters.Select(p => p.Id).Contains(x.ParameterId)).ToList();
                    unitOfWork.Repository<ParameterRelation>().DeleteRange(relations);

                    //удаление блока
                    unitOfWork.Repository<ProductBlock>().Delete(task.Product.ProductBlock);

                    //удаление продукта
                    unitOfWork.Repository<Product>().Delete(task.Product);

                    //удаление задания
                    unitOfWork.Repository<CreateNewProductTask>().Delete(task);

                    await unitOfWork.SaveChangesAsync();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterRemoveCreateNewProductTaskEvent>().Publish(task);
                },
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup =>
            {
                ((DelegateCommand)ChangeProductCommand).RaiseCanExecuteChanged();
            };
        }
    }
}