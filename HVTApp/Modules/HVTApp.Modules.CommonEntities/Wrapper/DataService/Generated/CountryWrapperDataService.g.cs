using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CountryWrapperDataService : WrapperDataService<Country, CountryWrapper>
    {
        public CountryWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override CountryWrapper GenerateWrapper(Country model)
        {
            return new CountryWrapper(model);
        }
    }
}
