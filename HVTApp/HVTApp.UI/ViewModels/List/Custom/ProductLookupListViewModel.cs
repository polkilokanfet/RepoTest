using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductLookupListViewModel
    {
        public DelegateLogCommand CleanEmptyProductsCommand { get; private set; }

        protected override void InitSpecialCommands()
        {
            CleanEmptyProductsCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    var productsAll = unitOfWork.Repository<Product>().GetAll();

                    var productsFromSalesUnits = unitOfWork.Repository<SalesUnit>().GetAll().Select(x => x.Product).ToList();
                    var productsFromOfferUnit = unitOfWork.Repository<OfferUnit>().GetAll().Select(x => x.Product).ToList();
                    var productsFromProductDependent = unitOfWork.Repository<ProductDependent>().GetAll().Select(x => x.Product).ToList();
                    var productsFromProductIncluded = unitOfWork.Repository<ProductIncluded>().GetAll().Select(x => x.Product).ToList();

                    foreach (var product in productsAll)
                    {
                        if (productsFromSalesUnits.Contains(product) == false &&
                            productsFromOfferUnit.Contains(product) == false &&
                            productsFromProductDependent.Contains(product) == false &&
                            productsFromProductIncluded.Contains(product) == false)
                        {
                            unitOfWork.Repository<Product>().Delete(product);
                        }
                    }

                    unitOfWork.SaveChanges();
                });
        }
    }
}