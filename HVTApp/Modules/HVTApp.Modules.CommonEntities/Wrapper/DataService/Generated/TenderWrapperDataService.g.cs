using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TenderWrapperDataService : WrapperDataService<Tender, TenderWrapper>
    {
        public TenderWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override TenderWrapper GenerateWrapper(Tender model)
        {
            return new TenderWrapper(model);
        }
    }
}
