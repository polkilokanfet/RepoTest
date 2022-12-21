using System;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManagerBack : PriceEngineeringTaskViewModel
    {
        public override bool IsTarget => true;

        public override bool IsEditMode => true;

        public PriceEngineeringTaskViewModelManagerBack(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }
    }
}