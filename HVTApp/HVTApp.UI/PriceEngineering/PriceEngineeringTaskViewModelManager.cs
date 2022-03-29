using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskViewModel
    {
        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }
    }
}