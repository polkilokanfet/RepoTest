using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelConstructor : PriceEngineeringTaskViewModel
    {
        /// <summary>
        /// Выбрать блок продукта
        /// </summary>
        public DelegateLogCommand SelectProductBlockCommand { get; }

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) 
            : base(container, unitOfWork, priceEngineeringTask)
        {
            SelectProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var department = unitOfWork.Repository<DesignDepartment>()
                        .Find(designDepartment => designDepartment.ProductBlockIsSuitable(ProductBlockManager.Model))
                        .FirstOrDefault();

                    if (department == null)
                        return;

                    var requiredParameters = department.ParameterSets
                        .FirstOrDefault(x => x.Parameters.AllContainsInById(ProductBlockManager.Model.Parameters));

                    var getProductService = container.Resolve<IGetProductService>();
                    var originProductBlock = this.ProductBlockEngineer.Model;
                    var selectedProductBlock = getProductService.GetProductBlock(originProductBlock, requiredParameters.Parameters);
                    if (originProductBlock.Id != selectedProductBlock.Id)
                    {
                        this.ProductBlockEngineer = new ProductBlockEmptyWrapper(selectedProductBlock);
                    }
                },
                () =>
                {
                    return true;
                });
        }

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
            throw new System.NotImplementedException();
        }

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
            throw new System.NotImplementedException();
        }
    }
}