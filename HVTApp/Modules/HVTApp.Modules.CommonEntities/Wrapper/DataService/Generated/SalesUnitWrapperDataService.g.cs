using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class SalesUnitWrapperDataService : WrapperDataService<SalesUnit, SalesUnitWrapper>
    {
        public SalesUnitWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override SalesUnitWrapper GenerateWrapper(SalesUnit model)
        {
            return new SalesUnitWrapper(model);
        }
    }
}