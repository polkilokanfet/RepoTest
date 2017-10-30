using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class AdditionalSalesUnitsWrapperDataService : WrapperDataService<AdditionalSalesUnits, AdditionalSalesUnitsWrapper>
    {
        public AdditionalSalesUnitsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override AdditionalSalesUnitsWrapper GenerateWrapper(AdditionalSalesUnits model)
        {
            return new AdditionalSalesUnitsWrapper(model);
        }
    }
}
