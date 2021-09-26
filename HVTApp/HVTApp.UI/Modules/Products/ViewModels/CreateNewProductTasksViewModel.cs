using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class CreateNewProductTasksViewModel : CreateNewProductTaskLookupListViewModel
    {
        public DelegateLogCommand ChangeProductCommand { get; }

        public CreateNewProductTasksViewModel(IUnityContainer container) : base(container)
        {
            ChangeProductCommand = new DelegateLogCommand(() =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    //продукт на замену
                    var targetProduct = Container.Resolve<IGetProductService>().GetProduct();

                    var task = unitOfWork.Repository<CreateNewProductTask>().GetById(SelectedItem.Id);

                    if (targetProduct == null || targetProduct.Id == task.Product.Id) return;

                    targetProduct = unitOfWork.Repository<Product>().GetById(targetProduct.Id);

                    //юниты с новым продуктом
                    var salesUnits = unitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.Product.Id == task.Product.Id);
                    var offerUnits = unitOfWork.Repository<OfferUnit>().Find(offerUnit => offerUnit.Product.Id == task.Product.Id);

                    //меняем продукт в юнитах
                    salesUnits.ForEach(salesUnit => salesUnit.Product = targetProduct);
                    offerUnits.ForEach(offerUnit => offerUnit.Product = targetProduct);

                    //удаление параметров
                    var parameters = task.Product.ProductBlock.Parameters
                        .Where(parameter => parameter.Id != GlobalAppProperties.Actual.NewProductParameter.Id).ToList();
                    unitOfWork.Repository<Parameter>().DeleteRange(parameters);

                    //удаление связей параметров
                    var relations = unitOfWork.Repository<ParameterRelation>().GetAll()
                        .Where(x => parameters.Select(p => p.Id).Contains(x.ParameterId)).ToList();
                    unitOfWork.Repository<ParameterRelation>().DeleteRange(relations);

                    //удаление блока
                    unitOfWork.Repository<ProductBlock>().Delete(task.Product.ProductBlock);

                    //удаление продукта
                    unitOfWork.Repository<Product>().Delete(task.Product);

                    //удаление задания
                    unitOfWork.Repository<CreateNewProductTask>().Delete(task);

                    if (unitOfWork.SaveChanges().OperationCompletedSuccessfully)
                    {
                        Container.Resolve<IEventAggregator>().GetEvent<AfterRemoveCreateNewProductTaskEvent>().Publish(task);
                    }
                },
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup =>
            {
                ChangeProductCommand.RaiseCanExecuteChanged();
            };
        }
    }
}