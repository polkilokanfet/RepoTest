using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CurrencyWrapperDataService : WrapperDataService<Currency, CurrencyWrapper>
    {
        public CurrencyWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override CurrencyWrapper GenerateWrapper(Currency model)
        {
            return new CurrencyWrapper(model);
        }
    }
}