using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskViewModel
    {
        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(container, unitOfWork, priceEngineeringTask)
        {
        }
    }
}