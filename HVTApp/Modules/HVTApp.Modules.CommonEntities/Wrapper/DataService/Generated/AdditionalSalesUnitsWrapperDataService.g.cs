using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class AdditionalSalesUnitsWrapperDataService : WrapperDataService<AdditionalSalesUnits, AdditionalSalesUnitsWrapper>
    {
        public AdditionalSalesUnitsWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override AdditionalSalesUnitsWrapper GenerateWrapper(AdditionalSalesUnits model)
        {
            return new AdditionalSalesUnitsWrapper(model);
        }
    }
}