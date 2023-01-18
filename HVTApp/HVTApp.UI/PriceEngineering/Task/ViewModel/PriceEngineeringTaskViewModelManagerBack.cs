using System;
using HVTApp.Model.POCOs;
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

        protected override void InitializeProductBlockEngineerProperty()
        {
            throw new NotImplementedException();
        }

        protected override PriceEngineeringTaskProductBlockAddedWrapper1 GetPriceEngineeringTaskProductBlockAddedWrapper(
            PriceEngineeringTaskProductBlockAdded p)
        {
            throw new NotImplementedException();
        }
    }
}