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

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
            SelectProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var department = unitOfWork.Repository<DesignDepartment>()
                        .Find(designDepartment => designDepartment.ProductBlockIsSuitable(PriceEngineeringTaskWrapper.ProductBlockManager.Model))
                        .FirstOrDefault();

                    if (department == null)
                        return;

                    var requiredParameters = department.ParameterSets
                        .FirstOrDefault(x => x.Parameters.AllContainsInById(PriceEngineeringTaskWrapper.ProductBlockManager.Model.Parameters));

                    var getProductService = container.Resolve<IGetProductService>();
                    var originProductBlock = this.PriceEngineeringTaskWrapper.ProductBlockEngineer.Model;
                    var selectedProductBlock = getProductService.GetProductBlock(originProductBlock, requiredParameters.Parameters);
                    if (originProductBlock.Id != selectedProductBlock.Id)
                    {
                        this.PriceEngineeringTaskWrapper.ProductBlockEngineer = new ProductBlockWrapper(selectedProductBlock);
                    }
                },
                () =>
                {
                    return true;
                });
        }
    }
}