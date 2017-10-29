using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class AdditionalSalesUnitsWrapperDataService : WrapperDataService<AdditionalSalesUnits, AdditionalSalesUnitsWrapper>
    {
        public AdditionalSalesUnitsWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override AdditionalSalesUnitsWrapper GenerateWrapper(AdditionalSalesUnits model)
        {
            return new AdditionalSalesUnitsWrapper(model);
        }
    }
}
