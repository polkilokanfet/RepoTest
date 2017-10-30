using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class StandartPaymentConditionsWrapperDataService : WrapperDataService<StandartPaymentConditions, StandartPaymentConditionsWrapper>
    {
        public StandartPaymentConditionsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override StandartPaymentConditionsWrapper GenerateWrapper(StandartPaymentConditions model)
        {
            return new StandartPaymentConditionsWrapper(model);
        }
    }
}
