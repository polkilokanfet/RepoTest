using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ExchangeCurrencyRateWrapperDataService : WrapperDataService<ExchangeCurrencyRate, ExchangeCurrencyRateWrapper>
    {
        public ExchangeCurrencyRateWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ExchangeCurrencyRateWrapper GenerateWrapper(ExchangeCurrencyRate model)
        {
            return new ExchangeCurrencyRateWrapper(model);
        }
    }
}
